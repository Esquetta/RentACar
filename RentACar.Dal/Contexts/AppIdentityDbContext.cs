using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.Entities.Concrete;

namespace RentACar.Dal.Contexts
{
    public class AppIdentityDbContext : IdentityDbContext<Customer, IdentityRole, string>
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<CarColor> CarColors { get; set; }
        public DbSet<GearBox> GearBoxes { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<RentDetail> RentDetails { get; set; }


        public AppIdentityDbContext()
        {

        }
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-EMPIGDT;Initial Catalog=RentACar;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RentDetail>().HasKey(x => new { x.RentId, x.CarId });
            modelBuilder.Entity<Brand>().HasIndex(p => p.BrandName).IsUnique();
        }

    }
}
