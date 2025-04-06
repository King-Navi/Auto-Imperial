using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Repositories
{
    //TODO: Esta clase debe borrarase o reestrucutrarse para una mejor abstraccion

    //TODO: Validar escenarios en los cuales los parametros son nulos o flujos alternos
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private int NO_CHANGES = 0;
        protected readonly AutoImperialContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AutoImperialContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Obtiene todos los registros de la entidad
        protected virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        // Obtiene un registro por su ID
        //protected virtual T GetById(int id)
        //{
        //    #pragma warning disable CS8603 // Possible null reference return.
        //    return _dbSet.Find(id);
        //    #pragma warning restore CS8603 // Possible null reference return.
        //}

        // Agrega una nueva entidad
        //public virtual void Add(T entity)
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("null");
        //    }
        //    _dbSet.Add(entity);

        //}

        // Actualiza una entidad existente
        //public virtual void Update(T entity)
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("null");
        //    }
        //    _dbSet.Update(entity);
        //}

        // Elimina una entidad
        //public virtual void Delete(T entity)
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("null");
        //    }
        //    _dbSet.Remove(entity);
        //}

        // Guarda los cambios en la base de datos
        protected virtual bool Save()
        {
            return _context.SaveChanges() > NO_CHANGES;
        }
    }
}
