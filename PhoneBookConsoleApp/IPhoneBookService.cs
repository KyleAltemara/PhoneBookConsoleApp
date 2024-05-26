namespace PhoneBookConsoleApp;

internal interface IPhoneBookService
{
    public Task<List<Contact>> GetContacts();
    public Task AddContact(Contact contact);
    public Task UpdateContact(Contact contact);
    public Task DeleteContact(Contact contact);
}
