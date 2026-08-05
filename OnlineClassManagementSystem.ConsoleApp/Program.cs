using Domain.models;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

Console.WriteLine("Online Class Management System Console App Started.");

using var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5167/api/") };

while (true)
{
    Console.WriteLine("\n--- Onilne Class Management System Menu ---");
    Console.WriteLine("1. List SubClasses");
    Console.WriteLine("2. Get SubClass by ID");
    Console.WriteLine("3. Add new SubClass");
    Console.WriteLine("4. Edit SubClass");
    Console.WriteLine("5. Delete SubClass");
    Console.WriteLine("6. List Enrollments");
    Console.WriteLine("7. Get Enrollment by ID");
    Console.WriteLine("8. Add new Enrollment");
    Console.WriteLine("9. Exit");
    Console.Write("Enter your choice: ");
    
    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                var res1 = await httpClient.GetAsync("SubClass");
                var subClassesResponse = await res1.Content.ReadFromJsonAsync<SubClassListResponseModel>();
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
                break;

            case "2":
                Console.Write("Enter SubClass ID: ");
                if (int.TryParse(Console.ReadLine(), out int id2))
                {
                    var res2 = await httpClient.GetAsync($"SubClass/{id2}");
                    var response = await res2.Content.ReadFromJsonAsync<SubClassEditResponseModel>();
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
                break;

            case "3":
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
                var createRes = await httpClient.PostAsJsonAsync("SubClass", createReq);
                var createResponse = await createRes.Content.ReadFromJsonAsync<SubClassCreateResponseModel>();
                Console.WriteLine(createResponse?.Message);
                break;

            case "4":
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

                    var patchRes = await httpClient.PatchAsJsonAsync($"SubClass/{editId}", patchReq);
                    var patchResponse = await patchRes.Content.ReadFromJsonAsync<SubClassPatchResponseModel>();
                    Console.WriteLine(patchResponse?.Message);
                }
                else
                {
                    Console.WriteLine("Invalid ID.");
                }
                break;

            case "5":
                Console.Write("Enter SubClass ID to delete: ");
                if (int.TryParse(Console.ReadLine(), out int delId))
                {
                    var delRes = await httpClient.DeleteAsync($"SubClass/{delId}");
                    var delResponse = await delRes.Content.ReadFromJsonAsync<SubClassDeleteResponseModel>();
                    Console.WriteLine(delResponse?.Message);
                }
                else
                {
                    Console.WriteLine("Invalid ID.");
                }
                break;

            case "6":
                var res6 = await httpClient.GetAsync("Enrollment");
                var enrollmentsResponse = await res6.Content.ReadFromJsonAsync<EnrollmentListResponseModel>();
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
                break;

            case "7":
                Console.Write("Enter Enrollment ID: ");
                if (int.TryParse(Console.ReadLine(), out int enrId))
                {
                    var res7 = await httpClient.GetAsync($"Enrollment/{enrId}");
                    var response = await res7.Content.ReadFromJsonAsync<EnrollmentEditResponseModel>();
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
                break;

            case "8":
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
                
                var enrRes = await httpClient.PostAsJsonAsync("Enrollment", enrCreateReq);
                var enrCreateResponse = await enrRes.Content.ReadFromJsonAsync<EnrollmentCreateResponseModel>();
                
                Console.WriteLine(enrCreateResponse?.Message);
                break;

            case "9":
                Console.WriteLine("Exiting...");
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
