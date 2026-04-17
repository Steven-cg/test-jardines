using System;
using System.Web.Mvc;
using Prueba.Business;
using Prueba.Entities;

namespace Prueba.Api.Controllers
{
    public class ProductoController : Controller
    {
        private ProductoService _service = new ProductoService();

        // GET: Producto/Index (Carga la página y la lista)
        public ActionResult Index()
        {
            var lista = _service.ListarTodo();
            return View(lista);
        }

        // POST: Producto/Guardar (Recibe los datos del formulario)
        [HttpPost]
        public ActionResult Guardar(Producto p)
        {
            if (_service.Registrar(p))
            {
                return RedirectToAction("Index");
            }

            // Si falla la validación o el guardado
            ViewBag.Error = "No se pudo guardar. Verifique que el precio sea mayor a 0 y el stock no sea negativo.";
            return View("Index", _service.ListarTodo());
        }
    }
}
