using System;
using System.Collections.Generic;

namespace LibroMind_BE.DAL.Models;

public partial class Library
{
    public int Id { get; set; }

    public int AddressId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<BookLibrary> BookLibraries { get; } = new List<BookLibrary>();
}
