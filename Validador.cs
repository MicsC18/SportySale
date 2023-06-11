using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClasesProyectoVentas
{
    public  class Validador
    {
        // ESTE VALIDADOR ES UTIL PARA EL SECTOR DONDE SE LOGUEA EL USUARIO.
        // EN CASO DE COMETER UN ERROR DE INGRESAR UN VALOR NUMERICO
        // EL VALIDADOR RETORNA UN VALOR FALSO Y AUTOMATICAMENTE SE PRODUCE
        // UNA EXCEPCION. -> linea 37 de login
        public static bool ValidarSoloCaracteresAlfabeticos(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
