using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class BusinessProcess : IModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Const.BusinessProcessConstants.Status Status { get; set; }

        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }

        public string Author { get; set; }
        public string Division { get; set; }
        public string Comment { get; set; }
    }
}
