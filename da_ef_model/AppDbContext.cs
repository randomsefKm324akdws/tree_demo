using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace da_ef_model;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Node> Nodes { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			.AddJsonFile("appsettings.json")
			.Build();

		optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Log_pk");

            entity.ToTable("Log");

            entity.Property(e => e.Data).IsUnicode(false);
            entity.Property(e => e.EventTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Node>(entity =>
        {
            entity.HasKey(e => new { e.TreeName, e.Id });

            entity.HasIndex(e => new { e.TreeName, e.ParentId }, "IX_Nodes_TreeName_ParentId");

            entity.HasIndex(e => new { e.TreeName, e.ParentId }, "IX_Nodes_ParentId_Null")
                .IsUnique()
                .HasFilter("([ParentId] IS NULL)");

            entity.Property(e => e.TreeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Parent).WithMany(p => p.Children)
                .HasForeignKey(d => new { d.TreeName, d.ParentId })
                .HasConstraintName("Nodes_Nodes_TreeName_Id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
