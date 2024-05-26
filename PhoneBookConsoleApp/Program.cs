using Microsoft.Extensions.DependencyInjection;

namespace PhoneBookConsoleApp;

internal class Program
{
    static void Main()
    {
        // Create a service collection and configure our services
        var serviceProvider = Startup.ConfigureServices();
        // Get an instance of our menu
        var menu = serviceProvider.GetRequiredService<PhoneBookMenu>();
        // Run the menu
        Task.Run(menu.ShowMenu).GetAwaiter().GetResult();
    }
}
