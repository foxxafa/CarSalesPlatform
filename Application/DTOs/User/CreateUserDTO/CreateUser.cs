﻿namespace Application.DTOs.User.CreateUserDTO
{
    public class CreateUser
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
