using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSSolution.Data.Entities;
using RSSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Data.Configurations
{
    public class BookTableConfiguration : IEntityTypeConfiguration<BookTable>
    {
        public void Configure(EntityTypeBuilder<BookTable> builder)
        {
            builder.ToTable("BookTables");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Note);
            builder.Property(x => x.CountAdults);
            builder.Property(x => x.CountChilds);
            builder.Property(x => x.Status).HasDefaultValue(BookTableStatus.InProgress);
            builder.HasOne(x => x.User).WithMany(x => x.bookTables).HasForeignKey(x => x.UserId);
        }
    }
}
