namespace Domain.models;

public class SubClassDeleteRequestModel
{
    public int SubClassId {  get; set; }
}

public class SubClassDeleteResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}