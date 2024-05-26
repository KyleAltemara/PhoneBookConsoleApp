using Spectre.Console;

namespace PhoneBookConsoleApp;

public class PhoneBookMenu
{
    private readonly PhoneBookContext _context;

    public PhoneBookMenu(PhoneBookContext context)
    {
        _context = context;
    }

    public async Task ShowMenu()
    {
        // Create the database if it doesn't exist
        _context.Database.EnsureCreated();

        AnsiConsole.WriteLine("Phone Book Console App");
        var continueRunning = true;
        while (continueRunning)
        {
            var menuOptions = new Dictionary<string, Func<Task>>
                {
                    { "List Contacts", () => ListContacts() },
                    { "Add Contact", () =>  AddContact() },
                    { "Update Contact", () =>  UpdateContact() },
                    { "Delete Contact", () =>  DeleteContact() },
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
                AnsiConsole.Markup("[red]Invalid option.[/]");
            }
        }
    }

    private async Task DeleteContact()
    {
        throw new NotImplementedException();
    }

    private async Task UpdateContact()
    {
        throw new NotImplementedException();
    }

    private async Task ListContacts()
    {
        throw new NotImplementedException();
    }

    private static async Task AddContact()
    {
        throw new NotImplementedException();
    }
}
