using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MediaReviewerServer.Models;

public partial class MediaReviewerDbContext : DbContext
{
    public MediaReviewerDbContext()
    {
    }

    public MediaReviewerDbContext(DbContextOptions<MediaReviewerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=MediaReviewerDB;User ID=MediaReviewerAdminLogin;Password=pass031206;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385055E1053407D");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__4BD2943AEAD91C43");

            entity.HasMany(d => d.Genres).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "GenresToMovie",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GenresToM__Genre__32E0915F"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GenresToM__Movie__31EC6D26"),
                    j =>
                    {
                        j.HasKey("MovieId", "GenreId").HasName("PK__GenresTo__BBEAC46F7F3E8F00");
                        j.ToTable("GenresToMovies");
                        j.IndexerProperty<int>("MovieId").HasColumnName("MovieID");
                        j.IndexerProperty<int>("GenreId").HasColumnName("GenreID");
                    });
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8519AD2E3E70D");

            entity.HasOne(d => d.User).WithMany(p => p.Requests).HasConstraintName("FK__Requests__UserID__2F10007B");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AEFB9C8AE6");

            entity.HasOne(d => d.Movie).WithMany(p => p.Reviews).HasConstraintName("FK__Reviews__MovieID__2C3393D0");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews).HasConstraintName("FK__Reviews__UserID__2B3F6F97");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC2B490F3F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
