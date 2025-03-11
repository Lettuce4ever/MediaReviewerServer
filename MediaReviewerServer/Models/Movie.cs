using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediaReviewerServer.Models;

public partial class Movie
{
    [Key]
    [Column("MovieID")]
    public int MovieId { get; set; }

    [StringLength(256)]
    public string MovieName { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public int Length { get; set; }

    [StringLength(1024)]
    public string Description { get; set; } = null!;

    public double Rating { get; set; }

    public string Image { get; set; } = null!;

    public string Trailer { get; set; } = null!;

    [StringLength(256)]
    public string Director { get; set; } = null!;

    [StringLength(256)]
    public string Star { get; set; } = null!;

    [StringLength(256)]
    public string Writer { get; set; } = null!;

    public bool MultiDirectors { get; set; }

    public bool MultiStars { get; set; }

    public bool MultiWriters { get; set; }

    [InverseProperty("Movie")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [ForeignKey("MovieId")]
    [InverseProperty("Movies")]
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
