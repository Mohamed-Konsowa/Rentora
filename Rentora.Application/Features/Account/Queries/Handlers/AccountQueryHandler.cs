using AutoMapper;
using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Account;
using Rentora.Application.Features.Account.Queries.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Account.Queries.Handlers
{
    public class AccountQueryHandler : IRequestHandler<GetAllUsersQuery, Response<List<UserDTO>>>
                                     , IRequestHandler<GetUserByIdQuery, Response<UserDTO>>
                                     , IRequestHandler<CheckIfEmailExistsQuery, Response<bool>>
                                     , IRequestHandler<CheckIfUserNameExistsQuery, Response<bool>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<Response<List<UserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllUsers();
            var usersMapper = _mapper.Map<List<UserDTO>>(users);
            return ResponseHandler.Success(usersMapper);
        }

        public async Task<Response<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserById(request.UserId);
            if (user == null) return ResponseHandler.NotFound<UserDTO>();
            var userMapper = _mapper.Map<UserDTO>(user);
            return ResponseHandler.Success(userMapper);
        }

        public async Task<Response<bool>> Handle(CheckIfEmailExistsQuery request, CancellationToken cancellationToken)
        {
            var result = await _userService.CheckIfEmailExists(request.Email);
            return ResponseHandler.Success(result);
        }

        public async Task<Response<bool>> Handle(CheckIfUserNameExistsQuery request, CancellationToken cancellationToken)
        {
            var result = await _userService.CheckIfUserNameExists(request.UserName);
            return ResponseHandler.Success(result);
        }
    }
}
