using System;
using System.Web.Mvc;
using Prueba.Business;
using Prueba.Entities;

namespace Prueba.Api.Controllers
{
    public class ProductoController : Controller
    {
        private ProductoService _service = new ProductoService();

        // GET: Producto/Index (Carga la página y la lista. Opcionalmente carga un producto para editar)
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                ViewBag.ProductoEdicion = _service.Obtener(id.Value);
            }

            var lista = _service.ListarTodo();
            return View(lista);
        }

        [HttpPost]
        public ActionResult Guardar(Producto p)
        {
            if (_service.Registrar(p))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Error = "No se pudo guardar. Verifique que el precio sea mayor a 0.";
            return View("Index", _service.ListarTodo());
        }

        public ActionResult Editar(int id)
        {
            var p = _service.Obtener(id);
            if (p == null) return RedirectToAction("Index");
            return View(p);
        }

        [HttpPost]
        public ActionResult Actualizar(Producto p)
        {
            if (_service.Actualizar(p))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Error = "No se pudo actualizar los datos.";
            return View("Index", _service.ListarTodo());
        }

        public ActionResult Eliminar(int id)
        {
            _service.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}
