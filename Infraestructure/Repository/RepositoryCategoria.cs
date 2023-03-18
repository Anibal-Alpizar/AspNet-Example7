using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryCategoria : IRepositoryCategoria
    {
        public IEnumerable<TipoCategoria> ListadoCategoria()
        {
           
            try
            {
                IEnumerable<TipoCategoria> lista = null;

                using (MyContext ctx = new MyContext())
                {
                ctx.Configuration.LazyLoadingEnabled = false;


                    lista = ctx.TipoCategoria.ToList<TipoCategoria>();
                   

                }
                return lista;
            }

            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public TipoCategoria ObtenerTipoCategoriaID(int id)
        {
            TipoCategoria categoria = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                categoria = ctx.TipoCategoria.Find(id);
            }
            return categoria;
        }
    }
}
