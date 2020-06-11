using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BooksApi.Entities.Abstractions;

namespace BooksApi.CQRS.Queries
{
    public interface IFindQuery<TEntity>
        where TEntity: Entity
    {
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> pattern);
        Task<IEnumerable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> pattern);
    }
}