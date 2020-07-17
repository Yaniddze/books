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
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task HandleAsync(AddUserCommand handled)
        {
            var mappedUser = _mapper.Map<User, UserDB>(handled.UserToAdd);
            _context.Users.Add(mappedUser);
            await _context.SaveChangesAsync();
        }
    }
}