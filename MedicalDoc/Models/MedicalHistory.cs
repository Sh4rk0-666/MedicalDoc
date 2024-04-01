using System;
using System.Collections.Generic;

namespace MedicalDoc.Models;

public partial class MedicalHistory
{
    public int IdHistory { get; set; }

    public int? IdPatient { get; set; }

    public DateOnly? VisitDate { get; set; }

    public string Diagnosis { get; set; }

    public string Treatment { get; set; }

    public string DoctorNotes { get; set; }

    public string TestResults { get; set; }

    public virtual Patient IdPatientNavigation { get; set; }
}
