using System;
using System.Collections.Generic;

namespace MedisrceenCMS.Models.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Specialty { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<DailyCheckupReport> DailyCheckupReports { get; set; } = new List<DailyCheckupReport>();

    public virtual ICollection<DoctorShift> DoctorShifts { get; set; } = new List<DoctorShift>();
}
