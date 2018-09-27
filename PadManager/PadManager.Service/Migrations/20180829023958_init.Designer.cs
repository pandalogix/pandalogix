﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PadManager.Core;

namespace PadManager.Service.Migrations
{
    [DbContext(typeof(PandaManagerContext))]
    [Migration("20180829023958_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PadManager.Core.Models.InstanceMapping", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("FieldMappings");

                    b.Property<Guid>("Identifier");

                    b.Property<DateTimeOffset>("LastUpdatedDate");

                    b.Property<long?>("PadId");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("Identifier")
                        .IsUnique();

                    b.HasIndex("PadId");

                    b.ToTable("InstanceMapping");
                });

            modelBuilder.Entity("PadManager.Core.Models.Node", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<Guid>("Identifier");

                    b.Property<string>("InNodeList")
                        .HasColumnName("InNodes");

                    b.Property<DateTimeOffset>("LastUpdatedDate");

                    b.Property<string>("Location");

                    b.Property<string>("MetaData");

                    b.Property<int>("NodeId");

                    b.Property<string>("NodeType");

                    b.Property<string>("OutNodesList")
                        .HasColumnName("OutNodes");

                    b.Property<long?>("PadId");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("Identifier")
                        .IsUnique();

                    b.HasIndex("PadId");

                    b.ToTable("Node");
                });

            modelBuilder.Entity("PadManager.Core.Models.Pad", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<int>("CurrentMaxSequenceId");

                    b.Property<string>("Description");

                    b.Property<Guid>("Identifier");

                    b.Property<DateTimeOffset>("LastUpdatedDate");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("Identifier")
                        .IsUnique();

                    b.ToTable("Pads");
                });

            modelBuilder.Entity("PadManager.Core.Models.InstanceMapping", b =>
                {
                    b.HasOne("PadManager.Core.Models.Pad", "Pad")
                        .WithMany("InstanceMappings")
                        .HasForeignKey("PadId");
                });

            modelBuilder.Entity("PadManager.Core.Models.Node", b =>
                {
                    b.HasOne("PadManager.Core.Models.Pad", "Pad")
                        .WithMany("Nodes")
                        .HasForeignKey("PadId");
                });
#pragma warning restore 612, 618
        }
    }
}