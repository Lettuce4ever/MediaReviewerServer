using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediaReviewerServer.Models;

public partial class Book
{
    [Key]
    [Column("ContentID")]
    public int ContentId { get; set; }

    [StringLength(256)]
    public string Author { get; set; } = null!;

    public bool MultiAuthors { get; set; }

    [ForeignKey("ContentId")]
    [InverseProperty("Book")]
    public virtual Content Content { get; set; } = null!;
}
