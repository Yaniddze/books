using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace BooksApi.UseCases.Abstractions
{
    public abstract class AbstractUseCase<TRequest, TResponse>: IRequestHandler<TRequest, AbstractAnswer<TResponse>>
        where TRequest: IRequest<AbstractAnswer<TResponse>>
    {
        private readonly IValidator<TRequest> _validator;

        protected AbstractUseCase(IValidator<TRequest> validator)
        {
            _validator = validator;
        }
        
        public async Task<AbstractAnswer<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return CreateBadAnswer(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            
            return await HandleAsync(request, cancellationToken);
        }

        protected abstract Task<AbstractAnswer<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken);

        protected AbstractAnswer<TResponse> CreateBadAnswer(IEnumerable<string> errors)
        {
            return new AbstractAnswer<TResponse>
            {
                Errors = errors,
                Success = false,
            };
        }

        protected AbstractAnswer<TResponse> CreateSuccessAnswer(TResponse data)
        {
            return new AbstractAnswer<TResponse>
            {
                Data = data,
                Success = true,
            };
        }
    }
    
    public abstract class AbstractUseCase<TRequest>: IRequestHandler<TRequest, AbstractAnswer>
        where TRequest: IRequest<AbstractAnswer>
    {
        private readonly IValidator<TRequest> _validator;

        protected AbstractUseCase(IValidator<TRequest> validator)
        {
            _validator = validator;
        }
        
        public async Task<AbstractAnswer> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return CreateBadAnswer(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            
            return await HandleAsync(request, cancellationToken);
        }

        protected abstract Task<AbstractAnswer> HandleAsync(TRequest request, CancellationToken cancellationToken);

        protected AbstractAnswer CreateBadAnswer(IEnumerable<string> errors)
        {
            return new AbstractAnswer
            {
                Errors = errors,
                Success = false,
            };
        }

        protected AbstractAnswer CreateSuccessAnswer()
        {
            return new AbstractAnswer
            {
                Success = true,
            };
        }
    }
}