using System;
using System.Collections.Generic;

namespace LibroMind_BE.DAL.Models;

public partial class BookUser
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public int UserId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
