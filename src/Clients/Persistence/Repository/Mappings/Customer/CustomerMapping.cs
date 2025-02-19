using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", "dbo");

            builder.HasKey(e => e.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Logo).IsRequired().HasMaxLength(150);
            builder.Property(c => c.CreateDate);
            builder.Property(c => c.UpdateDate);

            builder.HasMany(c => c.Users).WithOne(u => u.Customer).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(c => c.Places).WithOne(d => d.Customer).HasForeignKey(d => d.CustomerId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
