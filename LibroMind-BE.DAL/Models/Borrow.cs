using System;
using System.Collections.Generic;

namespace LibroMind_BE.DAL.Models;

public partial class Borrow
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BookLibraryId { get; set; }

    public DateTime BorrowingDate { get; set; }

    public bool HasReturnedBook { get; set; }

    public virtual BookLibrary BookLibrary { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
