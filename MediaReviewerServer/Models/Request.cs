using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediaReviewerServer.Models;

public partial class Request
{
    [Key]
    [Column("RequestID")]
    public int RequestId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [StringLength(256)]
    public string Title { get; set; } = null!;

    [StringLength(1024)]
    public string Description { get; set; } = null!;

    public DateOnly RequestDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Requests")]
    public virtual User? User { get; set; }
}
