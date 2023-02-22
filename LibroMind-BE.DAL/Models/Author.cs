using System;
using System.Collections.Generic;

namespace LibroMind_BE.DAL.Models;

public partial class Author
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string? Nationality { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
