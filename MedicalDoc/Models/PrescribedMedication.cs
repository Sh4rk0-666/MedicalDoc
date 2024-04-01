using System;
using System.Collections.Generic;

namespace MedicalDoc.Models;

public partial class PrescribedMedication
{
    public int IdMedication { get; set; }

    public int? IdPatient { get; set; }

    public string MedicationName { get; set; }

    public string Dosage { get; set; }

    public string Frequency { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string UsageInstructions { get; set; }

    public virtual Patient IdPatientNavigation { get; set; }
}
