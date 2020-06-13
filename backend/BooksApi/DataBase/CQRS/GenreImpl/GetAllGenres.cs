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
        private readonly ContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public GetAllGenres(IMapper mapper, ContextProvider contextProvider)
        {
            _mapper = mapper;
            _contextProvider = contextProvider;
        }

        public async Task<IEnumerable<Genre>> InvokeAsync()
        {
            using (var context = _contextProvider.GetContext())
            {
                return await context.Genres
                    .Select(x => _mapper.Map<GenreDB, Genre>(x))
                    .ToListAsync();
            }
        }
    }
}