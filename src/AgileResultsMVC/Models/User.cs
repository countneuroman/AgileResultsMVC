﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AgileResultsMVC.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<AllTask> AllTask { get; set; }
}