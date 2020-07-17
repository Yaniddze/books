using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.UpdateBook
{
    public class UpdateBookUseCase : AbstractUseCase<UpdateBookRequest>
    {
        private readonly ICommandHandler<UpdateBookCommand> _handler;
        private readonly IFindQuery<Author> _authorFinder;
        private readonly IFindQuery<Genre> _genreFinder;
        private readonly IFindQuery<Book> _bookFinder;
        private readonly IMapper _mapper;

        public UpdateBookUseCase(
            IValidator<UpdateBookRequest> validator,
            ICommandHandler<UpdateBookCommand> handler,
            IMapper mapper, IFindQuery<Author> authorFinder,
            IFindQuery<Genre> genreFinder,
            IFindQuery<Book> bookFinder
        )
        : base(validator)
        {
            _handler = handler;
            _mapper = mapper;
            _authorFinder = authorFinder;
            _genreFinder = genreFinder;
            _bookFinder = bookFinder;
        }

        protected override async Task<AbstractAnswer> HandleAsync(UpdateBookRequest request, CancellationToken cancellationToken)
        {
            var mappedCommand = _mapper.Map<UpdateBookRequest, UpdateBookCommand>(request);

            var foundedBook = await _bookFinder.FindOneAsync(x => x.Id == mappedCommand.BookId);
            if (foundedBook == null)
            {
                return CreateBadAnswer(new[] {"Book not found. Use other id"});
            }
            
            var foundedAuthor = await _authorFinder.FindOneAsync(x => x.Id == mappedCommand.NewAuthorId);
            if (foundedAuthor == null)
            {
                return CreateBadAnswer(new[] {"Author not found. Use other id"});
            }

            var foundedGenre = await _genreFinder.FindOneAsync(x => x.Id == mappedCommand.NewGenreId);
            if (foundedGenre == null)
            {
                return CreateBadAnswer(new[] {"Genre not found. Use other id"});
            }

            await _handler.HandleAsync(mappedCommand);

            return CreateSuccessAnswer();
        }
    }
}