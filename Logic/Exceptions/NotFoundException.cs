using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string itemType, int id)
        {
            ItemType = itemType;
            Id = id;
        }

        public string ItemType { get; set; }
        public int Id { get; set; }

        public string ExceptionMessage()
        {
            return $"Not found {ItemType} with id {Id}.";
        }
    }
}
