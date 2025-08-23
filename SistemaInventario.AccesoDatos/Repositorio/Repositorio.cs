using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad);  //Insert into table
        }

        public async Task<T> Obtener(int id)
        {
            return await dbSet.FindAsync(id);   //  Select * from (solo por Id)
        }
        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); // Where condition
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); // Include related entities
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query); // Order by condition
            }
            if (!isTracking)
            {
                query = query.AsNoTracking(); // Disable tracking for read-only operations
            }
            return await query.ToListAsync(); // Execute the query and return the results
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); // Where condition
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp); // Include related entities
                }
            }
            if (!isTracking)
            {
                query = query.AsNoTracking(); // Disable tracking for read-only operations
            }
            return await query.FirstOrDefaultAsync(); // Execute the query and return the first matching entity or null if none found
        }



        public void Remove(T entidad)
        {
            dbSet.Remove(entidad); // Delete the entity
        }

        public void RemoveRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad); // Delete a range of entities based on the provided id
        }
    }
}
