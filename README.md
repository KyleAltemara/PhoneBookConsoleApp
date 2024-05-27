# PhoneBookConsoleApp
   
This is a console application that allows users to manage a phone book. The application uses a SQLite database to store and retrieve contact data. Users can insert, delete, update, and view their contacts.
<https://www.thecsharpacademy.com/project/16/phonebook>

## Features

- When the application starts, it will create a SQLite database if one doesn't exist and create tables to store contact data.
- Contact Management: Users can add contacts to the phone book, update contacts, and delete contacts. This is managed by the PhoneBookService class in 'PhoneBookService.cs'.
- Validation: The application validates email addresses contain an "@" symbol and phone numbers contain only digits.
- Termination: The application continues to run until the user chooses the "Exit" option.

## Getting Started

To run the application, follow these steps:

1.  Clone the repository to your local machine.
2.  Open the solution in Visual Studio.
3.  The user has to configure the app.config file with the appropiate connection string for SQL Sever.
4.  Build the solution to restore NuGet packages and compile the code.
5.  Run the application.

## Dependencies

- Microsoft.EntityFrameworkCore: The application uses this package to manage the database context and entity relationships.
- Spectre.Console: The application uses this package to create a user-friendly console interface.
- System.Configuration.ConfigurationManager: The application uses this package to read the connection string from the app.config file.

## Usage

1.  The application will display a menu with options to manage contacts or exit the application.
2.  Select an option by using the arrow keys and press Enter.
3.  Follow the prompts to perform the desired action.
4.  The application will continue to run until you choose the "Exit" option.

## License

This project is licensed under the MIT License.

## Resources Used

- [The C# Academy](https://www.thecsharpacademy.com/)
- GitHub Copilot to generate code snippets.
