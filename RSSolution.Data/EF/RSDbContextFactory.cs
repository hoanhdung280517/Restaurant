﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RSSolution.Data.EF
{
    public class RSDbContextFactory : IDesignTimeDbContextFactory<RSDbContext>
    {
        public RSDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<RSDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new RSDbContext(optionsBuilder.Options);
        }
    }
}
