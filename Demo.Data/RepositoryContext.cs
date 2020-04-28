using System.Collections.Generic;
using Demo.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(item =>
                {
                    item.HasKey(i => i.ItemId);

                    item.Property(i => i.ItemId).ValueGeneratedNever();

                    item.Property(i => i.Title)
                        .IsRequired()
                        .HasMaxLength(254);

                    item.HasIndex(i => i.Title)
                        .IsUnique(false);

                    item
                        .HasOne(i => (Item)i.Parent)
                        .WithMany(i => (IEnumerable<Item>) i.Childs)
                        .HasForeignKey(i => i.ParentId);
                }

            );
        }

        public DbSet<Item> Items { get; set; }
    }
}
