using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infra.Contexts;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    //setting the tables
    public DbSet<Resident> Residents { get; set; }
    public DbSet<Apart> Aparts { get; set; }
    public DbSet<Packages> Packages { get; set; }
    public DbSet<Visitant> Visitant { get; set; }
    public DbSet<Administrator> Administrators { get; set; }

    //setting the properties with the correct DataType; / FluentAPI
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //Resident Mapping
        modelBuilder.Entity<Resident>().ToTable("Residents");
        modelBuilder.Entity<Resident>().Property(x => x.Id).IsRequired();
        modelBuilder.Entity<Resident>().OwnsOne(x => x.Name, x => x.Property(x => x.FirstName).HasColumnType("varchar(120)").HasColumnName("First name"));
        modelBuilder.Entity<Resident>().OwnsOne(x => x.Name, x => x.Property(x => x.LastName).HasColumnType("varchar(120)").HasColumnName("Last name"));
        modelBuilder.Entity<Resident>().Property(x => x.PhoneNumber).HasColumnType("varchar(14)").HasColumnName("Phone number");
        modelBuilder.Entity<Resident>().Property(x => x.Email).HasColumnType("nvarchar(300)");
        modelBuilder.Entity<Resident>().OwnsOne(x => x.Document, x => x.Property(x => x.Number).HasColumnType("varchar(14)").HasColumnName("Document number"));
        modelBuilder.Entity<Resident>().OwnsOne(x => x.Document, x => x.Property(x => x.Type).HasColumnName("Document type").HasConversion(v => v.ToString(), v => (EDocumentType)Enum.Parse(typeof(EDocumentType), v)));
        modelBuilder.Entity<Resident>().Property(x => x.Password).HasColumnType("varchar(100)").IsRequired();

        modelBuilder.Entity<Resident>().HasKey(a => a.Id);

        //Apart Mapping
        modelBuilder.Entity<Apart>().ToTable("Aparts");
        modelBuilder.Entity<Apart>().Property(x => x.Number).HasColumnType("int").HasColumnName("Apart number").IsRequired();
        modelBuilder.Entity<Apart>().Property(x => x.Block).HasColumnType("int").HasColumnName("Apart block").IsRequired();
        modelBuilder.Entity<Apart>().HasMany(x => x.Residents).WithOne(x => x.Apart).HasForeignKey(x => new { x.Number, x.Block });
        modelBuilder.Entity<Apart>().HasMany(x => x.Packages).WithOne(x => x.Apart).HasForeignKey(x => new { x.Number, x.Block });
        modelBuilder.Entity<Apart>().HasMany(x => x.Visitants).WithOne(x => x.Apart).HasForeignKey(x => new { x.Number, x.Block });

        //composite key
        modelBuilder.Entity<Apart>().HasKey(a => new { a.Number, a.Block });

        //Packages Mapping
        modelBuilder.Entity<Packages>().ToTable("Packages");
        modelBuilder.Entity<Packages>().Property(x => x.Id).IsRequired();
        modelBuilder.Entity<Packages>().Property(x => x.ItemName).HasColumnType("varchar(150)").HasColumnName("Item name");
        modelBuilder.Entity<Packages>().Property(x => x.BarCode).HasColumnType("varchar(14)");
        modelBuilder.Entity<Packages>().Property(x => x.Type).HasConversion(v => v.ToString(), v => (EPackageType)Enum.Parse(typeof(EPackageType), v));
        modelBuilder.Entity<Packages>().Property(x => x.Addressee).HasColumnType("varchar(250)").HasColumnName("Addressed to");
        modelBuilder.Entity<Packages>().Property(x => x.Sender).HasColumnType("varchar(150)");
        modelBuilder.Entity<Packages>().Property(x => x.SenderAddress).HasColumnType("varchar(250)").HasColumnName("Sender address");

        modelBuilder.Entity<Packages>().HasKey(x => x.Id);

        //Visitant Mapping
        modelBuilder.Entity<Visitant>().ToTable("Visitants");
        modelBuilder.Entity<Visitant>().Property(x => x.Id).IsRequired();
        modelBuilder.Entity<Visitant>().OwnsOne(x => x.Name, x => x.Property(x => x.FirstName).HasColumnType("varchar(120)").HasColumnName("First name"));
        modelBuilder.Entity<Visitant>().OwnsOne(x => x.Name, x => x.Property(x => x.LastName).HasColumnType("varchar(120)").HasColumnName("Last name"));
        modelBuilder.Entity<Visitant>().Property(x => x.PhoneNumber).HasColumnType("varchar(14)").HasColumnName("Phone number");
        modelBuilder.Entity<Visitant>().Property(x => x.Email).HasColumnType("nvarchar(300)");
        modelBuilder.Entity<Visitant>().OwnsOne(x => x.Document, x => x.Property(x => x.Number).HasColumnType("varchar(14)").HasColumnName("Document number"));
        modelBuilder.Entity<Visitant>().OwnsOne(x => x.Document, x => x.Property(x => x.Type).HasColumnName("Document type").HasConversion(v => v.ToString(), v => (EDocumentType)Enum.Parse(typeof(EDocumentType), v)));
        modelBuilder.Entity<Visitant>().Property(x => x.Active).HasColumnType("bit");

        modelBuilder.Entity<Visitant>().HasKey(a => a.Id);

        //Administrator Mapping
        modelBuilder.Entity<Administrator>().ToTable("Administrators");
        modelBuilder.Entity<Administrator>().Property(x => x.Id).IsRequired();
        modelBuilder.Entity<Administrator>().OwnsOne(x => x.Name, x => x.Property(x => x.FirstName).HasColumnType("varchar(120)").HasColumnName("First name"));
        modelBuilder.Entity<Administrator>().OwnsOne(x => x.Name, x => x.Property(x => x.LastName).HasColumnType("varchar(120)").HasColumnName("Last name"));
        modelBuilder.Entity<Administrator>().Property(x => x.PhoneNumber).HasColumnType("varchar(14)").HasColumnName("Phone number");
        modelBuilder.Entity<Administrator>().Property(x => x.Email).HasColumnType("nvarchar(300)");
        modelBuilder.Entity<Administrator>().OwnsOne(x => x.Document, x => x.Property(x => x.Number).HasColumnType("varchar(14)").HasColumnName("Document number"));
        modelBuilder.Entity<Administrator>().OwnsOne(x => x.Document, x => x.Property(x => x.Type).HasColumnName("Document type").HasConversion(v => v.ToString(), v => (EDocumentType)Enum.Parse(typeof(EDocumentType), v)));
        modelBuilder.Entity<Administrator>().Property(x => x.Password).HasColumnType("varchar(100)").IsRequired();

        modelBuilder.Entity<Administrator>().HasKey(a => a.Id);
    }
}