using OCMS.Database.AppDbContextModels;

namespace Domain.models;

public class SubClassListRequestModel
{
}

public class SubClassListResponseModel
{
    public bool IsSuccess { get; set; }
    
    public string Message { get; set; }
    public List<SubClassModel> SubClasses { get; set; }
}

public class SubClassModel
{
    public string ClassName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateOnly OpenDate { get; set; }

    public int StudentLimit { get; set; }

    public int StudentCount { get; set; }

    public TimeOnly OpenTime { get; set; }

    public DateTime CreatedDateTime { get; set; }


    public DateTime ModifiedDateTime { get; set; }


}