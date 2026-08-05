using System;
using System.Collections.Generic;

namespace OnlineClassManagementSystem.ConsoleApp.Models;

public class EnrollmentModel
{
    public int? SubClassId { get; set; }
    public string StudentName { get; set; } = null!;
    public string StudentContact { get; set; } = null!;
    public string PaymentInfo { get; set; } = null!;
    public string? FatherName { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime ModifiedDateTime { get; set; }
}

public class EnrollmentListResponseModel 
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
    public List<EnrollmentModel> Enrollments { get; set; } = new();
}

public class EnrollmentEditResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
    public int? SubClassId { get; set; }
    public string StudentName { get; set; } = null!;
    public string StudentContact { get; set; } = null!;
    public string PaymentInfo { get; set; } = null!;
    public string? FatherName { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime ModifiedDateTime { get; set; }
}

public class EnrollmentCreateRequestModel
{
    public int SubClassId { get; set; }
    public string StudentName { get; set; } = null!;
    public string StudentContact { get; set; } = null!;
    public string PaymentInfo { get; set; } = null!;
    public string? FatherName { get; set; }
}

public class EnrollmentCreateResponseModel
{ 
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
}
