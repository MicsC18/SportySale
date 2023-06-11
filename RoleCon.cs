using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace DAL.Datos
{
    public class RoleCon
    {
        private static RoleCon instance = null;
        public static RoleCon GetUsuarioCon
        {
            get
            {
                //Si no existe instancia se genera una nueva
                if (instance == null)
                {
                    instance = new RoleCon();
                }
                return instance;
            }
        }
        private RoleCon() { }

        public DataTable ObtenerRoles()
        {
            Conexion objConexion = Conexion.GetConexion();
            DataTable dt;
            dt = objConexion.LeerPorStoreProcedure("sp_obtener_roles");
            return dt;
        }
    }
}
