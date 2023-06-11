using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BibliotecaClasesProyectoVentas;
using System.Data;

namespace DAL.Datos
{
    public class EmpleadoCon
    {
        private static EmpleadoCon instance = null;
        public static EmpleadoCon GetEmpleadoCon
        {
            get
            {
                //Si no existe instancia se genera una nueva
                if (instance == null)
                {
                    instance = new EmpleadoCon();
                }
                return instance;
            }
        }

        private EmpleadoCon() { }

        public bool RegistrarEmpleado(long idDireccion,string nombre,string apellido,long dni,long telefono,string email)
        {
            Conexion objConexion =new Conexion();
            SqlParameter[] parametros = new SqlParameter[6];
            int filasAfectadas = 0;

            parametros[0] = objConexion.crearParametro("@Id_Direccion", idDireccion);
            parametros[1] = objConexion.crearParametro("@Nombre", nombre);
            parametros[2] = objConexion.crearParametro("@Apellido", apellido);
            parametros[3] = objConexion.crearParametro("@DNI", dni);
            parametros[4] = objConexion.crearParametro("@Telefono", telefono);
            parametros[5] = objConexion.crearParametro("@Email", email);

            filasAfectadas = objConexion.EscribirPorStoreProcedure("sp_registrar_empleado", parametros);
            if (filasAfectadas > 0)
            {
                return true;
            }
            return false;
        }

        public Empleado BuscarEmpleadoPorLegajo(int legajo)
        {
            Conexion objConexion = new Conexion();
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = objConexion.crearParametro("@Legajo", legajo);
            DataTable dt = objConexion.LeerPorStoreProcedure("sp_buscar_empleado_por_legajo", parametros);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                int idDireccion = Convert.ToInt32(row["Id_Direccion"]);
                string nombre = row["Nombre"].ToString();
                string apellido = row["Apellido"].ToString();
                long dni = Convert.ToInt64(row["DNI"]);
                long telefono = Convert.ToInt64(row["Telefono"]);
                string email = row["Email"].ToString();

                // Crear y retornar el objeto Empleado
                return new Empleado(idDireccion, nombre, apellido, dni, telefono, email);
            }

            return null; // Si no se encuentra el empleado, se devuelve null o puedes modificar para lanzar una excepción.
        }
    }
}
