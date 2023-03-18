using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCategoria : IServiceCategoria
    {
        public IEnumerable<TipoCategoria> ListadoCategoria()
        {
            IRepositoryCategoria repositoryCategoria = new RepositoryCategoria();
            return repositoryCategoria.ListadoCategoria();
        }

        public TipoCategoria ObtenerTipoCategoriaID(int id)
        {
            IServiceCategoria serviceCategoria= new ServiceCategoria();
            return serviceCategoria.ObtenerTipoCategoriaID(id);
        }
    }
}
