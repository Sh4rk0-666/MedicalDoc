﻿using System;
using System.Collections.Generic;

namespace MedicalDoc.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Role { get; set; }
}
