using PhoneBookConsoleApp.Models;
using PhoneBookConsoleApp.Services;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;

namespace PhoneBookConsoleApp;

/// <summary>
/// Represents the phone book console menu.
/// </summary>
/// <param name="phoneBookService"> The phone book service. <see cref="IPhoneBookService"/> </param>
public class PhoneBookMenu(IPhoneBookService phoneBookService)
{
    /// <summary>
    /// The phone book service used to interact with the phone book database.
    /// </summary>
    private readonly IPhoneBookService _phoneBookService = phoneBookService;

    /// <summary>
    /// Shows the phone book console menu.
    /// </summary>
    /// <returns> An asynchronous task. </returns>
    public async Task ShowMenu()
    {
        AnsiConsole.WriteLine("Phone Book Console App");
        var continueRunning = true;
        while (continueRunning)
        {
            var menuOptions = new Dictionary<string, Func<Task>>
                {
                    { "List Contacts", ListContacts },
                    { "Add Contact", AddContact },
                    { "Update Contact", UpdateContact },
                    { "Delete Contact", DeleteContact },
                    { "Exit", () =>
                        {
                            AnsiConsole.Markup("[red]Exiting Program[/]");
                            continueRunning = false;
                            return Task.CompletedTask;
                        }
                    }
                };

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an option:")
                    .AddChoices(menuOptions.Keys));

            if (menuOptions.TryGetValue(choice, out var selectedAction))
            {
                AnsiConsole.Clear();
                await selectedAction.Invoke();
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid option.[/]");
            }
        }
    }

    /// <summary>
    /// Lists all contacts in the phone book.
    /// </summary>
    /// <returns> An asynchronous task. </returns>
    private async Task ListContacts()
    {
        var contacts = await _phoneBookService.GetContacts();
        if (contacts is null || contacts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No contacts found.[/]");
            return;
        }

        var table = new Table();
        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("Phone Number");

        foreach (var contact in contacts)
        {
            table.AddRow(contact.Name!, contact.Email!, contact.PhoneNumber!);
        }

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Adds a new contact to the phone book.
    /// </summary>
    /// <returns> An asynchronous task. </returns>
    private async Task AddContact()
    {
        var contact = new ContactDTO
        {
            Name = AnsiConsole.Ask<string>("Enter contact name:").Trim()
        };

        if (string.IsNullOrWhiteSpace(contact.Name))
        {
            AnsiConsole.MarkupLine("[red]No contace added.[/]");
            return;
        }

        contact.Email = AnsiConsole.Ask<string>("Enter contact email:").Trim();
        if (string.IsNullOrWhiteSpace(contact.Email))
        {
            AnsiConsole.MarkupLine("[red]No contace added.[/]");
            return;
        }

        while (!new EmailAddressAttribute().IsValid(contact.Email))
        {
            AnsiConsole.Markup("[red]Invalid email address.[/]");
            contact.Email = AnsiConsole.Ask<string>("Enter contact email:").Trim();
            if (string.IsNullOrWhiteSpace(contact.Email))
            {
                AnsiConsole.MarkupLine("[red]No contace added.[/]");
                return;
            }
        }

        contact.PhoneNumber = AnsiConsole.Ask<string>("Enter contact phone number:").Trim();
        if (string.IsNullOrWhiteSpace(contact.PhoneNumber))
        {
            AnsiConsole.MarkupLine("[red]No contace added.[/]");
            return;
        }

        while (!new PhoneAttribute().IsValid(contact.PhoneNumber))
        {
            AnsiConsole.MarkupLine("[red]Invalid phone number.[/]");
            contact.PhoneNumber = AnsiConsole.Ask<string>("Enter contact phone number:").Trim()
                ;
            if (string.IsNullOrWhiteSpace(contact.PhoneNumber))
            {
                AnsiConsole.MarkupLine("[red]No contace added.[/]");
                return;
            }
        }

        await _phoneBookService.AddContact(contact);
        AnsiConsole.MarkupLine("[green]Contact added successfully.[/]");
    }

    /// <summary>
    /// Updates an existing contact in the phone book.
    /// </summary>
    /// <returns> An asynchronous task. </returns>
    private async Task UpdateContact()
    {
        var contacts = await _phoneBookService.GetContacts();
        if (contacts is null || contacts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No contacts found.[/]");
            return;
        }

        var contactDictionary = contacts.ToDictionary(c => c.Name!, c => c);
        contactDictionary.Add("Cancel", new());
        var contactName = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose a contact to update:")
            .PageSize(25)
            .AddChoices(contactDictionary.Keys));
        if (contactName == "Cancel")
        {
            return;
        }

        var contact = contactDictionary[contactName];
        var newContact = contact.Clone();
        if (AnsiConsole.Confirm("Do you want to update the contact name?", false))
        {
            newContact.Name = AnsiConsole.Ask<string>("Enter contact name:").Trim();
        }

        if (AnsiConsole.Confirm("Do you want to update the contact email?", false))
        {
            newContact.Email = AnsiConsole.Ask<string>("Enter contact email:").Trim();
            while (!new EmailAddressAttribute().IsValid(contact.Email))
            {
                AnsiConsole.MarkupLine("[red]Invalid email address.[/]");
                newContact.Email = AnsiConsole.Ask<string>("Enter contact email:").Trim();
                if (string.IsNullOrWhiteSpace(contact.Email) &&
                    AnsiConsole.Confirm("Cancle updating email?", true))
                {
                    newContact.Email = contact.Email;
                    AnsiConsole.MarkupLine("[red]Email not updated.[/]");
                    break;
                }
            }
        }

        if (AnsiConsole.Confirm("Do you want to update the contact phone number?", false))
        {
            newContact.PhoneNumber = AnsiConsole.Ask<string>("Enter contact phone number:").Trim();
            while (!new PhoneAttribute().IsValid(contact.PhoneNumber))
            {
                AnsiConsole.MarkupLine("[red]Invalid phone number.[/]");
                newContact.PhoneNumber = AnsiConsole.Ask<string>("Enter contact phone number:").Trim();
                if (string.IsNullOrWhiteSpace(contact.PhoneNumber) &&
                                       AnsiConsole.Confirm("Cancle updating phone number?", true))
                {
                    newContact.PhoneNumber = contact.PhoneNumber;
                    AnsiConsole.MarkupLine("[red]Phone number not updated.[/]");
                    break;
                }
            }
        }

        if (newContact.Name == contact.Name && newContact.Email == contact.Email && newContact.PhoneNumber == contact.PhoneNumber)
        {
            AnsiConsole.MarkupLine("[red]No changes made to contact.[/]");
            return;
        }

        await _phoneBookService.UpdateContact(newContact);
        AnsiConsole.MarkupLine("[green]Contact updated successfully.[/]");
    }

    /// <summary>
    /// Deletes a contact from the phone book.
    /// </summary>
    /// <returns> An asynchronous task. </returns>
    private async Task DeleteContact()
    {
        var contacts = await _phoneBookService.GetContacts();
        if (contacts is null || contacts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No contacts found.[/]");
            return;
        }

        var contactDictionary = contacts.ToDictionary(c => c.Name!, c => c);
        contactDictionary.Add("Cancel", new());
        var contactName = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose a contact to delete:")
            .PageSize(25)
            .AddChoices(contactDictionary.Keys));
        if (contactName == "Cancel")
        {
            return;
        }

        var contact = contactDictionary[contactName];
        await _phoneBookService.DeleteContact(contact);
    }
}
