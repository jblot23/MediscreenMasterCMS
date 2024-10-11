using System;
using System.Collections.Generic;

namespace MedisrceenCMS.Models.Models;

public partial class DoctorShift
{
    public int ShiftId { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public DateOnly? ShiftDate { get; set; }

    public string? ShiftTime { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
