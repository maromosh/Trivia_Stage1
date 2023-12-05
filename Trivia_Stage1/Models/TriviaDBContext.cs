using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class TriviaDBContext : DbContext
{
    public TriviaDBContext()
    {
    }

    public TriviaDBContext(DbContextOptions<TriviaDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LevelTab> LevelTabs { get; set; }

    public virtual DbSet<PlayersTab> PlayersTabs { get; set; }

    public virtual DbSet<QuestionTab> QuestionTabs { get; set; }

    public virtual DbSet<QuestionsStatusTab> QuestionsStatusTabs { get; set; }

    public virtual DbSet<SubjectTab> SubjectTabs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database=TriviaDB; Trusted_Connection = True; TrustServerCertificate = True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LevelTab>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("PK__LevelTab__09F03C0610056B21");

            entity.Property(e => e.LevelId).ValueGeneratedNever();
        });

        modelBuilder.Entity<PlayersTab>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PlayersT__3214EC27323EBE2B");

            entity.HasOne(d => d.IdlevelNavigation).WithMany(p => p.PlayersTabs).HasConstraintName("FK__PlayersTa__IDlev__2B3F6F97");
        });

        modelBuilder.Entity<QuestionTab>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8CEC75AEDD");

            entity.HasOne(d => d.Player).WithMany(p => p.QuestionTabs).HasConstraintName("FK__QuestionT__Playe__2E1BDC42");

            entity.HasOne(d => d.Status).WithMany(p => p.QuestionTabs).HasConstraintName("FK__QuestionT__Statu__300424B4");

            entity.HasOne(d => d.Subject).WithMany(p => p.QuestionTabs).HasConstraintName("FK__QuestionT__Subje__2F10007B");
        });

        modelBuilder.Entity<QuestionsStatusTab>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Question__C8EE20630CF20019");

            entity.Property(e => e.StatusId).ValueGeneratedNever();
        });

        modelBuilder.Entity<SubjectTab>(entity =>
        {
            entity.HasKey(e => e.SubId).HasName("PK__SubjectT__4D9BB86A84D33135");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
