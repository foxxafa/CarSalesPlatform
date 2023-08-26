using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? ProfileImagePath { get; set; }
        public string NameSurname { get; set; }
        public string? Address { get; set; }    
        public string? Country { get; set; }  
        public string? City { get; set; }
        public DateTime? BirthDate { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndTime { get; set; }

        public ICollection<Car> Cars { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
