namespace Domain.models;

public class SubClassCreateRequestModel
{
    public string ClassName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateOnly OpenDate { get; set; }

    public int StudentLimit { get; set; }

    public TimeOnly OpenTime { get; set; }


}

public class SubClassCreateResponseModel
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; }

}