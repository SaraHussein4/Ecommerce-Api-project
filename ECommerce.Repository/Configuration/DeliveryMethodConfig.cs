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
    public class DeliveryMethodConfig : IEntityTypeConfiguration<Deliverymethod>
    {
        public void Configure(EntityTypeBuilder<Deliverymethod> builder)
        {
            builder.Property(o => o.Cost).HasColumnType("decimal(18,2)");
        }
    }
}
