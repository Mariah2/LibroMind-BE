using System;
using System.Collections.Generic;

namespace LibroMind_BE.DAL.Models;

public partial class BookLibrary
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public int LibraryId { get; set; }

    public int Quantity { get; set; }

    public DateTime AddedDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual ICollection<Borrow> Borrows { get; } = new List<Borrow>();

    public virtual Library Library { get; set; } = null!;
}
