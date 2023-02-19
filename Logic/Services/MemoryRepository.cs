using Logic.Exceptions;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class MemoryRepository<TEntity> : IRepository<TEntity> where TEntity : class, IModel
    {
        protected int NextId = 0;
        protected readonly List<TEntity> Entities;

        public MemoryRepository()
        {
            Entities = new List<TEntity>();
        }

        public MemoryRepository(List<TEntity> entities)
        {
            Entities = entities;
        }

        public MemoryRepository(params TEntity[] entities)
        {
            Entities = entities.ToList();
        }

        public async Task<TEntity> Create(TEntity data)
        {
            data.Id = NextId;
            if (Entities.Any(x => x.Id == data.Id))
            {
                throw new DuplicationException(typeof(TEntity).Name, data.Id);
            }

            Entities.Add(data);
            NextId++;

            return data;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            return Task.FromResult<IEnumerable<TEntity>>(Entities);
        }

        public Task<TEntity?> GetById(int id)
        {
            return Task.FromResult<TEntity?>(Entities.FirstOrDefault(x => x.Id == id));
        }

        public async Task<TEntity> Update(TEntity data)
        {
            var previousItem = Entities.SingleOrDefault(x => x.Id == data.Id);
            if (previousItem == null)
            {
                throw new NotFoundException(typeof(TEntity).Name, data.Id);
            }

            Entities.Remove(previousItem);
            Entities.Add(data);

            return data;
        }
    }
}
