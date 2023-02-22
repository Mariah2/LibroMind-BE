using System;
using System.Collections.Generic;

namespace LibroMind_BE.DAL.Models;

public partial class Publisher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
