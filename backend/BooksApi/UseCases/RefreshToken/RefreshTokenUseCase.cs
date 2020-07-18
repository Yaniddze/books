using System;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using BooksApi.UseCases.Abstractions;
using FluentValidation;

namespace BooksApi.UseCases.RefreshToken
{
    public class RefreshTokenUseCase: AbstractUseCase<RefreshTokenRequest, Guid>
    {
        private readonly IFindQuery<Token> _finder;
        public RefreshTokenUseCase(
            IValidator<RefreshTokenRequest> validator,
            IFindQuery<Token> finder
            ) 
            : base(validator)
        {
            _finder = finder;
        }

        protected override async Task<AbstractAnswer<Guid>> HandleAsync(
            RefreshTokenRequest request, CancellationToken cancellationToken
        )
        {
            var founded = await _finder.FindOneAsync(x => x.Id == request.OldTokenId);

            if (founded == null)
            {
                return CreateBadAnswer(new[] { "Your token doesn't exists" });
            }

            if (!founded.Active)
            {
                return CreateBadAnswer(new[] { "Token is not active" });
            }

            return CreateSuccessAnswer(founded.Id);
        }
    }
}