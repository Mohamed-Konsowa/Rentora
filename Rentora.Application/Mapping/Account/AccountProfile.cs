using AutoMapper;
using Rentora.Application.DTOs.Account;
using Rentora.Application.Features.Account.Commands.Models;
using Rentora.Domain.Models;

namespace Rentora.Application.Mapping.Account
{
    public partial class AccountProfile : Profile
    {
        public AccountProfile()
        {
            GetAllUsersQueryMapping();
            RegisterCommandMapping();
        }
        public void GetAllUsersQueryMapping()
        {
            CreateMap<ApplicationUser, UserDTO>();
        }
        public void RegisterCommandMapping()
        {
            CreateMap<RegisterCommand, ApplicationUser>();
        }
    }
}
