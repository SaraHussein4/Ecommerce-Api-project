using ECommerce.Core.model.OrderAggrgate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Configuration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.Status).HasConversion(Ostatus => Ostatus.ToString(),Ostatus=> (OrderStatus)Enum.Parse(typeof(OrderStatus), Ostatus));
            builder.OwnsOne(o => o.ShippingAddress, SA => SA.WithOwner());
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
      builder.HasOne(o=>o.Deliverymethod).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
