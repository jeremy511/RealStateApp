
using Microsoft.EntityFrameworkCore;
using RealState.Core.Domain.Common;
using RealState.Core.Domain.Entities;

namespace RealState.Infrastucture.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Ads> Ads { get; set; }
        public DbSet<AdsType> AdsTypes { get; set; }
        public DbSet<AdsUpgrates> AdsUpgrates { get; set; }
        public DbSet<FavouriteProperties> FavouriteProperties { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellation = new())
        {
            foreach (var item in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.CreatedBy = "Default";
                        item.Entity.CreatedDate = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        item.Entity.ModifiedBy = "Default";
                        item.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellation);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region "Tables"

            modelBuilder.Entity<Ads>()
                        .ToTable("Ads");

            modelBuilder.Entity<AdsType>()
                        .ToTable("AdsTypes");

            modelBuilder.Entity<Sales>()
                        .ToTable("Sales");

            modelBuilder.Entity<AdsUpgrates>()
                        .ToTable("AdsUpgrates");

            modelBuilder.Entity<FavouriteProperties>()
                        .ToTable("FavouriteProperties");

            #endregion
            #region "Primary Key"
            modelBuilder.Entity<Ads>().HasKey(a => a.Id);
            modelBuilder.Entity<AdsType>().HasKey(a => a.Id);
            modelBuilder.Entity<AdsUpgrates>().HasKey(a => a.Id);
            modelBuilder.Entity<Sales>().HasKey(s => s.Id);

            modelBuilder.Entity<FavouriteProperties>().HasKey(f => f.Id);

            #endregion
            #region "Relations"

            modelBuilder.Entity<Ads>()
                .HasMany<FavouriteProperties>(f => f.FavouriteProperties)
                .WithOne(f => f.Ads)
                .HasForeignKey(f => f.PropetyId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<AdsType>()
              .HasMany<Ads>(a => a.Ads)
              .WithOne(a => a.AdsType)
              .HasForeignKey(a => a.AdsTypeId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Sales>()
             .HasMany<Ads>(a => a.Ads)
             .WithOne(a => a.Sales)
             .HasForeignKey(a => a.SalesId)
             .OnDelete(DeleteBehavior.Cascade);

            #endregion
            #region "Properties Configuration"

            modelBuilder.Entity<FavouriteProperties>().Property(f => f.UserName).IsRequired();

            modelBuilder.Entity<Ads>().Property(f => f.Description).IsRequired();
            modelBuilder.Entity<Ads>().Property(s => s.SalesId).IsRequired();
            modelBuilder.Entity<Ads>().Property(f => f.AdsTypeId).IsRequired();
            modelBuilder.Entity<Ads>().Property(f => f.Price).IsRequired();
            modelBuilder.Entity<Ads>().Property(f => f.BathRooms).IsRequired();
            modelBuilder.Entity<Ads>().Property(f => f.BedRooms).IsRequired();
            modelBuilder.Entity<Ads>().Property(f => f.Size).IsRequired();

            modelBuilder.Entity<AdsType>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<AdsType>().Property(s => s.Description).IsRequired();

            modelBuilder.Entity<Sales>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Sales>().Property(s => s.Description).IsRequired();

            modelBuilder.Entity<AdsUpgrates>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<AdsUpgrates>().Property(s => s.Description).IsRequired();






            #endregion
        }

    }
}
