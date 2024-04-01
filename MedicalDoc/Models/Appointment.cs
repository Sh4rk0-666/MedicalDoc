using System;
using System.Collections.Generic;

namespace MedicalDoc.Models;

public partial class Appointment
{
    public int IdAppointment { get; set; }

    public int? IdPatient { get; set; }

    public DateTime? DateTime { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public string AdditionalNotes { get; set; }

    public virtual Patient IdPatientNavigation { get; set; }
}
