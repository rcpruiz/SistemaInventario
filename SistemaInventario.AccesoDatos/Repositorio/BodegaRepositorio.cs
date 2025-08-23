using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class BodegaRepositorio : Repositorio<Bodega>, IBodegaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public BodegaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Bodega bodega)
        {
            var bodegaDesdeDb = _db.Bodegas.FirstOrDefault(b => b.Id == bodega.Id);
            if (bodegaDesdeDb != null)
            {
                bodegaDesdeDb.Nombre = bodega.Nombre;
                bodegaDesdeDb.Descripcion = bodega.Descripcion;
                bodegaDesdeDb.Estado = bodega.Estado;
                // No es necesario llamar a _db.Bodegas.Update(bodegaDesdeDb) ya que el contexto ya está rastreando la entidad
                _db.SaveChanges(); // Guardar los cambios en la base de datos
            }
        }
    }

}
