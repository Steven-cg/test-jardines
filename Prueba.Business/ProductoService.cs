using System;
using System.Collections.Generic;
using System.IO;
using Prueba.Data;
using Prueba.Entities;

namespace Prueba.Business {
    public class ProductoService {
        private readonly ProductoData _data = new ProductoData();

        public List<Producto> ListarTodo() {
            try {
                return _data.Consultar();
            } catch (Exception ex) {
                EscribirLog(ex);
                throw;
            }
        }

        public bool Registrar(Producto p) {
            try {
                // Validación de datos (Requisito 4): No vacíos, precio > 0
                if (string.IsNullOrEmpty(p.Nombre) || p.Precio <= 0 || p.Stock < 0)
                    return false;

                return _data.Insertar(p);
            } catch (Exception ex) {
                EscribirLog(ex); // Registro de errores (Requisito 5)
                return false;
            }
        }

        private void EscribirLog(Exception ex) {
            string ruta = AppDomain.CurrentDomain.BaseDirectory + "errores.log";
            using (StreamWriter sw = new StreamWriter(ruta, true)) {
                sw.WriteLine($"[{DateTime.Now}] - ERROR: {ex.Message}");
                sw.WriteLine($"Stack: {ex.StackTrace}");
                sw.WriteLine("---------------------------------------------------");
            }
        }
    }
}
