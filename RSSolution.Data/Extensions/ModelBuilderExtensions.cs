using RSSolution.Data.Entities;
using RSSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig() { Key = "HomeTitle", Value = "This is home page of RSSolution" },
               new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of RSSolution" },
               new AppConfig() { Key = "HomeDescription", Value = "This is description of RSSolution" }
               );
            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en", Name = "English", IsDefault = false });

            modelBuilder.Entity<MealCategory>().HasData(
                new MealCategory()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Still,
                },
                 new MealCategory()
                 {
                     Id = 2,
                     IsShowOnHome = true,
                     ParentId = null,
                     SortOrder = 2,
                     Status = Status.Over
                 });

            modelBuilder.Entity<MealCategoryTranslation>().HasData(
                  new MealCategoryTranslation() { Id = 1, MealCategoryId = 1, Name = "Món nướng", LanguageId = "vi", SeoAlias = "mon-nuong", SeoDescription = "Các món ăn nướng", SeoTitle = "Món ăn nướng" },
                  new MealCategoryTranslation() { Id = 2, MealCategoryId = 1, Name = "BBQ", LanguageId = "en", SeoAlias = "grilled-dishes", SeoDescription = "The grilled dishes", SeoTitle = "The grilled dishes" },
                  new MealCategoryTranslation() { Id = 3, MealCategoryId = 2, Name = "Nước", LanguageId = "vi", SeoAlias = "nuoc", SeoDescription = "Các loại đồ uống", SeoTitle = "Các loại đồ uống" },
                  new MealCategoryTranslation() { Id = 4, MealCategoryId = 2, Name = "Water", LanguageId = "en", SeoAlias = "water", SeoDescription = "The Beverages", SeoTitle = "The Beverages" }
                    );

            modelBuilder.Entity<Meal>().HasData(
           new Meal()
           {
               Id = 1,
               DateCreated = DateTime.Now,
               Price = 200000,
               ViewCount = 0,
           });
            modelBuilder.Entity<MealTranslation>().HasData(
                 new MealTranslation()
                 {
                     Id = 1,
                     MealId = 1,
                     Name = "Bò bít tết",
                     LanguageId = "vi",
                     SeoAlias = "bo-bit-tet",
                     SeoDescription = "Bò bít tết",
                     SeoTitle = "Bò bít tết",
                     Details = "Bò bít tết",
                     Description = "Bò bít tết"
                 },
                    new MealTranslation()
                    {
                        Id = 2,
                        MealId = 1,
                        Name = "Beef Steak",
                        LanguageId = "en",
                        SeoAlias = "Beef-Steak",
                        SeoDescription = "Beef Steak",
                        SeoTitle = "Beef Steak",
                        Details = "Beef Steak",
                        Description = "Beef Steak"
                    });
            modelBuilder.Entity<MealInCategory>().HasData(
                new MealInCategory() { MealId = 1, MealCategoryId = 1 }
                );

            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });
            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "hoanhdung280517@gmail.com",
                NormalizedEmail = "hoanhdung280517@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Dung@280517"),
                SecurityStamp = string.Empty,
                FirstName = "Dung",
                LastName = "Ho",
                Dob = new DateTime(2020, 01, 31),
                DistrictId = 1,
                ProvinceId = 1

            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

            modelBuilder.Entity<Slide>().HasData(
              new Slide() { Id = 1, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 1, Url = "#", Image = "/themes/images/carousel/1.png", Status = Status.Still },
              new Slide() { Id = 2, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 2, Url = "#", Image = "/themes/images/carousel/2.png", Status = Status.Still },
              new Slide() { Id = 3, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 3, Url = "#", Image = "/themes/images/carousel/3.png", Status = Status.Still },
              new Slide() { Id = 4, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 4, Url = "#", Image = "/themes/images/carousel/4.png", Status = Status.Still },
              new Slide() { Id = 5, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 5, Url = "#", Image = "/themes/images/carousel/5.png", Status = Status.Still },
              new Slide() { Id = 6, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 6, Url = "#", Image = "/themes/images/carousel/6.png", Status = Status.Over }
              );
        }
    }
}