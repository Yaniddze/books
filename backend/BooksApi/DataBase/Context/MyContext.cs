using System;
using BooksApi.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.DataBase.Context
{
    public class MyContext: DbContext
    {
        public DbSet<AuthorDB> Authors { get; set; }
        public DbSet<GenreDB> Genres { get; set; }
        public DbSet<BookDB> Books { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                                     throw new ArgumentNullException());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Primary Keys
            modelBuilder.Entity<BookDB>()
                .ToTable("books")
                .HasKey(x => x.Id);

            modelBuilder.Entity<GenreDB>()
                .ToTable("genre")
                .HasKey(x => x.Id);

            modelBuilder.Entity<AuthorDB>()
                .ToTable("author")
                .HasKey(x => x.Id);
            
            // Foreign keys
            modelBuilder.Entity<BookDB>()
                .HasOne(book => book.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.AuthorId);
            
            modelBuilder.Entity<BookDB>()
                .HasOne(book => book.Genre)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.GenreId);
            
            // Map column name to prop
            modelBuilder.Entity<BookDB>()
                .Property(x => x.AuthorId)
                .HasColumnName("author_id");
            
            modelBuilder.Entity<BookDB>()
                .Property(x => x.Id)
                .HasColumnName("id");
            
            modelBuilder.Entity<BookDB>()
                .Property(x => x.Title)
                .HasColumnName("title");
            
            modelBuilder.Entity<BookDB>()
                .Property(x => x.Year)
                .HasColumnName("year");

            modelBuilder.Entity<BookDB>()
                .Property(x => x.GenreId)
                .HasColumnName("genre_id");
            
            modelBuilder.Entity<AuthorDB>()
                .Property(x => x.Id)
                .HasColumnName("id");
            
            modelBuilder.Entity<AuthorDB>()
                .Property(x => x.Name)
                .HasColumnName("name");
            
            modelBuilder.Entity<GenreDB>()
                .Property(x => x.Id)
                .HasColumnName("id");
            
            modelBuilder.Entity<GenreDB>()
                .Property(x => x.Title)
                .HasColumnName("title");
        }
    }
}