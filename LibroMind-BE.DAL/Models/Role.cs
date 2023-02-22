﻿using System;
using System.Collections.Generic;

namespace LibroMind_BE.DAL.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
