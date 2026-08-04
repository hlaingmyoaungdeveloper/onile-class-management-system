namespace Domain.models;

public class SubClassEditRequestModel
{
    public int SubClassId {  get; set; }
}

public class SubClassEditResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string ClassName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateOnly OpenDate { get; set; }

    public int StudentLimit { get; set; }

    public int StudentCount { get; set; }

    public TimeOnly OpenTime { get; set; }

    public DateTime CreatedDateTime { get; set; }


    public DateTime ModifiedDateTime { get; set; }

}