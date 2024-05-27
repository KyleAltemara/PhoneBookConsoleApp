using System.ComponentModel.DataAnnotations;

namespace PhoneBookConsoleApp;

public class Contact
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    public ContactDTO ToDTO() => new()
    {
        Name = Name,
        Email = Email,
        PhoneNumber = PhoneNumber
    };
}
