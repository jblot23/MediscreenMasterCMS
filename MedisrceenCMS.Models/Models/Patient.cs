using System;
using System.Collections.Generic;

namespace MedisrceenCMS.Models.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<DailyCheckupReport> DailyCheckupReports { get; set; } = new List<DailyCheckupReport>();

    public virtual ICollection<DoctorShift> DoctorShifts { get; set; } = new List<DoctorShift>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<PatientIllness> PatientIllnesses { get; set; } = new List<PatientIllness>();
}
