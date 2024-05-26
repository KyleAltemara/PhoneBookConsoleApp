using Microsoft.EntityFrameworkCore;

namespace PhoneBookConsoleApp;

public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
}
