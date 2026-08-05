using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OnlineClassManagementSystem.ConsoleApp.Models;

namespace OnlineClassManagementSystem.ConsoleApp.Features;

public class SubClassFeature
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public SubClassFeature(HttpClient client, string baseUrl)
    {
        _client = client;
        _baseUrl = baseUrl;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n--- SubClass Management ---");
            Console.WriteLine("1. List SubClasses");
            Console.WriteLine("2. Get SubClass by ID");
            Console.WriteLine("3. Add new SubClass");
            Console.WriteLine("4. Edit SubClass");
            Console.WriteLine("5. Delete SubClass");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Enter your choice: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await ListSubClasses();
                        break;
                    case "2":
                        await GetSubClass();
                        break;
                    case "3":
                        await AddSubClass();
                        break;
                    case "4":
                        await EditSubClass();
                        break;
                    case "5":
                        await DeleteSubClass();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    private async Task ListSubClasses()
    {
        var res = await _client.GetAsync($"{_baseUrl}/SubClass");
        var subClassesResponse = await res.Content.ReadFromJsonAsync<SubClassListResponseModel>();
        if (subClassesResponse != null && subClassesResponse.IsSuccess)
        {
            Console.WriteLine($"Found {subClassesResponse.SubClasses?.Count ?? 0} subclasses.");
            var classes = subClassesResponse.SubClasses ?? new System.Collections.Generic.List<SubClassModel>();
            int index = 1;
            foreach (var subClass in classes)
            {
                Console.WriteLine($"{index++}. {subClass.ClassName} | Location: {subClass.Location} | Date: {subClass.OpenDate.ToString("dd-MM-yyyy")} | Time: {subClass.OpenTime} | Limit: {subClass.StudentLimit} | Enrolled: {subClass.StudentCount}");
            }
        }
        else
        {
            Console.WriteLine($"Error fetching subclasses: {subClassesResponse?.Message}");
        }
    }

    private async Task GetSubClass()
    {
        Console.Write("Enter SubClass ID: ");
        if (int.TryParse(Console.ReadLine(), out int id2))
        {
            var res = await _client.GetAsync($"{_baseUrl}/SubClass/{id2}");
            var response = await res.Content.ReadFromJsonAsync<SubClassEditResponseModel>();
            if (response != null && response.IsSuccess)
            {
                Console.WriteLine($"SubClass: {response.ClassName} | Location: {response.Location} | Date: {response.OpenDate.ToString("dd-MM-yyyy")} | Time: {response.OpenTime} | Limit: {response.StudentLimit} | Enrolled: {response.StudentCount}");
            }
            else
            {
                Console.WriteLine(response?.Message ?? "Error.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private async Task AddSubClass()
    {
        Console.Write("Class Name: ");
        var className = Console.ReadLine();
        Console.Write("Location: ");
        var location = Console.ReadLine();
        Console.Write("Student Limit: ");
        int.TryParse(Console.ReadLine(), out int limit);
        Console.Write("Open Date (yyyy-mm-dd): ");
        DateOnly.TryParse(Console.ReadLine(), out DateOnly openDate);
        Console.Write("Open Time (hh:mm): ");
        TimeOnly.TryParse(Console.ReadLine(), out TimeOnly openTime);
        
        var createReq = new SubClassCreateRequestModel
        {
            ClassName = className ?? "",
            Location = location ?? "",
            StudentLimit = limit,
            OpenDate = openDate,
            OpenTime = openTime
        };
        var createRes = await _client.PostAsJsonAsync($"{_baseUrl}/SubClass", createReq);
        var createResponse = await createRes.Content.ReadFromJsonAsync<SubClassCreateResponseModel>();
        Console.WriteLine(createResponse?.Message);
    }

    private async Task EditSubClass()
    {
        Console.Write("Enter SubClass ID to edit: ");
        if (int.TryParse(Console.ReadLine(), out int editId))
        {
            Console.Write("New Class Name (leave empty to keep current): ");
            var newClassName = Console.ReadLine();
            Console.Write("New Location (leave empty to keep current): ");
            var newLocation = Console.ReadLine();
            Console.Write("New Student Limit (enter 0 to keep current): ");
            int.TryParse(Console.ReadLine(), out int newLimit);
            Console.Write("New Open Date (yyyy-mm-dd, leave empty to keep current): ");
            var dateInput = Console.ReadLine();
            DateOnly? newDate = string.IsNullOrWhiteSpace(dateInput) ? null : DateOnly.Parse(dateInput);
            Console.Write("New Open Time (hh:mm, leave empty to keep current): ");
            var timeInput = Console.ReadLine();
            TimeOnly? newTime = string.IsNullOrWhiteSpace(timeInput) ? null : TimeOnly.Parse(timeInput);

            var patchReq = new SubClassPatchRequestModel
            {
                ClassName = newClassName ?? "",
                Location = newLocation ?? ""
            };
            if (newLimit != 0) patchReq.StudentLimit = newLimit;
            if (newDate.HasValue) patchReq.OpenDate = newDate.Value;
            if (newTime.HasValue) patchReq.OpenTime = newTime.Value;

            var patchRes = await _client.PatchAsJsonAsync($"{_baseUrl}/SubClass/{editId}", patchReq);
            var patchResponse = await patchRes.Content.ReadFromJsonAsync<SubClassPatchResponseModel>();
            Console.WriteLine(patchResponse?.Message);
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private async Task DeleteSubClass()
    {
        Console.Write("Enter SubClass ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int delId))
        {
            var delRes = await _client.DeleteAsync($"{_baseUrl}/SubClass/{delId}");
            var delResponse = await delRes.Content.ReadFromJsonAsync<SubClassDeleteResponseModel>();
            Console.WriteLine(delResponse?.Message);
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }
}
