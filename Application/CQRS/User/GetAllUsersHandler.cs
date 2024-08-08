using Domain.Abstractions.Providers;
using Domain.CQRS.Query.User;
using Domain.ModelsDto;
using Domain.Responses;
using Domain.SharedModels;
using MediatR;

namespace Application.CQRS.User
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, CommonResponse<IEnumerable<UserDto>>>
    {
        private readonly IEnumerable<ICommonRepository> _repository;

        public GetAllUsersHandler(IEnumerable<ICommonRepository>  repository)
        {
            _repository = repository;
        }

        public async Task<CommonResponse<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.First(x => x.Provider == Providers.User).GetAll<UserDto>();
            users = users.Where(x => x.Active == true).ToList();
            return new CommonResponse<IEnumerable<UserDto>>
            {
                Data = users,
                Result = new Result { ErrorMessage = string.Empty, ResultNumber = 0 }
            };
        }
    }

}
