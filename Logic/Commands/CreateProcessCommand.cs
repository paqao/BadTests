﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Commands
{
    public class CreateProcessCommand : ICommand
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Division { get; set; }
        public string Comment { get; set; }
    }
}
