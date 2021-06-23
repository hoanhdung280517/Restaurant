using RSSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Configurations
{
    public class MealInCategoryConfiguration : IEntityTypeConfiguration<MealInCategory>
    {
        public void Configure(EntityTypeBuilder<MealInCategory> builder)
        {
            builder.HasKey(t => new {t.MealCategoryId, t.MealId });

            builder.ToTable("MealInCategories");

            builder.HasOne(t => t.Meal).WithMany(pc => pc.MealInCategories)
                .HasForeignKey(pc=>pc.MealId);

            builder.HasOne(t => t.MealCategory).WithMany(pc => pc.MealInCategories)
              .HasForeignKey(pc => pc.MealCategoryId);
        }
    }
}
