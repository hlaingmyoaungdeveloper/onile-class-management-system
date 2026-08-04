namespace Domain.models;

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
    public string Message { get; set; }
}
