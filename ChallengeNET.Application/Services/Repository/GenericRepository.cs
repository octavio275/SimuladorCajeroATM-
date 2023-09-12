using ChallengeNET.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace EjercicioPOO.Application.Services.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly OperacionContext _context;
        private DbSet<T> table;
        public GenericRepository(OperacionContext context)
        {
            this._context = context;
            table = context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return table;
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
