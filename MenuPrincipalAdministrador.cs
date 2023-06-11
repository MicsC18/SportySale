using BibliotecaClasesProyectoVentas;
using DAL;
using DAL.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace NegocioIndumentariaEscritorio
{
    public partial class MenuPrincipalAdministrador : Form
    {
        public MenuPrincipalAdministrador()
        {
            InitializeComponent();
            groupBoxCrearusuario.Visible = false;
            groupBoxEliminar.Visible = false;
            groupBoxCrearEmpleado.Visible = false;
            groupBoxBuscarEmpleado.Visible = false;
        }




        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxCrearusuario.Visible = false;
            groupBoxEliminar.Visible = false;
            groupBoxCrearEmpleado.Visible = false;
            groupBoxBuscarEmpleado.Visible = false;
            // Obtener el tamaño de la ventana del formulario
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Obtener el tamaño del groupBox
            int groupBoxWidth = groupBoxCrearusuario.Width;
            int groupBoxHeight = groupBoxCrearusuario.Height;

            // Calcular las coordenadas para centrar el groupBox en el medio de la ventana
            int groupBoxX = (formWidth - groupBoxWidth) / 2;
            int groupBoxY = (formHeight - groupBoxHeight) / 2;

            // Establecer la ubicación del groupBox
            groupBoxCrearusuario.Location = new Point(groupBoxX, groupBoxY);
            groupBoxCrearusuario.Visible = true;

            groupBoxCrearusuario.Visible = true;
        }

        private void buttonRegistrar_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los campos de texto
            string username = textBoxNuevoUsuario.Text;
            string password = textBoxPassword.Text;
            string legajoStr = textBoxLegajo.Text;
            string idRolStr = textBoxiDROL.Text;

            // Verificar que ningún campo esté vacío
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(legajoStr) || string.IsNullOrWhiteSpace(idRolStr))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            // Verificar que el username y password sean alfanuméricos
            if (!IsAlphaNumeric(username) || !IsAlphaNumeric(password))
            {
                MessageBox.Show("El username y password deben ser alfanuméricos.");
                return;
            }

            // Verificar que legajo y idRol sean números válidos
            if (!int.TryParse(legajoStr, out int legajo) || !int.TryParse(idRolStr, out int idRol))
            {
                MessageBox.Show("El legajo y el ID de rol deben ser números válidos.");
                return;
            }

            // Todos los campos son válidos, llamar al método RegistrarUsuario

            //EmpleadoCon empleadoCon = EmpleadoCon.GetEmpleadoCon;
            UsuarioCon usuarioCon = UsuarioCon.GetUsuarioCon; // Obtener la instancia de UsuarioCon

            //bool empleadoR = empleadoCon.RegistrarEmpleado(3, username, "PACHECO", 40514278, 54698745, "pacheco@gmail.com");
            bool registrado = usuarioCon.AgregarUsuario(username, password, false,idRol, legajo);

            if (registrado)
            {
                MessageBox.Show("Usuario registrado exitosamente.");
                // Aquí puedes realizar acciones adicionales después de registrar el usuario
            }
            else
            {
                MessageBox.Show("No se pudo registrar el usuario.");
            }

            // Vaciar los campos de texto
            textBoxNuevoUsuario.Text = "";
            textBoxPassword.Text = "";
            textBoxLegajo.Text = "";
            textBoxiDROL.Text = "";
        }


        // Función de validación alfanumérica
        private bool IsAlphaNumeric(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.All(char.IsLetterOrDigit);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxCrearusuario.Visible = false;
            groupBoxEliminar.Visible = false;
            groupBoxCrearEmpleado.Visible = false;
            groupBoxBuscarEmpleado.Visible = false;
            // Obtener el tamaño de la ventana del formulario
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Obtener el tamaño del groupBox
            int groupBoxWidth = groupBoxEliminar.Width;
            int groupBoxHeight = groupBoxEliminar.Height;

            // Calcular las coordenadas para centrar el groupBox en el medio de la ventana
            int groupBoxX = (formWidth - groupBoxWidth) / 2;
            int groupBoxY = (formHeight - groupBoxHeight) / 2;

            // Establecer la ubicación del groupBox
            groupBoxEliminar.Location = new Point(groupBoxX, groupBoxY);

            groupBoxEliminar.Visible = true;
            // Obtener la instancia de UsuarioCon
            UsuarioCon usuarioCon = UsuarioCon.GetUsuarioCon;

            // Obtener todos los usuarios
            DataTable dtUsuarios = usuarioCon.ObtenerUsuarios();

            // Limpiar el comboBoxUsuarios antes de agregar los nuevos elementos
            comboBoxUsuarios.Items.Clear();

            // Agregar cada usuario al comboBoxUsuarios
            foreach (DataRow row in dtUsuarios.Rows)
            {
                // Obtener el valor del campo "Username" y "ID_Usuario" de cada fila
                string username = row["Username"].ToString();
                long idUsuario = Convert.ToInt64(row["ID_Usuario"]);

                // Crear un objeto que represente al usuario
                UsuarioItem usuarioItem = new UsuarioItem(username, idUsuario);

                // Agregar el objeto al comboBoxUsuarios
                comboBoxUsuarios.Items.Add(usuarioItem);
            }

            // Establecer la propiedad DisplayMember del comboBoxUsuarios para mostrar el Username y el ID_Usuario en la lista desplegable
            comboBoxUsuarios.DisplayMember = "UsernameAndID";
            // Establecer la propiedad ValueMember del comboBoxUsuarios para que el ID_Usuario esté disponible al seleccionar un elemento
            comboBoxUsuarios.ValueMember = "IDUsuario";
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un item en el comboBoxUsuarios
            if (comboBoxUsuarios.SelectedItem != null)
            {
                // Obtener el item seleccionado del comboBoxUsuarios
                UsuarioItem usuarioItem = (UsuarioItem)comboBoxUsuarios.SelectedItem;

                // Obtener el ID de usuario del item seleccionado
                long idUsuario = usuarioItem.IDUsuario;

                // Obtener la instancia de UsuarioCon
                UsuarioCon usuarioCon = UsuarioCon.GetUsuarioCon;

                // Llamar al método BajaUsuario para eliminar al usuario
                bool eliminado = usuarioCon.BajaUsuario(idUsuario);

                if (eliminado)
                {
                    // Mostrar mensaje de éxito
                    MessageBox.Show("Usuario eliminado correctamente.");

                    // Actualizar la lista de usuarios en el comboBoxUsuarios
                    eliminarToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    // Mostrar mensaje de error
                    MessageBox.Show("No se pudo eliminar el usuario.");
                }
            }
            else
            {
                // Mostrar mensaje de error si no se ha seleccionado ningún item
                MessageBox.Show("Por favor, seleccione un usuario a eliminar.");
            }
            // Actualizar la lista de usuarios en el comboBoxUsuarios
            eliminarToolStripMenuItem_Click(sender, e);

            // Vaciar la selección del comboBoxUsuarios
            comboBoxUsuarios.SelectedIndex = -1;
        }


        private void buttonRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los campos de texto
            string nombre = textBoxNombreEmpleado.Text;
            string apellido = textBoxApellido.Text;
            string dniStr = textBoxDNI.Text;
            string telefonoStr = textBoxNTEL.Text;
            string CPDireccion = textBoxCP.Text;
            string email = textBoxEmail.Text;
            string calleDireccion = textBoxCalle.Text;
            string NumeroDireccion = textBoxNumero.Text;

            // Verificar que ningún campo esté vacío
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) ||
                string.IsNullOrWhiteSpace(dniStr) || string.IsNullOrWhiteSpace(telefonoStr) ||
                string.IsNullOrWhiteSpace(CPDireccion) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            // Verificar que el nombre y apellido sean alfabéticos
            if (!IsAlphabetic(nombre) || !IsAlphabetic(apellido))
            {
                MessageBox.Show("El nombre y apellido deben contener solo letras.");
                return;
            }

            // Verificar que dni, teléfono y idDirección sean números válidos
            if (!long.TryParse(dniStr, out long dni) || !int.TryParse(telefonoStr, out int telefono) ||
                !int.TryParse(CPDireccion, out int cpDireccion))
            {
                MessageBox.Show("El DNI, teléfono y ID de dirección deben ser números válidos.");
                return;
            }

            // Todos los campos son válidos, llamar al método RegistrarEmpleado

            EmpleadoCon empleadoCon = EmpleadoCon.GetEmpleadoCon; // Obtener la instancia de EmpleadoCon
            DireccionCon direccionCon = new DireccionCon();
            int.TryParse(NumeroDireccion, out int numeroCalle);
            direccionCon.registrarDireccion(CPDireccion, calleDireccion, numeroCalle);
            int idDireccion = direccionCon.obtenerIdporCalleYNumero(calleDireccion, numeroCalle);

            bool registrado = empleadoCon.RegistrarEmpleado(idDireccion, nombre, apellido, dni, telefono, email);

            if (registrado)
            {
                MessageBox.Show("Empleado registrado exitosamente.");
                // Aquí puedes realizar acciones adicionales después de registrar el empleado
            }
            else
            {
                MessageBox.Show("No se pudo registrar el empleado.");
            }

            // Vaciar los campos de texto
            textBoxNombreEmpleado.Text = "";
            textBoxApellido.Text = "";
            textBoxDNI.Text = "";
            textBoxNTEL.Text = "";
            textBoxCP.Text = "";
            textBoxEmail.Text = "";
            textBoxCalle.Text = "";
            textBoxNumero.Text = "";
        }

        // Función de validación alfabética
        private bool IsAlphabetic(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.All(char.IsLetter);
        }

        private void crearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            groupBoxCrearusuario.Visible = false;
            groupBoxEliminar.Visible = false;
            groupBoxCrearEmpleado.Visible = false;
            groupBoxBuscarEmpleado.Visible = false;
            // Obtener el tamaño de la ventana del formulario
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Obtener el tamaño del groupBox
            int groupBoxWidth = groupBoxCrearEmpleado.Width;
            int groupBoxHeight = groupBoxCrearEmpleado.Height;

            // Calcular las coordenadas para centrar el groupBox en el medio de la ventana
            int groupBoxX = (formWidth - groupBoxWidth) / 2;
            int groupBoxY = (formHeight - groupBoxHeight) / 2;

            // Establecer la ubicación del groupBox
            groupBoxCrearEmpleado.Location = new Point(groupBoxX, groupBoxY);
            groupBoxCrearEmpleado.Visible = true;


            groupBoxCrearEmpleado.Visible = true;

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login ingreso = new Login();
            ingreso.Show();
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            groupBoxBuscarEmpleado.Visible = false;
            EmpleadoCon empleadoCon = EmpleadoCon.GetEmpleadoCon;
            string legajo = textBoxBuscarLegajo.Text;
            int legajoInt;
            if (int.TryParse(legajo, out legajoInt))
            {
                Empleado empleadoEncontrado = empleadoCon.BuscarEmpleadoPorLegajo(legajoInt);

                if (empleadoEncontrado != null)
                {
                    // Crear el mensaje con los atributos del empleado
                    string mensaje = $"Id Dirección: {empleadoEncontrado.IdDireccion}\n" +
                                     $"Nombre: {empleadoEncontrado.Nombre}\n" +
                                     $"Apellido: {empleadoEncontrado.Apellido}\n" +
                                     $"DNI: {empleadoEncontrado.DNI}\n" +
                                     $"Teléfono: {empleadoEncontrado.Telefono}\n" +
                                     $"Email: {empleadoEncontrado.Email}";

                    // Mostrar el mensaje en un cuadro de mensaje
                    MessageBox.Show(mensaje, "Información del Empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // El empleado no fue encontrado
                    MessageBox.Show("No se encontró un empleado con el legajo proporcionado.", "Empleado no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // El valor del legajo no es un número válido
                MessageBox.Show("El valor del legajo no es válido.", "Error de legajo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buscarPorLegajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxCrearusuario.Visible = false;
            groupBoxEliminar.Visible = false;
            groupBoxCrearEmpleado.Visible = false;
            groupBoxBuscarEmpleado.Visible = false;
            // Obtener el tamaño de la ventana del formulario
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Obtener el tamaño del groupBox
            int groupBoxWidth = groupBoxBuscarEmpleado.Width;
            int groupBoxHeight = groupBoxBuscarEmpleado.Height;

            // Calcular las coordenadas para centrar el groupBox en el medio de la ventana
            int groupBoxX = (formWidth - groupBoxWidth) / 2;
            int groupBoxY = (formHeight - groupBoxHeight) / 2;

            // Establecer la ubicación del groupBox
            groupBoxBuscarEmpleado.Location = new Point(groupBoxX, groupBoxY);
            groupBoxBuscarEmpleado.Visible = true;


            groupBoxBuscarEmpleado.Visible = true;
        }

        private void MenuPrincipalAdministrador_Load(object sender, EventArgs e)
        {

        }
    }
}
