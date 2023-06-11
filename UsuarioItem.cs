using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioIndumentariaEscritorio
{
    public class UsuarioItem
    {
        public string Username { get; set; }
        public long IDUsuario { get; set; }

        public string UsernameAndID
        {
            get { return $"{Username} - ID: {IDUsuario}"; }
        }


        public UsuarioItem(string username, long idUsuario)
        {
            Username = username;
            IDUsuario = idUsuario;
        }
    }
}
