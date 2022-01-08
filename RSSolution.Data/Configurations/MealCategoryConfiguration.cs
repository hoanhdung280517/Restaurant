using RSSolution.Data.Entities;
using RSSolution.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Configurations
{
    public class MealCategoryConfiguration : IEntityTypeConfiguration<MealCategory>
    {
        public void Configure(EntityTypeBuilder<MealCategory> builder)
        {
            builder.ToTable("MealCategories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Status).HasDefaultValue(Status.Still);
        }
    }
}
