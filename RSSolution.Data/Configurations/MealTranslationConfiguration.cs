using RSSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Configurations
{
    public class MealTranslationConfiguration : IEntityTypeConfiguration<MealTranslation>
    {
        public void Configure(EntityTypeBuilder<MealTranslation> builder)
        {
            builder.ToTable("MealTranslations");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

            builder.Property(x => x.SeoAlias).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Details).HasMaxLength(500);

            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);

            builder.HasOne(x => x.Language).WithMany(x => x.MealTranslations).HasForeignKey(x => x.LanguageId);

            builder.HasOne(x => x.Meal).WithMany(x => x.MealTranslations).HasForeignKey(x => x.MealId);

        }
    }
}
