using Book_control_system.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Book_control_system.Data
{
    public class BookControlSystemContext:DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasKey(t => new { t.AuthorId, t.BookId });
            modelBuilder.Entity<BookAuthor>().HasOne(pt => pt.Book).WithMany(p => p.BookAuthors).HasForeignKey(pt => pt.BookId);
            modelBuilder.Entity<BookAuthor>().HasOne(pt => pt.Author).WithMany(p => p.BookAuthors).HasForeignKey(pt => pt.AuthorId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
