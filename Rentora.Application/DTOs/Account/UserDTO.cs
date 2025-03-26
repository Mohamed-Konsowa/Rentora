using Rentora.Domain.Models;

namespace Rentora.Application.DTOs.Account
{
    public class UserDTO
    {
        public UserDTO()
        {
            
        }
        public UserDTO(ApplicationUser u)
        {
            Id = u.Id;
            FirstName = u.FirstName;
            LastName = u.LastName;
            Username = u.UserName;
            Email = u.Email;
            EmailConfirmed = u.EmailConfirmed;
            NationalID = u.NationalID;
            Personal_summary = u.Personal_summary;
            PhoneNumber = u.PhoneNumber;
            Governorate = u.Governorate;
            Town = u.Governorate;
            Address = u.Address;
            ProfileImage = u.ProfileImage;
        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool   EmailConfirmed { get; set; }
        public string NationalID { get; set; }
        public string Personal_summary { get; set; }
        public string PhoneNumber { get; set; }
        public string Governorate { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public string ProfileImage { get; set; }
    }
}
