using AutoMapper;
using Rentora.Application.DTOs.Account;
using Rentora.Domain.Models;

namespace Rentora.Application.Mapping.Account
{
    public partial class AccountProfile : Profile
    {
        public AccountProfile()
        {
            GetAllUsersQueryMapping();
        }
        public void GetAllUsersQueryMapping()
        {
            CreateMap<ApplicationUser, UserDTO>();
        }
    }
}
