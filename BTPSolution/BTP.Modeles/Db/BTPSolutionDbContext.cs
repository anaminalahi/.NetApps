using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTP.Modeles.Db;

public partial class BTPSolutionDbContext : DbContext
{
    public BTPSolutionDbContext()
    {
    }

    public BTPSolutionDbContext(DbContextOptions<BTPSolutionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<Chantier> Chantiers { get; set; }

    public virtual DbSet<Projet> Projets { get; set; }


    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code.
    //You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148.
    //For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=GIGASERVER01\\SQLEXPRESS;Initial catalog=BTP;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(256);

            entity.HasMany(d => d.AspNetUsers).WithMany(p => p.AspNetRoles)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetUser>().WithMany()
                        .HasForeignKey("AspNetUsersId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers"),
                    l => l.HasOne<AspNetRole>().WithMany()
                        .HasForeignKey("AspNetRolesId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles"),
                    j =>
                    {
                        j.HasKey("AspNetRolesId", "AspNetUsersId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "AspNetUsersId" }, "IX_FK_AspNetUserRoles_AspNetUsers");
                        j.IndexerProperty<string>("AspNetRolesId")
                            .HasMaxLength(128)
                            .HasColumnName("AspNetRoles_Id");
                        j.IndexerProperty<string>("AspNetUsersId")
                            .HasMaxLength(128)
                            .HasColumnName("AspNetUsers_Id");
                    });
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(128);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId");

            entity.Property(e => e.UserId).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId");
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId });

            entity.HasIndex(e => e.UserId, "IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);
            entity.Property(e => e.UserId).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Chantier>(entity =>
        {
            entity.HasKey(e => e.IdChantier);

            entity.HasIndex(e => e.ProjetsIdProjet, "IX_FK_ProjetsChantiers");

            entity.Property(e => e.DateDebut).HasColumnType("datetime");
            entity.Property(e => e.DateFin).HasColumnType("datetime");
            entity.Property(e => e.LibelleChantier).HasMaxLength(100);

            entity.HasOne(d => d.ProjetsIdProjetNavigation).WithMany(p => p.Chantiers)
                .HasForeignKey(d => d.ProjetsIdProjet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjetsChantiers");
        });

        modelBuilder.Entity<Projet>(entity =>
        {
            entity.HasKey(e => e.IdProjet);

            entity.HasIndex(e => e.AspNetUsersId, "IX_FK_AspNetUsersProjets");

            entity.Property(e => e.AspNetUsersId).HasMaxLength(128);
            entity.Property(e => e.DateDebut).HasColumnType("datetime");
            entity.Property(e => e.DateFin).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.NomProjet).HasMaxLength(100);

            entity.HasOne(d => d.AspNetUsers).WithMany(p => p.Projets)
                .HasForeignKey(d => d.AspNetUsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AspNetUsersProjets");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
