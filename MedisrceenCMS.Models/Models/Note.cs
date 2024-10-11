using System;
using System.Collections.Generic;

namespace MedisrceenCMS.Models.Models;

public partial class Note
{
    public int NoteId { get; set; }

    public int PatientId { get; set; }

    public string? NoteText { get; set; }

    public DateOnly? NoteDate { get; set; }

    public virtual ICollection<DailyCheckupReport> DailyCheckupReports { get; set; } = new List<DailyCheckupReport>();

    public virtual Patient Patient { get; set; } = null!;
}
