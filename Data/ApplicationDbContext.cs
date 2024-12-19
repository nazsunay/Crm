using Microsoft.EntityFrameworkCore;
using ProjectDb.Models;
namespace ProjectDb.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Yapıcı metot ekleniyor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet tanımlamaları
        public DbSet<Login> Logins  { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<AuthorizedPerson> AuthorizedPersons { get; set; }
        public DbSet<MeetingNote> MeetingNotes { get; set; }

        public DbSet<MeetingType> MeetingTypes { get; set; }
        public DbSet<CustomerCase> CustomerCases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CustomerCase>(entity =>
            {
                entity.HasKey(e => new { e.CustomerCode, e.UserCode }); // Birleşik birincil anahtar
            });
            // Customer yapılandırması
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CompanyName).IsRequired();

                entity.Property(e => e.CountryCode).IsRequired();

                entity.HasOne(e => e.Country)
                      .WithMany()
                      .HasForeignKey(e => e.CountryCode)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.City)
                      .WithMany()
                      .HasForeignKey(e => e.CityCode)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.District)
                      .WithMany()
                      .HasForeignKey(e => e.DistrictCode)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.AuthorizedPersons)
                      .WithOne(ap => ap.Customers)
                      .HasForeignKey(ap => ap.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.MeetingNotes)
                      .WithOne(mn => mn.Customers)
                      .HasForeignKey(mn => mn.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Country yapılandırması
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryCode);

                entity.Property(e => e.CountryDescription).IsRequired();

                entity.HasMany(e => e.Cities)
                      .WithOne(c => c.Country)
                      .HasForeignKey(c => c.CountryCode)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // City yapılandırması
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CityCode);

                entity.Property(e => e.CityDescription).IsRequired();

                entity.HasOne(e => e.Country)
                      .WithMany(c => c.Cities)
                      .HasForeignKey(e => e.CountryCode)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Districts)
                      .WithOne(d => d.City)
                      .HasForeignKey(d => d.CityCode)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // District yapılandırması
            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.DistrictCode);

                entity.Property(e => e.DistrictDescription).IsRequired();

                entity.HasOne(e => e.City)
                      .WithMany(c => c.Districts)
                      .HasForeignKey(e => e.CityCode)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Country)
                      .WithMany()
                      .HasForeignKey(e => e.CountryCode)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Sector yapılandırması
            modelBuilder.Entity<Sector>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired();
            });

            // AuthorizedPerson yapılandırması
            modelBuilder.Entity<AuthorizedPerson>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(e => e.Customers)
                      .WithMany(c => c.AuthorizedPersons)
                      .HasForeignKey(e => e.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // MeetingNote yapılandırması
            modelBuilder.Entity<MeetingNote>(entity =>
            {
                entity.HasKey(e => e.Id);

                

                entity.Property(e => e.Date)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.RowGuid)
                      .HasDefaultValueSql("NEWID()");

                entity.HasOne(e => e.Customers)
                      .WithMany(c => c.MeetingNotes)
                      .HasForeignKey(e => e.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

           
        }
    }
}