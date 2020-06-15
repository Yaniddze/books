using System;
using System.Collections.Generic;
using BooksApi.Entities;

namespace BooksApi.UnitTests.TestCQRS
{
    public class Storage
    {
        public List<Book> Books { get; set; } = new List<Book>
        {
            new Book
            {
                Id = Guid.Parse("8f2ae7c0-3399-4432-a79f-917ee18c15ea"),
                Author = new Author
                {
                    Id = Guid.Parse("e635b25f-a408-4db7-be15-3352b4d69425"),
                    Name = "Some Author"
                },
                Genre = new Genre()
                {
                    Id = Guid.Parse("ef1148ea-fc3a-46b5-850e-438d1107e257"),
                    Title = "Some genre"
                },
                BookInfo = new BookInfo
                {
                    Title = "Book title",
                    Year = 992,
                }
            },
            new Book
            {
                Id = Guid.Parse("173638aa-2ad7-467b-8a6c-272dd1eab8e3"),
                Author = new Author
                {
                    Id = Guid.Parse("ef1148ea-fc3a-46b5-850e-438d1107e257"),
                    Name = "Some Author 2"
                },
                Genre = new Genre()
                {
                    Id = Guid.Parse("dce3b0cf-e8c2-4430-af45-6e18df4596e7"),
                    Title = "Some genre 2"
                },
                BookInfo = new BookInfo
                {
                    Title = "Another book",
                    Year = 993,
                }
            }
        };
        public List<Author> Authors { get; set; } = new List<Author>
        {
            new Author
            {
                Id = Guid.Parse("e635b25f-a408-4db7-be15-3352b4d69425"),
                Name = "Some Author"
            },
            new Author
            {
                Id = Guid.Parse("ef1148ea-fc3a-46b5-850e-438d1107e257"),
                Name = "Some Author 2"
            }
        };
        public List<Genre> Genres { get; set; } = new List<Genre>
        {
            new Genre()
            {
                Id = Guid.Parse("ef1148ea-fc3a-46b5-850e-438d1107e257"),
                Title = "Some genre"
            },
            new Genre()
            {
                Id = Guid.Parse("dce3b0cf-e8c2-4430-af45-6e18df4596e7"),
                Title = "Some genre 2"
            }
        };

        public List<User> Users { get; set; } = new List<User>
        {
            new User()
            {
                Id = Guid.Parse("0ffcb912-c274-45a4-ab45-9b4a18caf5cf"),
                LoginInfo = new LoginInfo
                {
                    Login = "root",
                    Password = "root",
                },
            },
            new User()
            {
                Id = Guid.Parse("d93ef6c1-aa04-4dc4-82d7-71eaf12bfe86"),
                LoginInfo = new LoginInfo
                {
                    Login = "yaniddze",
                    Password = "somePass",
                },
            },
        };
    }
}