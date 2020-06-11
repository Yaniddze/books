using System.Threading.Tasks;

namespace BooksApi.CQRS.Commands.Abstractions
{
    public interface ICommandHandler<TCommand> 
        where TCommand: ICommand
    {
        Task HandleAsync(TCommand handled);
    }
}