﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using infosysapi.Context;

namespace infosysapi.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20220601083850_5th")]
    partial class _5th
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("infosysapi.Models.Cours", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<int>("semester")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("infosysapi.Models.Grade", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("courseid")
                        .HasColumnType("text");

                    b.Property<int>("grade")
                        .HasColumnType("integer");

                    b.Property<string>("studentid")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("grades");
                });

            modelBuilder.Entity("infosysapi.Models.Homework", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("courseid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("studentid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("submissiondate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.ToTable("homeworks");
                });

            modelBuilder.Entity("infosysapi.Models.ProfTeaching", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("courseid")
                        .HasColumnType("text");

                    b.Property<string>("profid")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("profteachings");
                });

            modelBuilder.Entity("infosysapi.Models.Professor", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<string>("firstname")
                        .HasColumnType("text");

                    b.Property<string>("lastname")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("professors");
                });

            modelBuilder.Entity("infosysapi.Models.StudEnrollment", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("courseid")
                        .HasColumnType("text");

                    b.Property<string>("studentid")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("studenrollments");
                });

            modelBuilder.Entity("infosysapi.Models.Student", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<string>("firstname")
                        .HasColumnType("text");

                    b.Property<string>("lastname")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("students");
                });
#pragma warning restore 612, 618
        }
    }
}
