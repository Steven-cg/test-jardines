using System;
using System.Globalization;
using System.Web.Mvc;
using Prueba.Business;
using Prueba.Entities;

namespace Prueba.Api.Controllers
{
    public class ProductoController : Controller
    {
        private ProductoService _service = new ProductoService();

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
        public ActionResult Guardar(FormCollection form)
        {
            var p = new Producto
            {
                Nombre = form["Nombre"],
                Precio = ParseDecimal(form["Precio"]),
                Stock  = ParseInt(form["Stock"])
            };

            if (_service.Registrar(p))
                return RedirectToAction("Index");

            ViewBag.Error = "No se pudo guardar. Verifique que el precio sea mayor a 0.";
            return View("Index", _service.ListarTodo());
        }

        [HttpPost]
        public ActionResult Actualizar(FormCollection form)
        {
            var p = new Producto
            {
                Id     = ParseInt(form["Id"]),
                Nombre = form["Nombre"],
                Precio = ParseDecimal(form["Precio"]),
                Stock  = ParseInt(form["Stock"])
            };

            if (_service.Actualizar(p))
                return RedirectToAction("Index");

            ViewBag.Error = "No se pudo actualizar los datos.";
            ViewBag.ProductoEdicion = p;
            return View("Index", _service.ListarTodo());
        }

        public ActionResult Eliminar(int id)
        {
            _service.Eliminar(id);
            return RedirectToAction("Index");
        }

        // Helpers para parsear correctamente decimales con punto o coma
        private decimal ParseDecimal(string valor)
        {
            if (string.IsNullOrEmpty(valor)) return 0;
            valor = valor.Trim().Replace(",", ".");
            decimal result;
            decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
            return result;
        }

        private int ParseInt(string valor)
        {
            if (string.IsNullOrEmpty(valor)) return 0;
            int result;
            int.TryParse(valor.Trim(), out result);
            return result;
        }
    }
}
