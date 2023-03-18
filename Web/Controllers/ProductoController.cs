using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;
using static System.Collections.Specialized.BitVector32;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        // GET: oProducto
        public ActionResult Index()
        {
            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();
                return View(_ServiceProducto.ListadoProducto());
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: oProducto/Details/5
        public ActionResult Details(int? id)
        {
            ServiceProducto _ServiceProducto = new ServiceProducto();
            producto oProducto = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                oProducto = _ServiceProducto.ObtenerProductoID(Convert.ToInt32(id));
                if (oProducto == null)
                {
                    TempData["Message"] = "No existe el Producto solicitado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(oProducto);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: oProducto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oProducto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //Craer Plan de Cobro
        [HttpGet]

        public ActionResult Createe()
        {

            ViewBag.idproveedor = listProveedores();
            ViewBag.idCategoria = listaTipoCategoria();

            return View();
        }


        [HttpPost]
        public ActionResult Save(producto gestion, string[] selectProvedores)
        {

            IServiceProducto servicePlanCobro = new ServiceProducto();

            try
            {

                if (ModelState.IsValid)
                {
                    producto oGestion = servicePlanCobro.Save(gestion, selectProvedores);

                }
                else
                {

                     ViewBag.idproveedor = listProveedores(gestion.proveedor);
                    ViewBag.idCategoria = listaTipoCategoria(gestion.idCategoria);

                    if (gestion.id > 0)
                    {
                        return (ActionResult)View("Edit", gestion);
                    }
                    else
                    {
                        return View("Index", gestion);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        // GET: oProducto/Edit/5
        public ActionResult Edit(int? id)
        {
            ServiceProducto serviceProducto = new ServiceProducto();
            producto producto = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                producto = serviceProducto.ObtenerProductoID(Convert.ToInt32(id));

                if (producto == null)
                {
                    TempData["Message"] = "No existe el producto solicitado";
                    TempData["Redirect"] = "Index";
                    TempData["Redirect-Action"] = "Index";
                    return RedirectToAction("Default", "Error");
                }
                   ViewBag.idProducto = listProveedores(producto.proveedor);
                ViewBag.idCategoria = listaTipoCategoria(producto.idCategoria);
                return View(producto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }


        private MultiSelectList listProveedores(ICollection<proveedor> provedores = null)
        {
            IServiceProveedores serviceProveedores = new ServiceProveedores();
            IEnumerable<proveedor> lista = serviceProveedores.GetProveedors();
            int[] listaProveedores = null;
            if (provedores != null)
            {
                listaProveedores = provedores.Select(r => r.id).ToArray();
            }

            return new MultiSelectList(lista, "id", "nombreEmpresa", listaProveedores);
        }


        private SelectList listaTipoCategoria(int idTipoCategoria = 0)
        {
            IServiceCategoria service = new ServiceCategoria();
            IEnumerable<TipoCategoria> lista = service.ListadoCategoria(); 
            return new SelectList(lista, "id", "Descripcion", idTipoCategoria);

        }
        // POST: oProducto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: oProducto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: oProducto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
