using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BibliotecaClasesProyectoVentas;
using DAL;
using DAL.Datos;

namespace NegocioIndumentariaEscritorio
{
    public partial class Login : Form
    {
        private UsuarioCon usuarioCon;

        public Login()
        {
            InitializeComponent();
            usuarioCon = UsuarioCon.GetUsuarioCon;

        }

        private void buttonIniciarSesion_Click(object sender, EventArgs e)
        {
            string usuario = "";
            try
            {
                usuario = textBox1.Text;

                // Validar si el usuario contiene solo caracteres alfabéticos
                if (!Validador.ValidarSoloCaracteresAlfabeticos(usuario))
                {
                    throw new Exception("El nombre de usuario contiene caracteres no alfabéticos");
                }



                // Resto del código...
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {

                string password = textBoxPassw.Text;

                // Verificar si el usuario existe en la base de datos
                DataTable dtUsuario = usuarioCon.IniciarSesion(usuario, password);
                long id_rol = usuarioCon.ObtenerRole(usuario);

                if (dtUsuario.Rows.Count > 0)
                {
                    // El usuario existe, mostrar mensaje de éxito
                    MessageBox.Show("Inicio de sesión exitoso", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    if (id_rol == 1)
                    {
                        MenuPrincipalAdministrador menu = new MenuPrincipalAdministrador();
                        menu.Show();
                    }
                    if (id_rol == 2)
                    {
                        MenuPrincipalGerente menug = new MenuPrincipalGerente();
                        menug.Show();
                    }
                    if (id_rol == 3)
                    {
                        MenuPrincipalVendedor menuv = new MenuPrincipalVendedor(dtUsuario);
                        menuv.Show();
                    }
                }
                else
                {
                    textBox1.Text = "";
                    textBoxPassw.Text = "";
                    // El usuario no existe, mostrar mensaje de error
                    MessageBox.Show("Nombre de usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
