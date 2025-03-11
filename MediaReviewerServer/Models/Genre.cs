using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediaReviewerServer.Models;

public partial class Genre
{
    [Key]
    [Column("GenreID")]
    public int GenreId { get; set; }

    [StringLength(256)]
    public string GenreName { get; set; } = null!;

    [ForeignKey("GenreId")]
    [InverseProperty("Genres")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
