using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.CQRS.Queries;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.DataBase.CQRS.AuthorImpl
{
    public class GetAllAuthors: IGetAllQuery<Author>
    {
        private readonly ContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public GetAllAuthors(ContextProvider contextProvider, IMapper mapper)
        {
            _contextProvider = contextProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Author>> InvokeAsync()
        {
            using (var context = _contextProvider.GetContext())
            {
                return await context.Authors
                    .Select(x => _mapper.Map<AuthorDB, Author>(x))
                    .ToListAsync();
            }
        }
    }
}