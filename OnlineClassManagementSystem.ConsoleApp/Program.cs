using System;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineClassManagementSystem.ConsoleApp.Features;

namespace OnlineClassManagementSystem.ConsoleApp;

class Program
{
    static async Task Main(string[] args)
    {
        // Change the port if your WebApi runs on a different one
        string baseUrl = "http://localhost:5167/api"; 
        
        using HttpClient client = new HttpClient();
        
        var subClassFeature = new SubClassFeature(client, baseUrl);
        var enrollmentFeature = new EnrollmentFeature(client, baseUrl);

    MainMenu:
        Console.WriteLine("\n==================================");
        Console.WriteLine("   Online Class Management System");
        Console.WriteLine("==================================");
        Console.WriteLine("1. Manage SubClasses");
        Console.WriteLine("2. Manage Enrollments");
        Console.WriteLine("3. Exit Program");
        Console.Write("Choose a section: ");
        
        string? choiceStr = Console.ReadLine();
        if (!int.TryParse(choiceStr, out int choice)) choice = 0;

        switch (choice)
        {
            case 1:
                await subClassFeature.RunAsync();
                break;
            case 2:
                await enrollmentFeature.RunAsync();
                break;
            case 3:
                goto Exit;
            default:
                Console.WriteLine("Invalid Choice. Try again.");
                break;
        }

        goto MainMenu;

    Exit:
        Console.WriteLine("Exiting the program. Goodbye!");
    }
}
