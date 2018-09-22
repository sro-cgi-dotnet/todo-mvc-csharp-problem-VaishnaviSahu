﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi.Models;

namespace TodoApi.Migrations
{
    [DbContext(typeof(TodoContext))]
    partial class TodoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TodoApi.Models.CheckListItem", b =>
                {
                    b.Property<int>("Id");

                    b.Property<bool>("IsChecked");

                    b.Property<int>("NoteId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("CheckLists");
                });

            modelBuilder.Entity("TodoApi.Models.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("NoteId");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("TodoApi.Models.Note", b =>
                {
                    b.Property<int?>("NoteId");

                    b.Property<bool>("IsPinned");

                    b.Property<string>("PlainText");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("NoteId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("TodoApi.Models.CheckListItem", b =>
                {
                    b.HasOne("TodoApi.Models.Note")
                        .WithMany("CheckList")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TodoApi.Models.Label", b =>
                {
                    b.HasOne("TodoApi.Models.Note")
                        .WithMany("Labels")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
