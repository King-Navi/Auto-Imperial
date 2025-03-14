using AutoImperialDAO.DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Obtiene todos los registros de la entidad
        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        // Obtiene un registro por su ID
        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        // Agrega una nueva entidad
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        // Actualiza una entidad existente
        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        // Elimina una entidad
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        // Guarda los cambios en la base de datos
        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}
