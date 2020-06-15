using System.Threading.Tasks;
using AutoMapper;
using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;

namespace BooksApi.DataBase.CQRS.UserImpl
{
    public class AddUserCommandHandler: ICommandHandler<AddUserCommand>
    {
        private readonly ContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(ContextProvider contextProvider, IMapper mapper)
        {
            _contextProvider = contextProvider;
            _mapper = mapper;
        }

        public async Task HandleAsync(AddUserCommand handled)
        {
            using (var context = _contextProvider.GetContext())
            {
                var mappedUser = _mapper.Map<User, UserDB>(handled.UserToAdd);
                context.Users.Add(mappedUser);
                await context.SaveChangesAsync();
            }
        }
    }
}