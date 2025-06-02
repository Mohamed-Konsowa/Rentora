using AutoMapper;
using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Account;
using Rentora.Application.Features.Account.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Account.Commands.Handlers
{
    public class AccountCommandHandler : IRequestHandler<RegisterCommand, Response<string>>
                                       , IRequestHandler<LoginCommand, Response<AuthModel>>
                                       , IRequestHandler<AddRoleCommand, Response<string>>
                                       , IRequestHandler<ResetPasswordCommand, Response<string>>
                                       , IRequestHandler<UpdateProfileCommand, Response<string>>
                                       , IRequestHandler<UpdateProfileImageCommand, Response<string>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.RegisterAsync(request);
            if (result.Item1) return ResponseHandler.Created("");
            
            var bad = ResponseHandler.BadRequest<string>();
            bad.Message = "There is an error!";
            bad.Errors = result.Item2;
            return bad;
        }

        public async Task<Response<AuthModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.LoginAsync(request);
            if (!result.IsAuthinticated) return ResponseHandler.NotFound<AuthModel>(result.Message);

            return ResponseHandler.Success(result);
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.AddRoleAsync(request);
            if (result.Item1) return ResponseHandler.Success(result.Item2);
            else return ResponseHandler.BadRequest<string>(result.Item2);
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByEmailAsync(request.Email);
            if (user == null) return ResponseHandler.NotFound<string>();

            var result = await _userService.ResetPasswordAsync(user, request.Token, request.NewPassword);
            if (result) return ResponseHandler.Success(Messages.Success);
            return ResponseHandler.BadRequest<string>();
        }

        public async Task<Response<string>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserById(request.Id.ToString());
            if(user is null) return ResponseHandler.NotFound<string>("User not found!");
            var result = await _userService.UpdateProfileAsync(request);
            if(result) return ResponseHandler.Success<string>("");
            return ResponseHandler.BadRequest<string>();
        }

        public async Task<Response<string>> Handle(UpdateProfileImageCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.UpdateProfileImageAsync(request);
            if(result) return ResponseHandler.Success(Messages.Success);
            return ResponseHandler.BadRequest<string>();
        }
    }
}
