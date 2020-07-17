using System;
using System.Threading.Tasks;
using BooksApi.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BooksApi.DataBase.Context
{
    public class MyContext: DbContext, IContext
    {
        public DbSet<AuthorDB> Authors { get; set; }
        public DbSet<GenreDB> Genres { get; set; }
        public DbSet<BookDB> Books { get; set; }
        public DbSet<UserDB> Users { get; set; }
        public DatabaseFacade DataBaseFacade => Database;
        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        public DbSet<TokenDB> Tokens { get; set; }

        public MyContext(DbContextOptions options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Primary keys

            modelBuilder.Entity<BookDB>()
                .ToTable("books")
                .HasKey(x => x.Id);

            modelBuilder.Entity<GenreDB>()
                .ToTable("genre")
                .HasKey(x => x.Id);

            modelBuilder.Entity<AuthorDB>()
                .ToTable("author")
                .HasKey(x => x.Id);

            modelBuilder.Entity<UserDB>()
                .ToTable("users")
                .HasKey(x => x.Id);

            modelBuilder.Entity<TokenDB>()
                .ToTable("tokens")
                .HasKey(x => x.Id);

            #endregion


            #region Foreign keys

            modelBuilder.Entity<BookDB>()
                .HasOne(book => book.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.AuthorId);
            
            modelBuilder.Entity<BookDB>()
                .HasOne(book => book.Genre)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.GenreId);

            modelBuilder.Entity<TokenDB>()
                .HasOne(token => token.User)
                .WithMany(user => user.Tokens)
                .HasForeignKey(token => token.UserId);

            #endregion


            #region Props to columns

            #region Book

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

            #endregion

            #region Author

            modelBuilder.Entity<AuthorDB>()
                .Property(x => x.Id)
                .HasColumnName("id");
            
            modelBuilder.Entity<AuthorDB>()
                .Property(x => x.Name)
                .HasColumnName("name");

            #endregion

            #region Genre

            modelBuilder.Entity<GenreDB>()
                .Property(x => x.Id)
                .HasColumnName("id");
            
            modelBuilder.Entity<GenreDB>()
                .Property(x => x.Title)
                .HasColumnName("title");

            #endregion

            #region User

            modelBuilder.Entity<UserDB>()
                .Property(x => x.Id)
                .HasColumnName("id");
            
            modelBuilder.Entity<UserDB>()
                .Property(x => x.Login)
                .HasColumnName("login");
            
            modelBuilder.Entity<UserDB>()
                .Property(x => x.Password)
                .HasColumnName("password");

            #endregion

            #region Token

            modelBuilder.Entity<TokenDB>()
                .Property(x => x.Id)
                .HasColumnName("id");
                
            modelBuilder.Entity<TokenDB>()
                .Property(x => x.CreationDate)
                .HasColumnName("creation_date");
                
            modelBuilder.Entity<TokenDB>()
                .Property(x => x.ExpiryDate)
                .HasColumnName("expiry_date");
                
            modelBuilder.Entity<TokenDB>()
                .Property(x => x.JwtId)
                .HasColumnName("jwt_id");
                
            modelBuilder.Entity<TokenDB>()
                .Property(x => x.TokenValue)
                .HasColumnName("token_value");
                
            modelBuilder.Entity<TokenDB>()
                .Property(x => x.UserId)
                .HasColumnName("user_id");

            #endregion

            #endregion
        }
    }
}