using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Repository.Mappings;

namespace Repository.Contexts
{
    public class CustomerDbContext : DbContext, IUnitOfWork
    {
        #region DbSet

        public DbSet<User> User { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Address> Address { get; set; }

        #endregion

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new CustomerMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new AddressMapping());

            modelBuilder.Entity<Customer>().HasIndex(t => t.Email).IsUnique(true);
            modelBuilder.Entity<User>().HasIndex(t => t.Login).IsUnique(true);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            var entitiesAdded = new List<EntityEntry>();
            var entitiesModified = new List<EntityEntry>();
            var entitiesDeleted = new List<EntityEntry>();

            foreach (var entry in this.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added: entitiesAdded.Add(entry); break;
                    case EntityState.Modified: entitiesModified.Add(entry); break;
                    case EntityState.Deleted: entitiesDeleted.Add(entry); break;
                }
            }

            var save = await SaveChangesAsync() > 0;

            return save;
        }

        public async Task<int> SaveChangesAsync()
        {
            foreach (var entries in ChangeTracker.Entries<IEntityBase>())
            {
                if (entries.Entity != null)
                {
                    if (entries.State == EntityState.Added)
                        entries.Entity.CreateDate = DateTime.Now;

                    if (entries.State == EntityState.Modified)
                        entries.Entity.UpdateDate = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync();
        }
    }
}
