using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Commands
{
    public class ApproveProcessCommand : ICommand
    {
        public int ProcessId { get; set; }
    }
}
