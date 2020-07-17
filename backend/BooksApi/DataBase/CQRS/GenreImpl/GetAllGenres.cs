using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.CQRS.Queries;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.DataBase.CQRS.GenreImpl
{
    public class GetAllGenres: IGetAllQuery<Genre>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public GetAllGenres(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<Genre>> InvokeAsync()
        {
            return await _context.Genres
                .Select(x => _mapper.Map<GenreDB, Genre>(x))
                .ToListAsync();
        }
    }
}