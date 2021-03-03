﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MySqlDevices.Persistence;

namespace MySqlDevices.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("MySqlDevices.Core.Entities.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DeviceType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .HasColumnType("varbinary(4000)");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("MySqlDevices.Core.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("MailAdress")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .HasColumnType("varbinary(4000)");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("MySqlDevices.Core.Entities.Usage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .HasColumnType("varbinary(4000)");

                    b.Property<DateTime?>("To")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("PersonId");

                    b.ToTable("Usages");
                });

            modelBuilder.Entity("MySqlDevices.Core.Entities.Usage", b =>
                {
                    b.HasOne("MySqlDevices.Core.Entities.Device", "Device")
                        .WithMany("Usages")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MySqlDevices.Core.Entities.Person", "Person")
                        .WithMany("Usages")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MySqlDevices.Core.Entities.Device", b =>
                {
                    b.Navigation("Usages");
                });

            modelBuilder.Entity("MySqlDevices.Core.Entities.Person", b =>
                {
                    b.Navigation("Usages");
                });
#pragma warning restore 612, 618
        }
    }
}