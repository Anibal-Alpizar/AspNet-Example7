using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProveedores : IServiceProveedores
    {
        public proveedor GetProveedoresById(int id)
        {
            IRepositoryProveedores repositoryProveedores = new RepositoryProveedores();
            return repositoryProveedores.GetProveedoresById(id);
        }

        public IEnumerable<proveedor> GetProveedors()
        {
            IRepositoryProveedores repository = new RepositoryProveedores();
            return repository.GetProveedores();
        }
    }
}
