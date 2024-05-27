using Microsoft.EntityFrameworkCore;
using PhoneBookConsoleApp.Models;

namespace PhoneBookConsoleApp.Services;

internal class PhoneBookService(PhoneBookContext context) : IPhoneBookService
{
    /// <summary>
    /// The database context for the phone book.
    /// </summary>
    private readonly PhoneBookContext _context = context;

    public async Task<List<ContactDTO>> GetContacts() => await _context.Contacts.Select(c => c.ToDTO()).ToListAsync();

    public async Task AddContact(ContactDTO contact)
    {
        var newContact = new Contact
        {
            Name = contact.Name!,
            Email = contact.Email!,
            PhoneNumber = contact.PhoneNumber!
        };

        _context.Contacts.Add(newContact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContact(ContactDTO contact)
    {
        var contactToDelete = await _context.Contacts.FirstOrDefaultAsync(c => c.Name == contact.Name);
        if (contactToDelete != null)
        {
            _context.Contacts.Remove(contactToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateContact(ContactDTO contact)
    {
        var contactToUpdate = await _context.Contacts.FirstOrDefaultAsync(c => c.Name == contact.Name);
        if (contactToUpdate != null)
        {
            contactToUpdate.Email = contact.Email!;
            contactToUpdate.PhoneNumber = contact.PhoneNumber!;
            await _context.SaveChangesAsync();
        }
    }
}
