using Domain.CQRS.Query.User;
using Domain.ModelsDto;
using Domain.Responses;
using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.User
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, CommonResponse<UserDto>>
    {
        private readonly ICommonService _commonService;

        public GetUserByIdHandler(ICommonService commonService)
        {
            _commonService = commonService;
        }

        public async Task<CommonResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _commonService.GetAsync<UserDto>(request.Id);
        }
    }
}
