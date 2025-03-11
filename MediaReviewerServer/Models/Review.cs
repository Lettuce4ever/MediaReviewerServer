using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediaReviewerServer.Models;

public partial class Review
{
    [Key]
    [Column("ReviewID")]
    public int ReviewId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("MovieID")]
    public int? MovieId { get; set; }

    public double Rating { get; set; }

    [StringLength(1024)]
    public string Description { get; set; } = null!;

    public DateOnly ReviewDate { get; set; }

    [ForeignKey("MovieId")]
    [InverseProperty("Reviews")]
    public virtual Movie? Movie { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Reviews")]
    public virtual User? User { get; set; }
}
