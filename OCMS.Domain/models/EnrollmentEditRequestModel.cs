namespace Domain.models;

public class EnrollmentEditRequestModel
{
    public int EnrollmentId { get; set; }
}

public class EnrollmentEditResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public int? SubClassId { get; set; }
    public string StudentName { get; set; } = null!;
    public string StudentContact { get; set; } = null!;
    public string PaymentInfo { get; set; } = null!;
    public string? FatherName { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime ModifiedDateTime { get; set; }
}
