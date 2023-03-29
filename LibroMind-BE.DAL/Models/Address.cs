using System;
using System.Collections.Generic;

namespace LibroMind_BE.DAL.Models;

public partial class Address
{
    public int Id { get; set; }

    public string Street { get; set; } = null!;

    public int Number { get; set; }

    public string? Building { get; set; }

    public int? Floor { get; set; }

    public int? Apartment { get; set; }

    public string City { get; set; } = null!;

    public string County { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string? Map { get; set; }

    public virtual ICollection<Library> Libraries { get; } = new List<Library>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
