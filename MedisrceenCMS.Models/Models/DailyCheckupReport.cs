using System;
using System.Collections.Generic;

namespace MedisrceenCMS.Models.Models;

public partial class DailyCheckupReport
{
    public int ReportId { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public int IllnessId { get; set; }

    public int NoteId { get; set; }

    public DateOnly? CheckupDate { get; set; }

    public string? CheckupDetails { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual PatientIllness Illness { get; set; } = null!;

    public virtual Note Note { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
