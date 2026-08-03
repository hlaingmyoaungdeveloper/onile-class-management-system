using June2026.OCMSDatabase.AppDbContextModels;

namespace Domain.models;

public class EnrollmentListRequestModel
{
}

public class EnrollmentListResponseModel
{
    public bool IsSuccess { get; set; }
    
    public string Message { get; set; }
    public List<EnrollmentModel> Enrollments { get; set; }
}

public class EnrollmentModel
{
    public int? SubClassId { get; set; }
    public string StudentName { get; set; } = null!;
    public string StudentContact { get; set; } = null!;
    public string PaymentInfo { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime CreatedDateTime { get; set; }
    public DateTime ModifiedDateTime { get; set; }
}
