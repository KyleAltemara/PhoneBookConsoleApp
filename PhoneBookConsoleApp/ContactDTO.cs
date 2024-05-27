using System.ComponentModel.DataAnnotations;

namespace PhoneBookConsoleApp
{
    public class ContactDTO
    {
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number") ]
        public string PhoneNumber { get; set; }

        internal ContactDTO Clone() => new()
        {
            Name = Name,
            Email = Email,
            PhoneNumber = PhoneNumber
        };
    }
}