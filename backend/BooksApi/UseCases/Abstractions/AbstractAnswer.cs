using System.Collections.Generic;

namespace BooksApi.UseCases.Abstractions
{
    public class AbstractAnswer<TData>
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public TData Data { get; set; }
    }
}