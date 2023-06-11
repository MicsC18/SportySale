using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ClienteCon
    {
        public bool RegistrarCliente(int idDireccion, int dni, string nombre, string apellido, string email, int telefono)
        {
            Conexion objConexion = new Conexion();
            SqlParameter[] parametros = new SqlParameter[6];
            int filasAfectadas = 0;

            parametros[0] = objConexion.crearParametro("@Id_Direccion", idDireccion);
            parametros[1] = objConexion.crearParametro("@DNI", dni);
            parametros[2] = objConexion.crearParametro("@Nombre", nombre);
            parametros[3] = objConexion.crearParametro("@Apellido", apellido);
            parametros[4] = objConexion.crearParametro("@Email", email);
            parametros[5] = objConexion.crearParametro("@Telefono", telefono);

            filasAfectadas = objConexion.EscribirPorStoreProcedure("sp_registrar_cliente", parametros);
            if (filasAfectadas > 0)
            {
                return true;
            }
            return false;
        }

        public int ObtenerIdCliente(string nombre, int dni)
        {
            Conexion objConexion = new Conexion();
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = objConexion.crearParametro("@Nombre", nombre);
            parametros[1] = objConexion.crearParametro("@DNI", dni);

            DataTable dt = objConexion.LeerPorStoreProcedure("sp_obtener_id_cliente", parametros);

            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["Id_Cliente"]);
            }

            return 0; // Si no se encuentra el cliente o el Id_Cliente es nulo, se devuelve 0 o puedes modificar para lanzar una excepción.
        }


    }
}
