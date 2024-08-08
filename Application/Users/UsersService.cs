using Application.Encrypt;
using Domain.Abstractions.Providers;
using Domain.Authentication;
using Domain.Authentication.Request;
using Domain.Authentication.Response;
using Domain.Interfaces.Users;
using Domain.ModelsDto;
using Domain.Responses;
using Domain.Responses.User;
using Domain.SharedModels;
using Domain.User;
using log4net;
using Microsoft.AspNetCore.Mvc.ModelBinding.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Users
{
    public class UsersService : ICommonService, IUserService
    {
        public static readonly DateTime Expiration = DateTime.UtcNow.AddHours(10);
        private ILog _logger;
        private readonly ICommonRepository _commonRepository;
        private readonly IUsersRepository _userRepository;
        private readonly string _pepper;
        private readonly int _iteration = 3;
        private readonly IMemoryCache _memoryCache;
        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();
        private readonly int cacheTimeMinutes = 600;
        private readonly IAuthenticationService _authenticationService;
        public UsersService(IEnumerable<ICommonRepository> commonRepository, IUsersRepository usersRepository, IMemoryCache memoryCache,
            IAuthenticationService authenticationService)
        {
            _commonRepository = commonRepository.First(x => x.Provider == Providers.User);
            _userRepository = usersRepository;
            _logger = LogManager.GetLogger(typeof(UsersService));
            _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
            _memoryCache = memoryCache;
            _authenticationService = authenticationService; 
        }

        public async Task<CommonResponse<bool>> Delete<T>(string id)
        {
            var response = new CommonResponse<bool>();
            try
            {
                response.Data = await _commonRepository.Delete<T>(id);
                response.Result = new Result { ResultNumber = response.Data ? 0 : 1, ErrorMessage = response.Data ? string.Empty : "User not found" };
            }
            catch (Exception ex)
            {
                response.Result = new Result { ResultNumber = 2, ErrorMessage = ex.Message };
            }
            CleanCache();
            return response;
        }
        public async Task<CommonResponse<IEnumerable<T>>> GetAll<T>()
        {
            var key = "USERS";
            try
            {
                if (!_memoryCache.TryGetValue(key, out CommonResponse<IEnumerable<T>> users))
                {
                    users = new CommonResponse<IEnumerable<T>>();
                    users.Data = await _commonRepository.GetAll<T>();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheTimeMinutes))
                        .AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
                    _memoryCache.Set(key, users, cacheEntryOptions);
                    users.Result = new Result { ResultNumber = 0, ErrorMessage = string.Empty };
                }

                return users;
            }
            catch (Exception ex)
            {
                return new CommonResponse<IEnumerable<T>>
                {
                    Result = new Result { ResultNumber = 1, ErrorMessage = ex.Message }
                };
            }
        }


        public async Task<CommonResponse<T>> GetAsync<T>(string id)
        {
            var key = $"USER_{id}";
            try
            {
                if (!_memoryCache.TryGetValue(key, out CommonResponse<T>  response))
                {
                    response = new CommonResponse<T>();
                    response.Data = await _commonRepository.GetAsync<T>(id);

                    if (response.Data != null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheTimeMinutes))
                            .AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
                        _memoryCache.Set(key, response, cacheEntryOptions);
                    }

                    response.Result = new Result
                    {
                        ResultNumber = response.Data != null ? 0 : 1,
                        ErrorMessage = response.Data != null ? string.Empty : "User not found"
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new CommonResponse<T>
                {
                    Result = new Result { ResultNumber = 2, ErrorMessage = ex.Message }
                };
            }
        }


        public async Task<CommonResponse<T>> Create<T>(T entity)
        {
            var response = new CommonResponse<T>();
            try
            {

                response.Data = await _commonRepository.Create<T>(entity);
                response.Result = new Result { ResultNumber = 0, ErrorMessage = string.Empty };
            }
            catch (Exception ex)
            {
                response.Result = new Result { ResultNumber = 1, ErrorMessage = ex.Message };
            }
            CleanCache();
            return response;
        }

        public async Task<CommonResponse<T>> Update<T>(T entity)
        {
            var response = new CommonResponse<T>();
            try
            {
                response.Data = await _commonRepository.Update<T>(entity);
                response.Result = new Result { ResultNumber = 0, ErrorMessage = string.Empty };
            }
            catch (Exception ex)
            {
                response.Result = new Result { ResultNumber = 1, ErrorMessage = ex.Message };
            }
            CleanCache();
            return response;
        }

        public async Task<CommonResponse<UserRegisterResponse>> Register(RegisterResource resource, CancellationToken cancellationToken)
        {
            var response = new CommonResponse<UserRegisterResponse>();
            try
            {
                var searchFilter = new UserFilters { Username = resource.Username, Email = resource.Email };
                var existingUser = await _userRepository.GetByFilters(searchFilter);
                if(existingUser != null && existingUser.Any())
                {
                    response.Result = new Result { ResultNumber = 1, ErrorMessage = "User Registered" };
                    return response;
                }
                var token = GetToken(resource.Username);
                var user = new UserDto()
                {
                    UserName = resource.Username,
                    Password = PasswordHasher.GenerateSalt(),
                    Email = resource.Email
                };
                user.PasswordHash = PasswordHasher.ComputeHash(resource.Password, user.Password, _pepper, _iteration);
                var userCreated = await _commonRepository.Create<UserDto>(user);
                if (userCreated == null)
                {
                    response.Result = new Result { ResultNumber = 1, ErrorMessage = "Error creating User" };
                    return response;
                }
                var result = new UserRegisterResponse
                {
                    UserName = userCreated.UserName,
                    Email = userCreated.Email,
                    Token = token,
                };
                response.Data = result;
                response.Result = new Result { ResultNumber = 0, ErrorMessage = string.Empty };

            }
            catch (Exception ex)
            {
                response.Result = new Result { ResultNumber = 1, ErrorMessage = ex.Message };
            }
            CleanCache();
            return response;
        }

        public async Task<CommonResponse<LoginResponse>> Login(LoginResource resource, CancellationToken cancellationToken)
        {
            var key = $"LOGIN_{resource.username}";
            try
            {
                var hasCache = _memoryCache.TryGetValue(key, out CommonResponse<LoginResponse> response);
                if (!hasCache || response.Data.RefreshTokenExpiryTime > DateTime.UtcNow)
                {
                    response = new CommonResponse<LoginResponse>(); 
                    var filter = new UserFilters() { Username = resource.username };
                    var users = await _userRepository.GetByFilters(filter);
                    var user = users.FirstOrDefault();
                    if (user == null)
                        throw new Exception("Username or password did not match.");

                    var passwordHash = PasswordHasher.ComputeHash(resource.password, user.Password, _pepper, _iteration);
                    if (user.PasswordHash != passwordHash)
                        throw new Exception("Username or password did not match.");

                    var token = GetToken(resource.username);
                    var loginResponse = new LoginResponse()
                    {
                        Token = token,
                        RefreshToken = GenerateRefreshToken(),
                        RefreshTokenExpiryTime = Expiration,
                        AgentId = user.Id.ToString()
                    };

                    response.Data = loginResponse;
                    response.Result = new Result { ResultNumber = 0, ErrorMessage = string.Empty };

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheTimeMinutes));
                    _memoryCache.Set(key, response, cacheEntryOptions);
                }

                return response;
            }
            catch (Exception ex)
            {
                return new CommonResponse<LoginResponse>
                {
                    Result = new Result { ResultNumber = 1, ErrorMessage = ex.Message }
                };
            }
        }

        private string GetToken(string username)
        {
            var issuer = "HubmaSoftAPI";
            var audience = "CRM";
            var keyBytes = Encoding.ASCII.GetBytes("asdfjasldkfhosafoihoqwjernlkqwer");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, username),
                            new Claim(JwtRegisteredClaimNames.Email, username),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        }),
                Expires = Expiration,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public void CleanCache()
        {
            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }

            _resetCacheToken = new CancellationTokenSource();
        }
    }
}
