using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ATMDBContext _dbContext;

    public Repository(ATMDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public TEntity GetById(int id)
    {
        return _dbContext.Set<TEntity>().Find(id);
    }

    public List<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>().ToList();
    }

    public void Insert(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    //public void Update(TEntity entity)
    //{
    //    _dbContext.Entry(entity).State = EntityState.Modified;
    //}

    public int Update(int Id, TEntity entity)
    {
        // Check if an entity with the provided ID exists in the database
        var existingEntity = _dbContext.Set<TEntity>().Find(Id);

        if (existingEntity == null)
        {
            // Handle the case where the entity does not exist
            throw new Exception($"Entity with ID {Id} not found.");
        }

        // If the entity exists, update its properties with the new values
        _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        return 1;
    }


    public int Delete(int id)
    {
        var entity = _dbContext.Set<TEntity>().Find(id);
        if (entity != null)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges(); // Returns the number of records affected (1 if successful)
        }

        return 0; // Return 0 if the entity was not found and nothing was deleted
    }


    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}