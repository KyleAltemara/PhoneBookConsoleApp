using Microsoft.EntityFrameworkCore;
using PhoneBookConsoleApp.Models;

namespace PhoneBookConsoleApp;

/// <summary>
/// Represents the database context for the phone book.
/// </summary>
public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public PhoneBookContext(DbContextOptions<PhoneBookContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
