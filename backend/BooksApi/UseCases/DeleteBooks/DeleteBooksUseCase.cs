using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using FluentValidation;
using MediatR;

namespace BooksApi.UseCases.DeleteBooks
{
    public class DeleteBooksUseCase : IRequestHandler<DeleteBooksRequest, DeleteBooksAnswer>
    {
        private readonly IValidator<DeleteBooksRequest> _validator;
        private readonly ICommandHandler<DeleteBooksCommand> _handler;

        public DeleteBooksUseCase(IValidator<DeleteBooksRequest> validator, ICommandHandler<DeleteBooksCommand> handler)
        {
            _validator = validator;
            _handler = handler;
        }

        public async Task<DeleteBooksAnswer> Handle(DeleteBooksRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new DeleteBooksAnswer
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage)
                };
            }

            await _handler.HandleAsync(new DeleteBooksCommand {BookIds = request.BookIds});
            
            return new DeleteBooksAnswer
            {
                Success = true,
            };
        }
    }
}