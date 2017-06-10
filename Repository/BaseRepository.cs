using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        DbContext db;
        IDbSet<T> dbSet;

        public BaseRepository(DbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }

        public void Create(T obj)
        {
            dbSet.Add(obj);
            db.SaveChanges();
        }

        public T SelectByID(int id)
        {
            return dbSet.Find(id);
        }

        public void Delete(int id)
        {
            T existing = dbSet.Find(id);
            dbSet.Remove(existing);
            db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }

        public void Update(T obj)
        {
            db.Set<T>().AddOrUpdate(obj);
            db.SaveChanges();
        }
    }
}
