using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryProducto : IRepositoryProducto
    {

        public IEnumerable<producto> ListadoProducto()
        {
            List<producto> lista = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                try
                {
                    lista = ctx.producto.Include(x=>x.TipoCategoria).Where(x => x.totalStock > 0).ToList();
                    return lista;

                }
                catch (Exception ex)
                {
                    string mensaje = "";
                    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }
        public producto ObtenerProductoID(int id)
        {
            producto producto = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                //producto = ctx.producto.Include(x => x.TipoCategoria).Include(x => x.proveedor)
                //    .Where(x => x.id == id).FirstOrDefault();
                producto = ctx.producto.Where(l => l.id== id)
                    .Include("TipoCategoria")
                    .Include("proveedor")
                    .FirstOrDefault();
            }
            return producto;
        }


        public producto Save(producto plan, string[] selectRubrosCobros)
        {


            try
            {
                int retorno = 0;
                producto gestion = null;


                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    gestion = ObtenerProductoID((int)plan.id);
                    IRepositoryProveedores repositoryRubrosCobros = new RepositoryProveedores();

                    if (gestion == null)
                    {

                        if (selectRubrosCobros != null)
                        {
                            //Insertar o agregar varios rubros de cobro
                            plan.proveedor = new List<proveedor>();
                            foreach (var rubro in selectRubrosCobros)
                            {
                                var rubroAdd = repositoryRubrosCobros.GetProveedoresById(int.Parse(rubro));

                                ctx.proveedor.Attach(rubroAdd);
                                plan.proveedor.Add(rubroAdd);

                            }
                        }

                        //Insertar Plan Cobro
                        ctx.producto.Add(plan);
                        retorno = ctx.SaveChanges();

                    }
                    else
                    {
                        //Actualizar Plan Cobro / Modificar

                        ctx.producto.Add(plan);
                        ctx.Entry(plan).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();

                        //Actualizar Rubros cobros
                        var selectRubrosId = new HashSet<string>(selectRubrosCobros);
                        if (selectRubrosCobros != null)
                        {
                            ctx.Entry(plan).Collection(r => r.proveedor).Load();
                            var newSelectRubro = ctx.proveedor.Where(x => selectRubrosId.Contains(x.id.ToString())).ToList();
                            plan.proveedor = newSelectRubro;

                            //insertamos los rubros modificados
                            ctx.Entry(plan).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();
                        }
                    }

                }
                if (retorno > 0)
                    plan = ObtenerProductoID((int)plan.id);
                return gestion;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
