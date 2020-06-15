using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BooksApi.CQRS.Queries;
using BooksApi.Entities;
using FluentValidation;
using MediatR;

namespace BooksApi.UseCases.Login
{
    public class LoginUseCase: IRequestHandler<LoginRequest, LoginAnswer>
    {
        private readonly IFindQuery<User> _findUser;
        private readonly IValidator<LoginRequest> _validator;

        public LoginUseCase(IFindQuery<User> findUser, IValidator<LoginRequest> validator)
        {
            _findUser = findUser;
            _validator = validator;
        }

        public async Task<LoginAnswer> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new LoginAnswer
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage),
                };
            }

            var foundedUser = await _findUser.FindOneAsync(x =>
                x.LoginInfo.Login == request.Login && x.LoginInfo.Password == request.Password);

            if (foundedUser == null)
            {
                return new LoginAnswer
                {
                    Success = false,
                    Errors = new []{ "User with this login and email not found." }
                };
            }
            
            return new LoginAnswer
            {
                Success = true,
                UserId = foundedUser.Id,
            };
        }
    }
}