namespace Domain.models;

public class SubClassPatchRequestModel
{
    public string? ClassName { get; set; }

    public string? Location { get; set; } 

    public DateOnly OpenDate { get; set; }

    public int StudentLimit { get; set; }

    public TimeOnly OpenTime { get; set; }

}

public class SubClassPatchResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}