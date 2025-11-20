using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.G02.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress);
            builder.HasOne(O=> O.DeliveryMethod).WithMany().HasForeignKey(O => O.DeliveryMethodId);
            builder.HasMany(O => O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(O => O.Subtotal).HasColumnType("decimal(18,2)");
        }
    }

}
