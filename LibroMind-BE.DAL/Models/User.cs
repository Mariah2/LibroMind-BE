using System;
using System.Collections.Generic;

namespace LibroMind_BE.DAL.Models;

public partial class User
{
    public int Id { get; set; }

    public int? AddressId { get; set; }

    public int RoleId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public virtual Address? Address { get; set; }

    public virtual ICollection<Borrow> Borrows { get; } = new List<Borrow>();

    public virtual Role Role { get; set; } = null!;
}
