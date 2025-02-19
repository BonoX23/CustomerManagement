using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");

            builder.HasKey(e => e.Id);
            builder.Property(c => c.Login).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Password).IsRequired().HasMaxLength(250);
            builder.Property(c => c.Actived).IsRequired();
            builder.Property(c => c.CreateDate);
            builder.Property(c => c.UpdateDate);

            builder.HasOne(c => c.Customer).WithMany(u => u.Users).HasForeignKey(x => x.CustomerId);
        }
    }
}
