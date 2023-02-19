using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IRepository<TEntity> where TEntity : class, IModel
    {
        Task<TEntity?> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Create(TEntity data);
        Task<TEntity> Update(TEntity data);
        Task Delete(int id);
    }
}
