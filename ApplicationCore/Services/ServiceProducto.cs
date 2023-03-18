using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        public IEnumerable<producto> ListadoProducto()
        {
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            return repositoryProducto.ListadoProducto();
        }

        public producto ObtenerProductoID(int id)
        {
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            return repositoryProducto.ObtenerProductoID(id);           
        }

        public producto Save(producto plan, string[] selectProvedores)
        {
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            return repositoryProducto.Save(plan, selectProvedores);
        }
    }
}
