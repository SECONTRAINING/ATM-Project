using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(int id);
        List<TEntity> GetAll();
        void Insert(TEntity entity);
        int Update(int Id, TEntity entity);
        int Delete(int Id);
        void SaveChanges();

    }
}