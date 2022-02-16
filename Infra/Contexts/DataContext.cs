using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
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

    //setting the properties with the correct DataType
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //Resident Mapping
        modelBuilder.Entity<Resident>().ToTable("Residents");
        modelBuilder.Entity<Resident>().Property(x => x.Id).IsRequired();
        modelBuilder.Entity<Resident>().OwnsOne(x=> x.Name, x=> x.Property(x=> x.FirstName).HasColumnType("varchar(120)").HasColumnName("First name"));
        modelBuilder.Entity<Resident>().OwnsOne(x => x.Name, x => x.Property(x => x.LastName).HasColumnType("varchar(120)").HasColumnName("Last name"));
        modelBuilder.Entity<Resident>().Property(x => x.PhoneNumber).HasColumnType("varchar(13)").HasColumnName("Phone number");
        modelBuilder.Entity<Resident>().Property(x => x.Email).HasColumnType("nvarchar(300)");
        modelBuilder.Entity<Resident>().OwnsOne(x => x.Document, x => x.Property(x => x.Number).HasColumnType("varchar(14)").HasColumnName("Document number"));
        modelBuilder.Entity<Resident>().OwnsOne(x => x.Document, x => x.Property(x => x.Type).HasColumnType("varchar(6)").HasColumnName("Document type"));

        modelBuilder.Entity<Resident>().HasKey(a => a.Id);

        //Apart Mapping
        modelBuilder.Entity<Apart>().ToTable("Aparts");
        modelBuilder.Entity<Apart>().Property(x => x.Number).HasColumnType("int(5)").HasColumnName("Apart number").IsRequired();
        modelBuilder.Entity<Apart>().Property(x => x.Block).HasColumnType("int(2)").HasColumnName("Apart block").IsRequired();
        modelBuilder.Entity<Apart>().HasMany(x => x.Residents).WithOne(x => x.Apart).HasForeignKey(x => new { x.ApartId, x.ApartId2 });
        modelBuilder.Entity<Apart>().HasMany(x => x.Packages).WithOne(x => x.Apart).HasForeignKey(x => new { x.ApartId, x.ApartId2 });
        modelBuilder.Entity<Apart>().HasMany(x => x.Visitants).WithOne(x => x.Apart).HasForeignKey(x => new { x.ApartId, x.ApartId2 });

        //composite key
        modelBuilder.Entity<Apart>().HasKey(a => new { a.Number, a.Block });

        //Packages Mapping
        modelBuilder.Entity<Packages>().ToTable("Packages");
        modelBuilder.Entity<Packages>().Property(x => x.Id).IsRequired();
        modelBuilder.Entity<Packages>().Property(x => x.ItemName).HasColumnType("varchar(150)").HasColumnName("Item name");
        modelBuilder.Entity<Packages>().Property(x => x.BarCode).HasColumnType("varchar(13)");
        modelBuilder.Entity<Packages>().Property(x => x.Type).HasConversion<int>();
        modelBuilder.Entity<Packages>().Property(x => x.Addressee).HasColumnType("varchar(250)").HasColumnName("Addressed to");
        modelBuilder.Entity<Packages>().Property(x => x.Sender).HasColumnType("varchar(150)");
        modelBuilder.Entity<Packages>().Property(x => x.SenderAddress).HasColumnType("varchar(250)").HasColumnName("Sender address");

        modelBuilder.Entity<Packages>().HasKey(x => x.Id);

        //Visitant Mapping
        modelBuilder.Entity<Visitant>().ToTable("Visitants");
        modelBuilder.Entity<Visitant>().Property(x => x.Id).IsRequired();
        modelBuilder.Entity<Visitant>().OwnsOne(x => x.Name, x => x.Property(x => x.FirstName).HasColumnType("varchar(120)").HasColumnName("First name"));
        modelBuilder.Entity<Visitant>().OwnsOne(x => x.Name, x => x.Property(x => x.LastName).HasColumnType("varchar(120)").HasColumnName("Last name"));
        modelBuilder.Entity<Visitant>().Property(x => x.PhoneNumber).HasColumnType("varchar(13)").HasColumnName("Phone number");
        modelBuilder.Entity<Visitant>().Property(x => x.Email).HasColumnType("nvarchar(300)");
        modelBuilder.Entity<Visitant>().OwnsOne(x => x.Document, x => x.Property(x => x.Number).HasColumnType("varchar(14)").HasColumnName("Document number"));
        modelBuilder.Entity<Visitant>().OwnsOne(x => x.Document, x => x.Property(x => x.Type).HasColumnType("varchar(6)").HasColumnName("Document type"));
        modelBuilder.Entity<Visitant>().Property(x => x.Active).HasColumnType("bit");

        modelBuilder.Entity<Visitant>().HasKey(a => a.Id);
    }
}