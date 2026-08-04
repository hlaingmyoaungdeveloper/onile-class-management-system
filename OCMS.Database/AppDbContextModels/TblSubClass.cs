using System;
using System.Collections.Generic;

namespace OCMS.Database.AppDbContextModels;

public partial class TblSubClass
{
    public int SubClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateOnly OpenDate { get; set; }

    public int StudentLimit { get; set; }

    public TimeOnly OpenTime { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime ModifiedDateTime { get; set; }

    public bool IsDelete { get; set; }

    public int StudentCount { get; set; }

    public virtual ICollection<TblEnrollment> TblEnrollments { get; set; } = new List<TblEnrollment>();
}
