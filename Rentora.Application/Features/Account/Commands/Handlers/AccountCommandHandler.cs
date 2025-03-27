using AutoMapper;
using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Account;
using Rentora.Application.Features.Account.Commands.Models;
using Rentora.Application.IServices;
using Rentora.Domain.Models;

namespace Rentora.Application.Features.Account.Commands.Handlers
{
    public class AccountCommandHandler : ResponseHandler
                                       , IRequestHandler<RegisterCommand, Response<string>>
                                       , IRequestHandler<LoginCommand, Response<AuthModel>>
                                       , IRequestHandler<AddRoleCommand, Response<string>>
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
            if (result.Item1) return Created("");
            
            var bad = BadRequest<string>();
            bad.Message = "There is an error!";
            bad.Errors = result.Item2;
            return bad;
        }

        public async Task<Response<AuthModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.GetTokenAsync(request);
            if (!result.IsAuthinticated) return NotFound<AuthModel>(result.Message);

            return Success(result);
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.AddRoleAsync(request);
            if (result.Item1) return Success(result.Item2);
            else return BadRequest<string>(result.Item2);
        }
    }
}
