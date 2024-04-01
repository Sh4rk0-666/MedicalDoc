using System;
using System.Collections.Generic;

namespace MedicalDoc.Models;

public partial class Healthinsurance
{
    public int InsuranceId { get; set; }

    public string InsuranceName { get; set; }

    public string PolicyNumber { get; set; }

    public string PolicyHolderFirstName { get; set; }

    public string PolicyHolderLastName { get; set; }

    public DateOnly PolicyHolderDateOfBirth { get; set; }

    public string PolicyHolderGender { get; set; }

    public string PolicyHolderAddress { get; set; }

    public string PolicyHolderPhoneNumber { get; set; }

    public string PolicyHolderEmail { get; set; }

    public DateOnly ValidFrom { get; set; }

    public DateOnly? ValidTo { get; set; }
}
