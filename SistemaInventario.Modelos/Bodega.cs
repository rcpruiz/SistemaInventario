using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Bodega
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Nombre es requerido")]
        [MaxLength(60, ErrorMessage = "El Nombre debe ser maximo 60 caracteres")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "Descripcion es requerido")]
        [MaxLength(100, ErrorMessage = "Descripcion debe ser maximo 100 caracteres")]
        public required string Descripcion { get; set; }
        [Required(ErrorMessage = "Êstado es requerido")]
        public bool Estado { get; set; }
    }
}
