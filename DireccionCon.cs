using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DireccionCon
    {
        public bool registrarDireccion(string CP, string calle, int numero)
        {
            Conexion objConexion = new Conexion();
            SqlParameter[] parametros = new SqlParameter[3];
            int filasAfectadas = 0;

            parametros[0] = objConexion.crearParametro("@CP", CP);
            parametros[1] = objConexion.crearParametro("@Calle", calle);
            parametros[2] = objConexion.crearParametro("@Numero", numero);

            filasAfectadas = objConexion.EscribirPorStoreProcedure("sp_registrar_direccion", parametros);

            if (filasAfectadas > 0)
            {
                return true;
            }

            return false;
        }

        public int obtenerIdporCalleYNumero(string calle, int numero)
        {
            Conexion objConexion = new Conexion();
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = objConexion.crearParametro("@Calle", calle);
            parametros[1] = objConexion.crearParametro("@Numero", numero);

            DataTable dt = objConexion.LeerPorStoreProcedure("sp_obtener_id_por_calle_y_numero", parametros);

            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["Id_Direccion"]);
            }

            return 0; // Si no se encuentra la dirección o el Id_Direccion es nulo, se devuelve 0 o puedes modificar para lanzar una excepción.
        }
    }
   
}
