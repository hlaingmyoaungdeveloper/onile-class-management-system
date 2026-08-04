using System;
using System.Collections.Generic;

namespace OnlineClassManagementSystem.Database.AppDbContextModels;

public partial class TblEnrollment
{
    public int EnrollmentId { get; set; }

    public int? SubClassId { get; set; }

    public string StudentName { get; set; } = null!;

    public string StudentContact { get; set; } = null!;

    public string PaymentInfo { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public DateTime ModifiedDateTime { get; set; }

    public string? FatherName { get; set; }

    public virtual TblSubClass? SubClass { get; set; }
}
