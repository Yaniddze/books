using System.Collections.Generic;
using System.Threading.Tasks;
using BooksApi.Entities.Abstractions;

namespace BooksApi.CQRS.Queries
{
    public interface IGetAllQuery<TEntity>
        where TEntity: Entity
    {
        Task<IEnumerable<TEntity>> InvokeAsync();
    }
}