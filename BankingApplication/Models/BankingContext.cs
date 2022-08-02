using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BankingApplication.Models
{
    public partial class BankingContext : DbContext
    {
        public BankingContext()
        {
        }

        public BankingContext(DbContextOptions<BankingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Beneficiary> Beneficiaries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<InternetBanking> InternetBankings { get; set; }
        public virtual DbSet<Register> Registers { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=Banking;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountNumber)
                    .HasName("PK__Account__9F092CAAF7EF927D");

                entity.ToTable("Account");

                entity.Property(e => e.AccountNumber).HasColumnName("Account_Number");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Account_type")
                    .HasDefaultValueSql("('Savings')");

                entity.Property(e => e.Balance).HasDefaultValueSql("((0))");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Account__Custome__236943A5");
            });

            modelBuilder.Entity<Beneficiary>(entity =>
            {
                entity.HasKey(e => new { e.AccountNumber, e.BeneficiaryAccountNo })
                    .HasName("PK_Beneficiary_accountNo");

                entity.ToTable("Beneficiary");

                entity.Property(e => e.AccountNumber).HasColumnName("Account_Number");

                entity.Property(e => e.BeneficiaryAccountNo).HasColumnName("Beneficiary_account_no");

                entity.Property(e => e.BeneficiaryName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Beneficiary_name");

                entity.Property(e => e.NickName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Nick_name");

                entity.HasOne(d => d.AccountNumberNavigation)
                    .WithMany(p => p.BeneficiaryAccountNumberNavigations)
                    .HasForeignKey(d => d.AccountNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Beneficia__Accou__2B0A656D");

                entity.HasOne(d => d.BeneficiaryAccountNoNavigation)
                    .WithMany(p => p.BeneficiaryBeneficiaryAccountNoNavigations)
                    .HasForeignKey(d => d.BeneficiaryAccountNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Beneficia__Benef__2A164134");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.AadharNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("Aadhar_NUMBER");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Address_Line1");

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Address_Line2");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Email_Id");

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Father_Name");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("First_name");

                entity.Property(e => e.GrossAnnualIncome)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Gross_annual_income");

                entity.Property(e => e.Landmark)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Middle_Name");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Mobile_Number");

                entity.Property(e => e.OccupationType)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Occupation_type");

                entity.Property(e => e.Pincode)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("pincode");

                entity.Property(e => e.SourceOfIncome)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Source_of_income");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InternetBanking>(entity =>
            {
                entity.ToTable("InternetBanking");

                entity.Property(e => e.InternetBankingId).HasColumnName("Internet_Banking_ID");

                entity.Property(e => e.AccountNumber).HasColumnName("Account_Number");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.TransactionPassword)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Transaction_password");

                entity.HasOne(d => d.AccountNumberNavigation)
                    .WithMany(p => p.InternetBankings)
                    .HasForeignKey(d => d.AccountNumber)
                    .HasConstraintName("FK__InternetB__Accou__2739D489");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.InternetBankings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__InternetB__Custo__2645B050");
            });

            modelBuilder.Entity<Register>(entity =>
            {
                entity.ToTable("Register");

                entity.Property(e => e.RegisterId).HasColumnName("Register_ID");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Registers)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Register__Custom__31B762FC");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.TransactionId).HasColumnName("Transaction_id");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FromAccountNo).HasColumnName("From_AccountNo");

                entity.Property(e => e.Mode)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ToAccountNo).HasColumnName("To_AccountNo");

                entity.HasOne(d => d.FromAccountNoNavigation)
                    .WithMany(p => p.TransactionFromAccountNoNavigations)
                    .HasForeignKey(d => d.FromAccountNo)
                    .HasConstraintName("FK__Transacti__From___2DE6D218");

                entity.HasOne(d => d.ToAccountNoNavigation)
                    .WithMany(p => p.TransactionToAccountNoNavigations)
                    .HasForeignKey(d => d.ToAccountNo)
                    .HasConstraintName("FK__Transacti__To_Ac__2EDAF651");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
