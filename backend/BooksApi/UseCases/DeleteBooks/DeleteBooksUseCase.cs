using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.DeleteBooks
{
    public class DeleteBooksUseCase : AbstractUseCase<DeleteBooksRequest>
    {
        private readonly ICommandHandler<DeleteBooksCommand> _handler;

        public DeleteBooksUseCase(IValidator<DeleteBooksRequest> validator, ICommandHandler<DeleteBooksCommand> handler)
        :base(validator)
        {
            _handler = handler;
        }

        protected override async Task<AbstractAnswer> HandleAsync(DeleteBooksRequest request, CancellationToken cancellationToken)
        {
            await _handler.HandleAsync(new DeleteBooksCommand {BookIds = request.BookIds});

            return CreateSuccessAnswer();
        }
    }
}