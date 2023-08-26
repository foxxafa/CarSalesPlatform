namespace Application.DTOs.User.UpdateUser
{
    public class UpdateUserDTO
    {
        public string? UserId { get; set; }
        public string? NameSurname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CurrentPassword { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }
    }
}
