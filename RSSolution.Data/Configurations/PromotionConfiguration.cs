using RSSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotions>
    {
        public void Configure(EntityTypeBuilder<Promotions> builder)
        {
            builder.ToTable("Promotions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.FromDate);
            builder.Property(x => x.ToDate);
            builder.Property(x => x.DiscountPercent).IsRequired();
            builder.Property(x => x.Code).IsRequired();

        }
    }
}
