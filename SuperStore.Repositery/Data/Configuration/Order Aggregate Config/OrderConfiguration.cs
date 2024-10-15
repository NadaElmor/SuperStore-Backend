using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperStore.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Repositery.Data.Configuration.Order_Aggregate_Config
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //enum status conversion
            builder.Property(s => s.Status).HasConversion(st => st.ToString(),st=>(OrderStatus)Enum.Parse(typeof(OrderStatus),st));
            //shipping Address
            builder.OwnsOne(O => O.ShippingAddress,sa=>sa.WithOwner());
            //delivery method
            builder.HasOne(O=>O.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
            //decimal
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
        }
    }
}
