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

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Series> Series { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=MediaReviewerDB;User ID=MediaReviewerAdminLogin;Password=pass031206;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PK__Books__2907A87E85B108A9");

            entity.Property(e => e.ContentId).ValueGeneratedNever();

            entity.HasOne(d => d.Content).WithOne(p => p.Book)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__ContentID__30F848ED");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PK__Contents__2907A87ED2CDE172");

            entity.HasMany(d => d.Genres).WithMany(p => p.Contents)
                .UsingEntity<Dictionary<string, object>>(
                    "GenresToContent",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GenresToC__Genre__3B75D760"),
                    l => l.HasOne<Content>().WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GenresToC__Conte__3A81B327"),
                    j =>
                    {
                        j.HasKey("ContentId", "GenreId").HasName("PK__GenresTo__D93FF82B8A699E22");
                        j.ToTable("GenresToContents");
                        j.IndexerProperty<int>("ContentId").HasColumnName("ContentID");
                        j.IndexerProperty<int>("GenreId").HasColumnName("GenreID");
                    });
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385055EE6028254");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PK__Movies__2907A87E46063D54");

            entity.Property(e => e.ContentId).ValueGeneratedNever();

            entity.HasOne(d => d.Content).WithOne(p => p.Movie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movies__ContentI__2B3F6F97");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8519A41D98811");

            entity.HasOne(d => d.User).WithMany(p => p.Requests).HasConstraintName("FK__Requests__UserID__37A5467C");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AE2B66FA17");

            entity.HasOne(d => d.Content).WithMany(p => p.Reviews).HasConstraintName("FK__Reviews__Content__34C8D9D1");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews).HasConstraintName("FK__Reviews__UserID__33D4B598");
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PK__Series__2907A87E2D323761");

            entity.Property(e => e.ContentId).ValueGeneratedNever();

            entity.HasOne(d => d.Content).WithOne(p => p.Series)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Series__ContentI__2E1BDC42");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACCEC740E0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
