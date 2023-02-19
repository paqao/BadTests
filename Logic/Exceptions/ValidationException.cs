using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string itemType, int id)
        {
            ItemType = itemType;
            Id = id;
        }

        public ValidationException(string itemType, int id, string message) : base(message)
        {
            ItemType = itemType;
            Id = id;
        }

        public string ItemType { get; set; }
        public int Id { get; set; }

        public string ExceptionMessage()
        {
            return $"Validation error for {ItemType}: {Id}. Detailed: {Message}";
        }
    }
}
