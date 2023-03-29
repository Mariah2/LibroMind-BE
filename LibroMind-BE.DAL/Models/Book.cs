using System;
using System.Collections.Generic;

namespace LibroMind_BE.DAL.Models;

public partial class Book
{
    public int Id { get; set; }

    public int AuthorId { get; set; }

    public int PublisherId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime PublishingDate { get; set; }

    public string Description { get; set; } = null!;

    public int PagesNumber { get; set; }

    public string? CoverUrl { get; set; }

    public double Rating { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<BookCategory> BookCategories { get; } = new List<BookCategory>();

    public virtual ICollection<BookLibrary> BookLibraries { get; } = new List<BookLibrary>();

    public virtual ICollection<BookUser> BookUsers { get; } = new List<BookUser>();

    public virtual Publisher Publisher { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();
}
