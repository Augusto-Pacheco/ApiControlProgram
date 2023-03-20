using System.ComponentModel.DataAnnotations;

namespace ApiControlProgram.Dto
{
    public class UserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

    }

    public class UserCreateDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }

}
