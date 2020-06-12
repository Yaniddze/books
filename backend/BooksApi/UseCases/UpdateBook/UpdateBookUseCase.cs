using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using FluentValidation;
using MediatR;

namespace BooksApi.UseCases.UpdateBook
{
    public class UpdateBookUseCase : IRequestHandler<UpdateBookRequest, UpdateBookAnswer>
    {
        private readonly IValidator<UpdateBookRequest> _validator;
        private readonly ICommandHandler<UpdateBookCommand> _handler;
        private readonly IFindQuery<Author> _authorFinder;
        private readonly IFindQuery<Genre> _genreFinder;
        private readonly IMapper _mapper;

        public UpdateBookUseCase(
            IValidator<UpdateBookRequest> validator,
            ICommandHandler<UpdateBookCommand> handler,
            IMapper mapper, IFindQuery<Author> authorFinder, IFindQuery<Genre> genreFinder)
        {
            _validator = validator;
            _handler = handler;
            _mapper = mapper;
            _authorFinder = authorFinder;
            _genreFinder = genreFinder;
        }

        public async Task<UpdateBookAnswer> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new UpdateBookAnswer
                {
                    Success = true,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage),
                };
            }

            var mappedCommand = _mapper.Map<UpdateBookRequest, UpdateBookCommand>(request);

            var foundedAuthor = await _authorFinder.FindOneAsync(x => x.Id == mappedCommand.NewAuthorId);
            if (foundedAuthor == null)
            {
                return new UpdateBookAnswer
                {
                    Success = false,
                    Errors = new []{ "Author not found. Use other id" }
                };
            }
            
            var foundedGenre = await _genreFinder.FindOneAsync(x => x.Id == mappedCommand.NewGenreId);
            if (foundedGenre == null)
            {
                return new UpdateBookAnswer
                {
                    Success = false,
                    Errors = new []{ "Genre not found. Use other id" }
                };
            }

            await _handler.HandleAsync(mappedCommand);
            
            return new UpdateBookAnswer
            {
                Success = true,
            };
        }
    }
}