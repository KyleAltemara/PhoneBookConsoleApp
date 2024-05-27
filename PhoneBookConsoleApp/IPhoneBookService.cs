namespace PhoneBookConsoleApp;

public interface IPhoneBookService
{
    public Task<List<ContactDTO>> GetContacts();
    public Task AddContact(ContactDTO contact);
    public Task UpdateContact(ContactDTO contact);
    public Task DeleteContact(ContactDTO contact);
}
