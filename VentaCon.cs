using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaClasesProyectoVentas;
namespace DAL
{
    public class VentaCon
    {
        public bool RegistrarVenta(int idCliente, int idUsuario, DateTime fecha, decimal total)
        {
            Conexion objConexion = new Conexion();
            SqlParameter[] parametros = new SqlParameter[4];
            int filasAfectadas = 0;

            parametros[0] = objConexion.crearParametro("@Id_Cliente", idCliente);
            parametros[1] = objConexion.crearParametro("@Id_Usuario", idUsuario);
            parametros[2] = objConexion.crearParametro("@Fecha", fecha);
            parametros[3] = objConexion.crearParametro("@Total", total.ToString());

            filasAfectadas = objConexion.EscribirPorStoreProcedure("sp_registrar_venta", parametros);
            if (filasAfectadas > 0)
            {
                return true;
            }
            return false;
        }
        public List<Venta> ObtenerTodasLasVentas()
        {
            Conexion objConexion = new Conexion();
            DataTable dataTable = new DataTable();
            List<Venta> ventas = new List<Venta>();

            dataTable = objConexion.LeerPorStoreProcedure("sp_obtener_todas_ventas", null);

            foreach (DataRow row in dataTable.Rows)
            {
                int idCliente = Convert.ToInt32(row["ID Venta"]);
                DateTime fecha = Convert.ToDateTime(row["Fecha Venta"]);
                decimal total = Convert.ToDecimal(row["Total"]);

                Venta venta = new Venta(idCliente, fecha, total);

                string nombreCliente = row["Cliente"].ToString();
                string emailCliente = row["Email Cliente"].ToString();
                string nombreUsuario = row["Username Usuario"].ToString();

                venta.clienteV = new Cliente(nombreCliente, emailCliente);
                venta.usuarioV = new Usuario(nombreUsuario);

                ventas.Add(venta);
            }

            return ventas;
        }
    }
    
}
