using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MaterialAccountingDatabase
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChartOfAccounts> ChartOfAccounts { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<PostingJournal> PostingJournal { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<ResponsiblePerson> ResponsiblePerson { get; set; }
        public virtual DbSet<Subdivision> Subdivision { get; set; }
        public virtual DbSet<TablePart> TablePart { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=genius;password=jfrum4t5fgh");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChartOfAccounts>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("chart_of_accounts_pkey");

                entity.ToTable("chart_of_accounts");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Nameofcheck)
                    .IsRequired()
                    .HasColumnName("nameofcheck")
                    .HasMaxLength(100);

                entity.Property(e => e.Numberofcheck)
                    .IsRequired()
                    .HasColumnName("numberofcheck")
                    .HasMaxLength(255);

                entity.Property(e => e.Subconto1)
                    .HasColumnName("subconto1")
                    .HasMaxLength(150);

                entity.Property(e => e.Subconto2)
                    .HasColumnName("subconto2")
                    .HasMaxLength(150);

                entity.Property(e => e.Subconto3)
                    .HasColumnName("subconto3")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("material_pkey");

                entity.ToTable("material");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(10,2)");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("operation_pkey");

                entity.ToTable("operation");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.Providercode).HasColumnName("providercode");

                entity.Property(e => e.Responsiblereceivercode).HasColumnName("responsiblereceivercode");

                entity.Property(e => e.Responsiblesendercode).HasColumnName("responsiblesendercode");

                entity.Property(e => e.Subdivisioncode).HasColumnName("subdivisioncode");

                entity.Property(e => e.Typeofoperation)
                    .HasColumnName("typeofoperation")
                    .HasMaxLength(100);

                entity.Property(e => e.Warehousereceivercode).HasColumnName("warehousereceivercode");

                entity.Property(e => e.Warehousesendercode).HasColumnName("warehousesendercode");

                entity.HasOne(d => d.ProvidercodeNavigation)
                    .WithMany(p => p.Operation)
                    .HasForeignKey(d => d.Providercode)
                    .HasConstraintName("provider_fkey");

                entity.HasOne(d => d.ResponsiblereceivercodeNavigation)
                    .WithMany(p => p.OperationResponsiblereceivercodeNavigation)
                    .HasForeignKey(d => d.Responsiblereceivercode)
                    .HasConstraintName("responsiblereceiver_fkey");

                entity.HasOne(d => d.ResponsiblesendercodeNavigation)
                    .WithMany(p => p.OperationResponsiblesendercodeNavigation)
                    .HasForeignKey(d => d.Responsiblesendercode)
                    .HasConstraintName("responsiblesender_fkey");

                entity.HasOne(d => d.SubdivisioncodeNavigation)
                    .WithMany(p => p.OperationSubdivisioncodeNavigation)
                    .HasForeignKey(d => d.Subdivisioncode)
                    .HasConstraintName("subdivision_fkey");

                entity.HasOne(d => d.WarehousereceivercodeNavigation)
                    .WithMany(p => p.OperationWarehousereceivercodeNavigation)
                    .HasForeignKey(d => d.Warehousereceivercode)
                    .HasConstraintName("warehousereceiver_fkey");

                entity.HasOne(d => d.WarehousesendercodeNavigation)
                    .WithMany(p => p.OperationWarehousesendercodeNavigation)
                    .HasForeignKey(d => d.Warehousesendercode)
                    .HasConstraintName("warehousesender_fkey");
            });

            modelBuilder.Entity<PostingJournal>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("posting_journal_pkey");

                entity.ToTable("posting_journal");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(150);

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Creditcheck).HasColumnName("creditcheck");

                entity.Property(e => e.Debetcheck).HasColumnName("debetcheck");

                entity.Property(e => e.Operationcode).HasColumnName("operationcode");

                entity.Property(e => e.Subcontocredit1)
                    .HasColumnName("subcontocredit1")
                    .HasMaxLength(150);

                entity.Property(e => e.Subcontocredit2)
                    .HasColumnName("subcontocredit2")
                    .HasMaxLength(150);

                entity.Property(e => e.Subcontocredit3)
                    .HasColumnName("subcontocredit3")
                    .HasMaxLength(150);

                entity.Property(e => e.Subcontodebet1)
                    .IsRequired()
                    .HasColumnName("subcontodebet1")
                    .HasMaxLength(150);

                entity.Property(e => e.Subcontodebet2)
                    .IsRequired()
                    .HasColumnName("subcontodebet2")
                    .HasMaxLength(150);

                entity.Property(e => e.Subcontodebet3)
                    .IsRequired()
                    .HasColumnName("subcontodebet3")
                    .HasMaxLength(150);

                entity.Property(e => e.Sum)
                    .HasColumnName("sum")
                    .HasColumnType("numeric(10,2)");

                entity.HasOne(d => d.CreditcheckNavigation)
                    .WithMany(p => p.PostingJournalCreditcheckNavigation)
                    .HasForeignKey(d => d.Creditcheck)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("creditcheck_fkey");

                entity.HasOne(d => d.DebetcheckNavigation)
                    .WithMany(p => p.PostingJournalDebetcheckNavigation)
                    .HasForeignKey(d => d.Debetcheck)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("debetcheck_fkey");

                entity.HasOne(d => d.OperationcodeNavigation)
                    .WithMany(p => p.PostingJournal)
                    .HasForeignKey(d => d.Operationcode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("operation_fkey");
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("provider_pkey");

                entity.ToTable("provider");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ResponsiblePerson>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("responsible_person_pkey");

                entity.ToTable("responsible_person");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Middlename)
                    .IsRequired()
                    .HasColumnName("middlename")
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Subdivision>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("subdivision_pkey");

                entity.ToTable("subdivision");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Warehouse).HasColumnName("warehouse");
            });

            modelBuilder.Entity<TablePart>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("table_part_pkey");

                entity.ToTable("table_part");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Materialcode).HasColumnName("materialcode");

                entity.Property(e => e.Operationcode).HasColumnName("operationcode");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.MaterialcodeNavigation)
                    .WithMany(p => p.TablePart)
                    .HasForeignKey(d => d.Materialcode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("material_fkey");

                entity.HasOne(d => d.OperationcodeNavigation)
                    .WithMany(p => p.TablePart)
                    .HasForeignKey(d => d.Operationcode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("operation_fkey");
            });

            modelBuilder.HasSequence("chartcode");

            modelBuilder.HasSequence("materialcode");

            modelBuilder.HasSequence("providercode");

            modelBuilder.HasSequence("responsiblepersoncode");

            modelBuilder.HasSequence("subdivisioncode");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
