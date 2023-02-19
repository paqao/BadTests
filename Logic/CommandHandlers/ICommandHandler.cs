using Logic.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.CommandHandlers
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : class, ICommand
    {
        Task<TResult> ExecuteAsync(TCommand command);
    }
}
