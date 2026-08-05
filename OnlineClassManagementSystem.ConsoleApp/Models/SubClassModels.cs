using System;
using System.Collections.Generic;

namespace OnlineClassManagementSystem.ConsoleApp.Models;

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

public class SubClassListResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
    public List<SubClassModel> SubClasses { get; set; } = new();
}

public class SubClassEditResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
    public string ClassName { get; set; } = null!;
    public string Location { get; set; } = null!;
    public DateOnly OpenDate { get; set; }
    public int StudentLimit { get; set; }
    public int StudentCount { get; set; }
    public TimeOnly OpenTime { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime ModifiedDateTime { get; set; }
}

public class SubClassCreateRequestModel
{
    public string ClassName { get; set; } = null!;
    public string Location { get; set; } = null!;
    public int StudentLimit { get; set; }
    public DateOnly OpenDate { get; set; }
    public TimeOnly OpenTime { get; set; }
}

public class SubClassPatchRequestModel
{
    public string? ClassName { get; set; }
    public string? Location { get; set; }
    public int? StudentLimit { get; set; }
    public DateOnly? OpenDate { get; set; }
    public TimeOnly? OpenTime { get; set; }
}

public class SubClassCreateResponseModel 
{ 
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
}
public class SubClassPatchResponseModel 
{ 
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
}
public class SubClassDeleteResponseModel 
{ 
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
}
