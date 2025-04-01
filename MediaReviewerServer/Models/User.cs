using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediaReviewerServer.Models;

[Index("Email", Name = "UQ__Users__A9D10534F43FC6E1", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(50)]
    public string Password { get; set; } = null!;

    [StringLength(256)]
    public string Firstname { get; set; } = null!;

    [StringLength(256)]
    public string Lastname { get; set; } = null!;

    [StringLength(50)]
    public string Email { get; set; } = null!;

    public bool IsAdmin { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    [InverseProperty("User")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
//הערה