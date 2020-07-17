using BooksApi.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.DataBase.Context
{
    public interface IContext
    {
        DbSet<TokenDB> Tokens { get; set; }
        DbSet<AuthorDB> Authors { get; set; }
        DbSet<BookDB> Books { get; set; }
        DbSet<GenreDB> Genres { get; set; }
        DbSet<UserDB> Users { get; set; }
        
    }
}