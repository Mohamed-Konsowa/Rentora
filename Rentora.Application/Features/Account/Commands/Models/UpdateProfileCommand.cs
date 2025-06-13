using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Account.Commands.Models
{
    public class UpdateProfileCommand : IRequest<Response<string>>
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Personal_summary { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Governorate { get; set; }
        public string? Town { get; set; }
        public string? Address { get; set; }
    }
}
