using System;
using BooksApi.Entities.Abstractions;

namespace BooksApi.Entities
{
    public class Token: Entity
    {
        public bool Active { get; set; }
        public Guid UserId { get; set; }
    }
}