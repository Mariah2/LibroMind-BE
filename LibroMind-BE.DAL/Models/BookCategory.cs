using System;
using System.Collections.Generic;

namespace LibroMind_BE.DAL.Models;

public partial class BookCategory
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public int CategoryId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;
}
