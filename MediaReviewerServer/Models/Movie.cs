using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediaReviewerServer.Models;

public partial class Movie
{
    [Key]
    [Column("ContentID")]
    public int ContentId { get; set; }

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

    [ForeignKey("ContentId")]
    [InverseProperty("Movie")]
    public virtual Content Content { get; set; } = null!;
}
