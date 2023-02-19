using Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Queries
{
    public class GetItemById : IQuery<BusinessProcessDto>
    {
        public int Id { get; set; }
    }
}
