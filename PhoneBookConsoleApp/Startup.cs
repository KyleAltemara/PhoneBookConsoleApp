using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace PhoneBookConsoleApp;

internal static class Startup
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection()
        // Add a DbContext to the service collection
        .AddDbContext<PhoneBookContext>(options =>
        {
            // Retrieve the connection string from the configuration file
            var connectionString = ConfigurationManager.ConnectionStrings["PhoneBookDbContext"].ConnectionString;

            // Configure the DbContext to use SQL Server with the retrieved connection string
            options.UseSqlServer(connectionString);
        })
        .AddScoped<IPhoneBookService, PhoneBookService>()
        .AddSingleton<PhoneBookMenu>();
        return services.BuildServiceProvider();
    }
}
