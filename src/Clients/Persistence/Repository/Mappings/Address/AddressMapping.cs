using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address", "dbo");

            builder.HasKey(e => e.Id);
            builder.Property(c => c.Place).IsRequired().HasMaxLength(100);
            builder.Property(c => c.CreateDate);
            builder.Property(c => c.UpdateDate);

            builder.HasOne(c => c.Customer).WithMany().HasForeignKey(x => x.CustomerId);
        }
    }
}
