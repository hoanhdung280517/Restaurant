using RSSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(x => new { x.OrderId, x.MealId });

            builder.HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId);
            builder.HasOne(x => x.Meal).WithMany(x => x.OrderDetails).HasForeignKey(x => x.MealId);
        }
    }
}
