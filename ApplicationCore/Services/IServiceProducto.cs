using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceProducto
    {
        IEnumerable<producto> ListadoProducto();

        producto ObtenerProductoID(int id);
        producto Save(producto plan, string[] selectProvedores);
    }
}
