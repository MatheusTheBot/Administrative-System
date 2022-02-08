using Domain.Entities;
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
        modelBuilder.Entity<Resident>().Property(x => x.Id).IsRequired();
        modelBuilder.Entity<Resident>().Property(x => x.Name.FirstName).HasColumnType("varchar(120)").HasColumnName("First name");
        modelBuilder.Entity<Resident>().Property(x => x.Name.LastName).HasColumnType("varchar(120)").HasColumnName("Last name");
        modelBuilder.Entity<Resident>().Property(x => x.PhoneNumber).HasColumnType("varchar(13)").HasColumnName("Phone number");
        modelBuilder.Entity<Resident>().Property(x => x.Email).HasColumnType("nvarchar(300)");
        modelBuilder.Entity<Resident>().Property(x => x.Document.Number).HasColumnType("varchar(14)").HasColumnName("Document number");
        modelBuilder.Entity<Resident>().Property(x => x.Document.Type).HasConversion<int>().HasColumnType("varchar(6)").HasColumnName("Document type");

        modelBuilder.Entity<Resident>().HasKey(a => a.Id);

        //Apart Mapping
        modelBuilder.Entity<Apart>().Property(x => x.Number).HasColumnType("int(5)").HasColumnName("Apart number").IsRequired();
        modelBuilder.Entity<Apart>().Property(x => x.Block).HasColumnType("int(2)").HasColumnName("Apart block").IsRequired();
        modelBuilder.Entity<Apart>().HasMany(x => x.Residents);
        modelBuilder.Entity<Apart>().HasMany(x => x.Packages);
        modelBuilder.Entity<Apart>().HasMany(x => x.Visitants);

        //composed key
        modelBuilder.Entity<Apart>().HasKey(a => new { a.Number, a.Block });

        //Packages Mapping
        modelBuilder.Entity<Packages>().Property(x => x.Id).IsRequired();
        modelBuilder.Entity<Packages>().Property(x => x.ItemName).HasColumnType("varchar(150)").HasColumnName("Item name");
        modelBuilder.Entity<Packages>().Property(x => x.BarCode).HasColumnType("varchar(30)");
        modelBuilder.Entity<Packages>().Property(x => x.Type).HasConversion<int>();
        modelBuilder.Entity<Packages>().Property(x => x.Addressee).HasColumnType("varchar(300)").HasColumnName("Addressed to");
        modelBuilder.Entity<Packages>().Property(x => x.Sender).HasColumnType("varchar(150)");
        modelBuilder.Entity<Packages>().Property(x => x.SenderAddress).HasColumnType("varchar(250)").HasColumnName("Sender address");

        modelBuilder.Entity<Packages>().HasKey(x => x.Id);

        //Visitant Mapping
        modelBuilder.Entity<Visitant>().Property(x => x.Id).IsRequired();
        modelBuilder.Entity<Visitant>().Property(x => x.Name.FirstName).HasColumnType("varchar(120)").HasColumnName("First name");
        modelBuilder.Entity<Visitant>().Property(x => x.Name.LastName).HasColumnType("varchar(120)").HasColumnName("Last name");
        modelBuilder.Entity<Visitant>().Property(x => x.PhoneNumber).HasColumnType("varchar(13)").HasColumnName("Phone number");
        modelBuilder.Entity<Visitant>().Property(x => x.Email).HasColumnType("nvarchar(300)");
        modelBuilder.Entity<Visitant>().Property(x => x.Document.Number).HasColumnType("varchar(14)").HasColumnName("Document number");
        modelBuilder.Entity<Visitant>().Property(x => x.Document.Type).HasConversion<int>().HasColumnType("varchar(6)").HasColumnName("Document type");
        modelBuilder.Entity<Visitant>().Property(x => x.Active).HasColumnType("bit");

        modelBuilder.Entity<Visitant>().HasKey(a => a.Id);
    }
}