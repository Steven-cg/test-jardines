using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Prueba.Entities;

namespace Prueba.Data {
    public class ProductoData {
        // Lee la conexión dinámicamente del Web.config
        private readonly string _conexion = ConfigurationManager.ConnectionStrings["CadenaPrueba"].ConnectionString;

        public List<Producto> Consultar() {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection cn = new SqlConnection(_conexion)) {
                // Uso de Procedimientos Almacenados (Requisito 3)
                SqlCommand cmd = new SqlCommand("sp_ConsultarProductos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader()) {
                    while (dr.Read()) {
                        lista.Add(new Producto {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"].ToString(),
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            Stock = Convert.ToInt32(dr["Stock"])
                        });
                    }
                }
            }
            return lista;
        }

        public bool Insertar(Producto p) {
            using (SqlConnection cn = new SqlConnection(_conexion)) {
                SqlCommand cmd = new SqlCommand("sp_InsertarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@Precio", p.Precio);
                cmd.Parameters.AddWithValue("@Stock", p.Stock);
                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
