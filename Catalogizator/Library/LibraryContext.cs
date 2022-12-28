using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogizator.Library
{
    internal class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<BookInfo> BookInfo { get; set; } = null!;
        public DbSet<BbkCode> BbkCodes { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Chapter> Chapters { get; set; } = null!;


        public LibraryContext()
        {
        //    Database.EnsureDeleted();
        //    Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=BooksCatalog; Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>()
                        .HasOne(book => book.Info)
                            .WithOne(info => info.Book)
                            .HasForeignKey<BookInfo>(info => info.IdBook);

            modelBuilder.Entity<Book>()
                        .HasOne(book => book.Author)
                            .WithMany(author => author.Books)
                            .HasForeignKey(book => book.IdAuthor);

            modelBuilder.Entity<Book>()
                        .HasOne(book => book.BbkCode)
                            .WithMany(code => code.Books)
                            .HasForeignKey(book => book.IdBbkCode);

            modelBuilder.Entity<Book>()
                        .HasMany(book => book.Genres)
                            .WithMany(genre => genre.Books);

            modelBuilder.Entity<Genre>()
                        .HasOne(genre => genre.Chapter)
                            .WithMany(chapter => chapter.Genres)
                            .HasForeignKey(genre => genre.IdChapter);

            modelBuilder.Entity<Book>().HasIndex(book => book.Isbn).IsUnique();
        }
    }
}
