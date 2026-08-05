using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OnlineClassManagementSystem.ConsoleApp.Models;

namespace OnlineClassManagementSystem.ConsoleApp.Features;

public class EnrollmentFeature
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public EnrollmentFeature(HttpClient client, string baseUrl)
    {
        _client = client;
        _baseUrl = baseUrl;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n--- Enrollment Management ---");
            Console.WriteLine("1. List Enrollments");
            Console.WriteLine("2. Get Enrollment by ID");
            Console.WriteLine("3. Add new Enrollment");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Enter your choice: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await ListEnrollments();
                        break;
                    case "2":
                        await GetEnrollment();
                        break;
                    case "3":
                        await AddEnrollment();
                        break;
                    case "4":
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

    private async Task ListEnrollments()
    {
        var res = await _client.GetAsync($"{_baseUrl}/Enrollment");
        var enrollmentsResponse = await res.Content.ReadFromJsonAsync<EnrollmentListResponseModel>();
        if (enrollmentsResponse != null && enrollmentsResponse.IsSuccess)
        {
            Console.WriteLine($"Found {enrollmentsResponse.Enrollments?.Count ?? 0} enrollments.");
            var enrolls = enrollmentsResponse.Enrollments ?? new System.Collections.Generic.List<EnrollmentModel>();
            int index = 1;
            foreach (var enrollment in enrolls)
            {
                Console.WriteLine($"{index++}. SubClassId: {enrollment.SubClassId} | Student: {enrollment.StudentName} | Contact: {enrollment.StudentContact} | Payment: {enrollment.PaymentInfo} | Father Name: {enrollment.FatherName}");
            }
        }
        else
        {
            Console.WriteLine($"Error fetching enrollments: {enrollmentsResponse?.Message}");
        }
    }

    private async Task GetEnrollment()
    {
        Console.Write("Enter Enrollment ID: ");
        if (int.TryParse(Console.ReadLine(), out int enrId))
        {
            var res = await _client.GetAsync($"{_baseUrl}/Enrollment/{enrId}");
            var response = await res.Content.ReadFromJsonAsync<EnrollmentEditResponseModel>();
            if (response != null && response.IsSuccess)
            {
                Console.WriteLine($"Student: {response.StudentName} | Contact: {response.StudentContact} | SubClassId: {response.SubClassId} | Payment: {response.PaymentInfo} | Father Name: {response.FatherName}");
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

    private async Task AddEnrollment()
    {
        Console.Write("SubClass ID: ");
        int.TryParse(Console.ReadLine(), out int subId);
        Console.Write("Student Name: ");
        var studentName = Console.ReadLine();
        Console.Write("Student Contact: ");
        var contact = Console.ReadLine();
        Console.Write("Payment Info: ");
        var payment = Console.ReadLine();
        Console.Write("Father Name: ");
        var fatherName = Console.ReadLine();
        
        var enrCreateReq = new EnrollmentCreateRequestModel
        {
            SubClassId = subId,
            StudentName = studentName ?? "",
            StudentContact = contact ?? "",
            PaymentInfo = payment ?? "",
            FatherName = fatherName
        };
        
        var enrRes = await _client.PostAsJsonAsync($"{_baseUrl}/Enrollment", enrCreateReq);
        var enrCreateResponse = await enrRes.Content.ReadFromJsonAsync<EnrollmentCreateResponseModel>();
        
        Console.WriteLine(enrCreateResponse?.Message);
    }
}
