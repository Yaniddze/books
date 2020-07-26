using System.Threading;
using System.Threading.Tasks;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.DeleteBooks
{
    public class DeleteBooksUseCase : AbstractUseCase<DeleteBooksRequest>
    {

        public DeleteBooksUseCase(IValidator<DeleteBooksRequest> validator)
        :base(validator) { }

        protected override async Task<AbstractAnswer> HandleAsync(DeleteBooksRequest request, CancellationToken cancellationToken)
        {
            return CreateSuccessAnswer();
        }
    }
}