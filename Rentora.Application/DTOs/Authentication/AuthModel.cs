namespace Rentora.Application.DTOs.Authentication
{
    public class AuthModel
    {
        public string Id { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        public bool IsAuthinticated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }
        public string ProfileImage { get; set; }
    }
}
