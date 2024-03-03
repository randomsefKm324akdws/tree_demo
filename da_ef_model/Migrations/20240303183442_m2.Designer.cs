﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using da_ef_model;

#nullable disable

namespace da_ef_model.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240303183442_m2")]
    partial class m2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("da_ef_model.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Data")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime>("EventTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getutcdate())");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("Log_pk");

                    b.ToTable("Log", (string)null);
                });

            modelBuilder.Entity("da_ef_model.Node", b =>
                {
                    b.Property<string>("TreeName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("TreeName", "Id");

                    b.HasIndex(new[] { "TreeName", "ParentId" }, "IX_Nodes_TreeName_ParentId");

                    b.HasIndex(new[] { "TreeName", "ParentId" }, "IX_Nodes_TreeName_ParentId_Null")
                        .IsUnique()
                        .HasFilter("([ParentId] IS NULL)");

                    b.ToTable("Nodes");
                });

            modelBuilder.Entity("da_ef_model.Node", b =>
                {
                    b.HasOne("da_ef_model.Node", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("TreeName", "ParentId")
                        .HasConstraintName("Nodes_Nodes_TreeName_Id_fk");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("da_ef_model.Node", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
