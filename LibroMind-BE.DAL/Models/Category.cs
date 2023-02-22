using System;
using System.Collections.Generic;

namespace LibroMind_BE.DAL.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BookCategory> BookCategories { get; } = new List<BookCategory>();
}
