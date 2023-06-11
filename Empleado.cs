using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClasesProyectoVentas
{
    public class Empleado
    {
        public int IdDireccion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public long DNI { get; set; }
        public long Telefono { get; set; }
        public string Email { get; set; }

        public Empleado(int idDireccion, string nombre, string apellido, long dni, long telefono, string email)
        {
            IdDireccion = idDireccion;
            Nombre = nombre;
            Apellido = apellido;
            DNI = dni;
            Telefono = telefono;
            Email = email;
        }
    }
}
