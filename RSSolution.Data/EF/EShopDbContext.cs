using RSSolution.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.EF
{
    public class RSDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public RSDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealCategory> MealCategories { get; set; }

        public DbSet<AppConfig> AppConfigs { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<MealCategoryTranslation> MealCategoryTranslations { get; set; }
        public DbSet<MealInCategory> MealInCategories { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<MealTranslation> MealTranslations { get; set; }

        public DbSet<Promotion> Promotions { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<MealImage> MealImages { get; set; }

        public DbSet<Slide> Slides { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Province> Provinces { get; set; }
    }
}