using RSSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Configurations
{
    public class MealImageConfiguration : IEntityTypeConfiguration<MealImage>
    {
        public void Configure(EntityTypeBuilder<MealImage> builder)
        {
            builder.ToTable("MealImages");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.Caption).HasMaxLength(200);

            builder.HasOne(x => x.Meal).WithMany(x => x.MealImages).HasForeignKey(x => x.MealId);
        }
    }
}
