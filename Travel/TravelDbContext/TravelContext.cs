using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Travel.Models.Travel;

namespace Travel.TravelDbContext
{
    public partial class TravelContext : DbContext
    {
        public TravelContext()
        {
        }

        public TravelContext(DbContextOptions<TravelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerTb> CustomerTb { get; set; } = null!;
        public virtual DbSet<TravelBn> TravelBn { get; set; } = null!;
        public virtual DbSet<VBimekind> VBimekind { get; set; } = null!;
        public virtual DbSet<VCountry> VCountry { get; set; } = null!;
        public virtual DbSet<VCoverLimit> VCoverLimit { get; set; } = null!;
        public virtual DbSet<VCustomer> VCustomer { get; set; } = null!;
        public virtual DbSet<VGrpTakhfif> VGrpTakhfif { get; set; } = null!;
        public virtual DbSet<VLocation> VLocation { get; set; } = null!;
        public virtual DbSet<VLocationZone> VLocationZone { get; set; } = null!;
        public virtual DbSet<VModdatKind> VModdatKind { get; set; } = null!;
        public virtual DbSet<VPersonJens> VPersonJens { get; set; } = null!;
        public virtual DbSet<VPersonKind> VPersonKind { get; set; } = null!;
        public virtual DbSet<VSafarKind> VSafarKind { get; set; } = null!;
        public virtual DbSet<VSodurPlace> VSodurPlace { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerTb>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("CustomerTB");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phoneno)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TravelBn>(entity =>
            {
                entity.Property(e => e.AgentName).HasMaxLength(150);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Bank).HasMaxLength(100);

                entity.Property(e => e.BirthDateMiladi).HasColumnType("date");

                entity.Property(e => e.BirthDay)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.BirthMonth)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.BirthYear)
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.BithDateShamsi)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CodeMelli).HasMaxLength(10);

                entity.Property(e => e.CodePosti).HasMaxLength(50);

                entity.Property(e => e.CodeSodurName).HasMaxLength(150);

                entity.Property(e => e.CompanyCode).HasMaxLength(11);

                entity.Property(e => e.CountryName).HasMaxLength(150);

                entity.Property(e => e.CountryOutName).HasMaxLength(150);

                entity.Property(e => e.CoverLimitName).HasMaxLength(50);

                entity.Property(e => e.DayBirth)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.FatherName).HasMaxLength(50);

                entity.Property(e => e.FishDate)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FishNo).HasMaxLength(50);

                entity.Property(e => e.IdentityNo).HasMaxLength(11);

                entity.Property(e => e.IssueDate)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.IssueDateMiladi).HasColumnType("date");

                entity.Property(e => e.LatinName).HasMaxLength(250);

                entity.Property(e => e.LatinlName).HasMaxLength(150);

                entity.Property(e => e.MbeginDate)
                    .HasMaxLength(10)
                    .HasColumnName("MBeginDate")
                    .IsFixedLength();

                entity.Property(e => e.MbirthYear)
                    .HasMaxLength(4)
                    .HasColumnName("MBirthYear")
                    .IsFixedLength();

                entity.Property(e => e.Mobile).HasMaxLength(100);

                entity.Property(e => e.MonthBirth)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.MsodurDate)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PassportNo).HasMaxLength(50);

                entity.Property(e => e.PersonAddress).HasMaxLength(300);

                entity.Property(e => e.PersonLname).HasMaxLength(150);

                entity.Property(e => e.PersonName).HasMaxLength(250);

                entity.Property(e => e.SafarKindName).HasMaxLength(50);

                entity.Property(e => e.Seri).HasMaxLength(20);

                entity.Property(e => e.Serial).HasMaxLength(10);

                entity.Property(e => e.Sodurplace)
                    .HasMaxLength(50)
                    .HasColumnName("sodurplace");

                entity.Property(e => e.Tel).HasMaxLength(100);

                entity.Property(e => e.UnIranianCode).HasMaxLength(100);

                entity.Property(e => e.YearBirth)
                    .HasMaxLength(4)
                    .IsFixedLength();
            });

            modelBuilder.Entity<VBimekind>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Bimekind");

                entity.Property(e => e.BimekindName).HasMaxLength(19);
            });

            modelBuilder.Entity<VCountry>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Country");

                entity.Property(e => e.CountryName).HasMaxLength(12);
            });

            modelBuilder.Entity<VCoverLimit>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_CoverLimit");

                entity.Property(e => e.CoverLimitName).HasMaxLength(8);
            });

            modelBuilder.Entity<VCustomer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Customer");

                entity.Property(e => e.Address)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.AgentCodeMelli)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.CiimobileStatus).HasColumnName("CIIMobileStatus");

                entity.Property(e => e.CiivalidationStatus).HasColumnName("CIIValidationStatus");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.CodeMelli)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.CodeMelliText)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.CodePosti)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("companyCode")
                    .IsFixedLength()
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.ComparableName)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeathDate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(275)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.Economic)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.FatherName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.FullAddress)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.IdentityNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.InquiryDate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.Internaltime).HasColumnType("datetime");

                entity.Property(e => e.JobAddress)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("jobAddress")
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.LatinAddress)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.LatinLname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LatinLName")
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.LatinName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.Lname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LName")
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.MaliAcc).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.MbirthDay).HasColumnName("MBirthDay");

                entity.Property(e => e.MbirthMonth).HasColumnName("MBirthMonth");

                entity.Property(e => e.MbirthYear).HasColumnName("MBirthYear");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.PassportNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.PersonalCode)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.PrintName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.RegisterDate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.SabtNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.ShortDisplayName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.ShortPrintName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.SodurPlace)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("state")
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.Tahsilat).HasColumnName("tahsilat");

                entity.Property(e => e.TahsilatReshte)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tahsilatReshte")
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.Tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.UniranianCode)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .UseCollation("Persian_100_CI_AS");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<VGrpTakhfif>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_GrpTakhfif");

                entity.Property(e => e.GrpTakhfifName).HasMaxLength(5);
            });

            modelBuilder.Entity<VLocation>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Location");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VLocationZone>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_LocationZone");

                entity.Property(e => e.LocationZoneName).HasMaxLength(9);
            });

            modelBuilder.Entity<VModdatKind>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_ModdatKind");

                entity.Property(e => e.ModdatKindName).HasMaxLength(7);
            });

            modelBuilder.Entity<VPersonJens>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_PersonJens");

                entity.Property(e => e.PersonJensName).HasMaxLength(6);
            });

            modelBuilder.Entity<VPersonKind>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_PersonKind");

                entity.Property(e => e.PersonKindName).HasMaxLength(5);
            });

            modelBuilder.Entity<VSafarKind>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_SafarKind");

                entity.Property(e => e.SafarKindName).HasMaxLength(8);
            });

            modelBuilder.Entity<VSodurPlace>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_SodurPlace");

                entity.Property(e => e.SodurPlaceName).HasMaxLength(6);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
