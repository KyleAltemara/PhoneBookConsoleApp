namespace PhoneBookConsoleApp.Models;

/// <summary>
/// Represents a contact in the phone book.
/// </summary>
public class Contact
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Converts the contact to a data transfer object.
    /// </summary>
    /// <returns> A <see cref="ContactDTO"/>. </returns>
    public ContactDTO ToDTO() => new()
    {
        Name = Name,
        Email = Email,
        PhoneNumber = PhoneNumber
    };
}
