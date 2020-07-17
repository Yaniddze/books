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
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetAllAuthors(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<Author>> InvokeAsync()
        {
            return await _context.Authors
                .Select(x => _mapper.Map<AuthorDB, Author>(x))
                .ToListAsync();
        }
    }
}