using PhoneBookConsoleApp.Models;

namespace PhoneBookConsoleApp.Services;

/// <summary>
/// This service is responsible for managing phone book contacts.
/// </summary>
public interface IPhoneBookService
{
    /// <summary>
    /// Get all contacts from the phone book.
    /// </summary>
    /// <returns> A list of <see cref="ContactDTO"/>>. </returns>
    public Task<List<ContactDTO>> GetContacts();

    /// <summary>
    /// Add a new contact to the phone book.
    /// </summary>
    /// <param name="contact"> The contact to add. </param>
    /// <returns> A an asynchronous task. </returns>
    public Task AddContact(ContactDTO contact);

    /// <summary>
    /// Update an existing contact in the phone book.
    /// </summary>
    /// <param name="contact"> The contact to update. </param>
    /// <returns> A an asynchronous task. </returns>
    public Task UpdateContact(ContactDTO contact);

    /// <summary>
    /// Delete a contact from the phone book.
    /// </summary>
    /// <param name="contact"> The contact to delete. </param>
    /// <returns> A an asynchronous task. </returns>
    public Task DeleteContact(ContactDTO contact);
}
