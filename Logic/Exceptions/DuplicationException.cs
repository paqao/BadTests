using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class DuplicationException : Exception
    {
        public DuplicationException(string itemType, int id)
        {
            ItemType = itemType;
            Id = id;
        }

        public string ItemType { get; set; }
        public int Id { get; set; }

        public string ExceptionMessage()
        {
            return $"Duplication error for {ItemType}: {Id}. Detailed: {Message}";
        }
    }
}
