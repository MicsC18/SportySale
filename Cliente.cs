using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClasesProyectoVentas
{
    public class Cliente
    {
        public string Nombre { get; set; }
        public string Email { get; set; }

        public Cliente(string nombre, string email)
        {
            Nombre = nombre;
            Email = email;
        }
    }
}
