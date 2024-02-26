﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tracker.Module.Budget.Persistence;

#nullable disable

namespace Tracker.Module.Budget.Persistence.Migrations
{
    [DbContext(typeof(BudgetDatabaseContext))]
    [Migration("20240226103627_InitialBudget")]
    partial class InitialBudget
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("Tracker.Shared.Models.Modules.Budget.Entity.CurrencyRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("CurrencyRateId");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("FromCurrency")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Rate")
                        .HasColumnType("REAL");

                    b.Property<int>("ToCurrency")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("CurrencyRates");
                });

            modelBuilder.Entity("Tracker.Shared.Models.Modules.Budget.Entity.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("PaymentId");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<int>("CoreUserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Currency")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RecurringPaymentId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("CoreUserId");

                    b.HasIndex("PaymentTypeId")
                        .IsUnique();

                    b.HasIndex("RecurringPaymentId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Tracker.Shared.Models.Modules.Budget.Entity.PaymentTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("PaymentTemplateId");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Currency")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecurringPaymentId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("PaymentTypeId")
                        .IsUnique();

                    b.HasIndex("RecurringPaymentId")
                        .IsUnique();

                    b.ToTable("PaymentTemplates");
                });

            modelBuilder.Entity("Tracker.Shared.Models.Modules.Budget.Entity.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("PaymentTypeId");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");
                });

            modelBuilder.Entity("Tracker.Shared.Models.Modules.Budget.RecurringPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("RecurringPaymentId");

                    b.Property<int>("CoreUserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("End")
                        .HasColumnType("TEXT");

                    b.Property<int>("PaymentTemplateId")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("Start")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("CoreUserId");

                    b.ToTable("RecurringPayments");
                });

            modelBuilder.Entity("Tracker.Shared.Models.User.CoreUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("CoreUserId");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("CoreUsers");
                });

            modelBuilder.Entity("Tracker.Shared.Models.Modules.Budget.Entity.Payment", b =>
                {
                    b.HasOne("Tracker.Shared.Models.User.CoreUser", "CoreUser")
                        .WithMany("Payments")
                        .HasForeignKey("CoreUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tracker.Shared.Models.Modules.Budget.Entity.PaymentType", "PaymentType")
                        .WithOne()
                        .HasForeignKey("Tracker.Shared.Models.Modules.Budget.Entity.Payment", "PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tracker.Shared.Models.Modules.Budget.RecurringPayment", "RecurringPayment")
                        .WithMany("Payments")
                        .HasForeignKey("RecurringPaymentId");

                    b.Navigation("CoreUser");

                    b.Navigation("PaymentType");

                    b.Navigation("RecurringPayment");
                });

            modelBuilder.Entity("Tracker.Shared.Models.Modules.Budget.Entity.PaymentTemplate", b =>
                {
                    b.HasOne("Tracker.Shared.Models.Modules.Budget.Entity.PaymentType", "PaymentType")
                        .WithOne()
                        .HasForeignKey("Tracker.Shared.Models.Modules.Budget.Entity.PaymentTemplate", "PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tracker.Shared.Models.Modules.Budget.RecurringPayment", "RecurringPayment")
                        .WithOne("PaymentTemplate")
                        .HasForeignKey("Tracker.Shared.Models.Modules.Budget.Entity.PaymentTemplate", "RecurringPaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentType");

                    b.Navigation("RecurringPayment");
                });

            modelBuilder.Entity("Tracker.Shared.Models.Modules.Budget.RecurringPayment", b =>
                {
                    b.HasOne("Tracker.Shared.Models.User.CoreUser", "CoreUser")
                        .WithMany("RecurringPayments")
                        .HasForeignKey("CoreUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoreUser");
                });

            modelBuilder.Entity("Tracker.Shared.Models.Modules.Budget.RecurringPayment", b =>
                {
                    b.Navigation("PaymentTemplate")
                        .IsRequired();

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Tracker.Shared.Models.User.CoreUser", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("RecurringPayments");
                });
#pragma warning restore 612, 618
        }
    }
}
