using System;
using System.Collections.Generic;

namespace MedisrceenCMS.Models.Models;

public partial class PatientIllness
{
    public int IllnessId { get; set; }

    public int PatientId { get; set; }

    public string? IllnessName { get; set; }

    public DateOnly? DiagnosisDate { get; set; }

    public string? Treatment { get; set; }

    public virtual ICollection<DailyCheckupReport> DailyCheckupReports { get; set; } = new List<DailyCheckupReport>();

    public virtual Patient Patient { get; set; } = null!;
}
