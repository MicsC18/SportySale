using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Datos
{
    public class ProductoCon
    {
        public bool BorrarProducto(int idProducto)
        {
            Conexion objConexion = new Conexion();
            SqlParameter[] parametros = new SqlParameter[1];
            int filasAfectadas = 0;

            parametros[0] = objConexion.crearParametro("@id", idProducto);

            filasAfectadas = objConexion.EscribirPorStoreProcedure("sp_borrar_productos", parametros);
            if (filasAfectadas > 0)
            {
                return true;
            }
            return false;
        }

        public bool EditarProducto(int idProducto, string descripcion, int cantidad, double precio)
        {
            Conexion objConexion = new Conexion();
            SqlParameter[] parametros = new SqlParameter[4];
            int filasAfectadas = 0;

            parametros[0] = objConexion.crearParametro("@id", idProducto);
            parametros[1] = objConexion.crearParametro("@Descripcion", descripcion);
            parametros[2] = objConexion.crearParametro("@Cantidad", cantidad);
            parametros[3] = objConexion.crearParametro("@Precio", precio);

            filasAfectadas = objConexion.EscribirPorStoreProcedure("sp_editar_productos", parametros);
            if (filasAfectadas > 0)
            {
                return true;
            }
            return false;
        }

        public DataTable ObtenerProducto(int idProducto)
        {
            Conexion objConexion = new Conexion();
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = objConexion.crearParametro("@id", idProducto);

            DataTable dt = objConexion.LeerPorStoreProcedure("sp_obtener_productos", parametros);

            return dt;
        }

        public DataTable DevolverTodosLosProductos()
        {
            Conexion objConexion = new Conexion();

            DataTable dt = objConexion.LeerPorStoreProcedure("sp_obtener_todos_los_productos");

            return dt;
        }

        public decimal ObtenerPrecioPorDescripcion(string descripcion)
        {
            Conexion objConexion = new Conexion();
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = objConexion.crearParametro("@Descripcion", descripcion);

            DataTable dt = objConexion.LeerPorStoreProcedure("sp_dar_precio_por", parametros);

            if (dt.Rows.Count > 0)
            {
                decimal precio = Convert.ToDecimal(dt.Rows[0]["Precio"]);
                return precio;
            }

            return 0; // Si no se encuentra la descripción o el precio es 0
        }
        public int ObtenerCantidadPorDescripcion(string descripcion)
        {
            Conexion objConexion = new Conexion();
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = objConexion.crearParametro("@Descripcion", descripcion);

            DataTable dt = objConexion.LeerPorStoreProcedure("sp_obtener_cantidad_por_descripcion", parametros);

            if (dt.Rows.Count > 0)
            {
                int cantidad = Convert.ToInt32(dt.Rows[0]["Cantidad"]);
                return cantidad;
            }

            return 0; // Si no se encuentra la descripción o la cantidad es 0
        }


    }
}

