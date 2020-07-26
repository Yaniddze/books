using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.UpdateBook
{
    public class UpdateBookUseCase : AbstractUseCase<UpdateBookRequest>
    {
        private readonly IFindQuery<Author> _authorFinder;
        private readonly IFindQuery<Genre> _genreFinder;
        private readonly IFindQuery<Book> _bookFinder;

        public UpdateBookUseCase(
            IValidator<UpdateBookRequest> validator,
            IFindQuery<Author> authorFinder,
            IFindQuery<Genre> genreFinder,
            IFindQuery<Book> bookFinder
        )
        : base(validator)
        {
            _authorFinder = authorFinder;
            _genreFinder = genreFinder;
            _bookFinder = bookFinder;
        }

        protected override async Task<AbstractAnswer> HandleAsync(UpdateBookRequest request, CancellationToken cancellationToken)
        {

            var foundedBook = await _bookFinder.FindOneAsync(x => x.Id == request.BookId);
            if (foundedBook == null)
            {
                return CreateBadAnswer(new[] {"Book not found. Use other id"});
            }
            
            var foundedAuthor = await _authorFinder.FindOneAsync(x => x.Id == request.NewAuthorId);
            if (foundedAuthor == null)
            {
                return CreateBadAnswer(new[] {"Author not found. Use other id"});
            }

            var foundedGenre = await _genreFinder.FindOneAsync(x => x.Id == request.NewGenreId);
            if (foundedGenre == null)
            {
                return CreateBadAnswer(new[] {"Genre not found. Use other id"});
            }

            return CreateSuccessAnswer();
        }
    }
}