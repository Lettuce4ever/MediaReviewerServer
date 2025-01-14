using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediaReviewerServer.Models;

public partial class Content
{
    [Key]
    [Column("ContentID")]
    public int ContentId { get; set; }

    [StringLength(256)]
    public string ContentName { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public int Length { get; set; }

    [StringLength(1024)]
    public string Description { get; set; } = null!;

    public double Rating { get; set; }

    public string Image { get; set; } = null!;

    [InverseProperty("Content")]
    public virtual Book? Book { get; set; }

    [InverseProperty("Content")]
    public virtual Movie? Movie { get; set; }

    [InverseProperty("Content")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("Content")]
    public virtual Series? Series { get; set; }

    [ForeignKey("ContentId")]
    [InverseProperty("Contents")]
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
