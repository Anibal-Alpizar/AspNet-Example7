using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Xml.Linq;

namespace Infraestructure.Models
{

    internal partial class ProductoMetadata
    {
        public int id { get; set; }
        
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "El campo Nombre debe tener al menos 4 caracteres.")]
        public string nombre { get; set; }
        
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría")]
        public int idCategoria { get; set; }
        
        [Required(ErrorMessage = "Total existencias es requerido")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Total existencias debe ser un número entero")]
        [DataType(DataType.Text, ErrorMessage = "Total existencias debe ser un número entero")]
        public int totalStock { get; set; }
        
        [Required(ErrorMessage = "Cantidad máxima es requerida")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Cantidad máxima debe ser un número entero")]
        public int cantMaxima { get; set; }
        
        [Required(ErrorMessage = "Cantidad mínima es requerida")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Cantidad mínima debe ser un número entero")]
        public int cantMinima { get; set; }
        
        [Required(ErrorMessage = "Costo es requerido")]
        [RegularExpression(@"^\d{1,2}(\.\d{2})*(\,\d{1,2})?$", ErrorMessage = "Costo debe ser un número con dos decimales")]
        public double costoUnitario { get; set; }     
    }
}
