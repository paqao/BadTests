using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class BusinessProcessRepository : MemoryRepository<BusinessProcess>
    {
        public BusinessProcessRepository()
        {
            Entities.Add(new BusinessProcess
            {
                Id = 1,
                Name = "Test Entity",
                Status = Const.BusinessProcessConstants.Status.New,
                UpdateDate = new DateTime(2023, 2, 13)
            });

            NextId = 2;
        }
    }
}
