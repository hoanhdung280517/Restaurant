using RSSolution.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using RSSolution.Data.Configurations;
using RSSolution.Data.Extensions;

namespace RSSolution.Data.EF
{
    public class RSDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public RSDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new DistrictConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new MealCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new MealCategoryTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new MealConfiguration());
            modelBuilder.ApplyConfiguration(new MealImageConfiguration());
            modelBuilder.ApplyConfiguration(new MealInCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new MealTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionConfiguration());
            modelBuilder.ApplyConfiguration(new ProvinceConfiguration());
            modelBuilder.ApplyConfiguration(new SlideConfiguration());
            modelBuilder.ApplyConfiguration(new TableConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new BookTableConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

           modelBuilder.Seed();

            // base.OnModelCreating(modelBuilder);
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

        public DbSet<Promotions> Promotions { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<MealImage> MealImages { get; set; }

        public DbSet<Slide> Slides { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<BookTable> BookTables { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}