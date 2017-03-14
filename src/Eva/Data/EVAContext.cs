using Microsoft.EntityFrameworkCore;
using EVA.Models;
using EVA.Models.ManageViewModels;

namespace EVA.Data
{
    public class EVAContext : DbContext
    {

        public EVAContext(DbContextOptions<EVAContext> options) : base(options)
        {

        }

        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Equipment> Equipments { get; set; }


        public DbSet<WaterBody> WaterBodys { get; set; }
        public DbSet<Site> Sites { get; set; }

        public DbSet<SiteType>SiteTypes {get;set;}

        public DbSet<Location> Locations { get; set; }

        public DbSet<PoolType> PoolTypes { get; set; }

        public DbSet<EqType> EqTypes { get; set; }

        public DbSet<Construction> Constructions { get; set; }

        public DbSet<Make> Makes { get; set; }

        public DbSet<HealthCategory> HealthCategorys { get; set; }

        public DbSet<HealthFirstAid> HealthFirstAids { get; set; }

        public DbSet<HealthGroup> HealthGroups { get; set; }

        public DbSet<HealthGroupToAid> HealthGroupToAids { get; set; }

        public DbSet<HealthFirstAidKit> HealthFirstAidKits { get; set; }

        public DbSet<HealthGroupToKit> HealthGroupToKits { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<ContactType> ContactTypes { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientContact> ClientContacts { get; set; }
        
        public DbSet<ClientContactDetail> ClientContactDetails { get; set; }

        public DbSet<Division> Divisions { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<HealthToilet> HealthToilets { get; set; }

        public DbSet<HealthWaterTestingFrequency> HealthWaterTestingFrequencys { get; set; }

        public DbSet<Job> Job { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Job>()
            //     .HasMany(typeof(Site),  "Site")
            //    .WithOptional()
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment>()
                .Property(d => d.InspectDate)
                .HasDefaultValueSql("getDate()");

           

            modelBuilder.Entity<Job>()
                .Property(d=>d.BookingDate)
                .HasDefaultValueSql("getDate()");

            modelBuilder.Entity<Condition>().ToTable("Condition");
            modelBuilder.Entity<Equipment>().ToTable("Equipment");
            modelBuilder.Entity<WaterBody>().ToTable("WaterBody");
            modelBuilder.Entity<Site>().ToTable("Site");
            modelBuilder.Entity<SiteType>().ToTable("SiteType");
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<PoolType>().ToTable("PoolType");
            modelBuilder.Entity<EqType>().ToTable("EqType");
            modelBuilder.Entity<Construction>().ToTable("Construction");
            modelBuilder.Entity<Make>().ToTable("Make");

        }

       
    }
}
