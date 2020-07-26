using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.CQRS.Commands;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;
using MediatR;

namespace BooksApi.DataBase.CQRS.UserImpl
{
    public class AddUserCommandHandler: IRequestHandler<AddUserCommand>
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IMapper mapper, IContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var mappedUser = _mapper.Map<User, UserDB>(request.UserToAdd);
            _context.Users.Add(mappedUser);
            await _context.SaveChangesAsync();
            
            return Unit.Value;
        }
    }
}