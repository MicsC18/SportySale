using DAL.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace NegocioIndumentariaEscritorio
{
    public partial class MenuPrincipalVendedor : Form
    {
        DataTable dtUsuario;
        decimal totalVenta = 0;
        public MenuPrincipalVendedor(DataTable dtUsuario)
        {
            InitializeComponent();
            groupBoxEP.Visible = false;
            groupBoxELIMINAR.Visible = false;
            groupBoxVender.Visible = false;
            this.dtUsuario = dtUsuario;
            CargarProductos();
        }
        #region EDITAR_PRODUCTO
        private void editarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxELIMINAR.Visible = false;
            groupBoxVender.Visible = false;
            // Obtener el tamaño de la ventana del formulario
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Obtener el tamaño del groupBox
            int groupBoxWidth = groupBoxEP.Width;
            int groupBoxHeight = groupBoxEP.Height;

            // Calcular las coordenadas para centrar el groupBox en el medio de la ventana
            int groupBoxX = (formWidth - groupBoxWidth) / 2;
            int groupBoxY = (formHeight - groupBoxHeight) / 2;

            // Establecer la ubicación del groupBox
            groupBoxEP.Location = new Point(groupBoxX, groupBoxY);
            groupBoxEP.Visible = true;
            ProductoCon productoCon = new ProductoCon(); // genero la conexion
            DataTable productos = productoCon.DevolverTodosLosProductos();

            comboBoxProductosEditar.DataSource = productos;
            comboBoxProductosEditar.DisplayMember = "Descripcion";
            comboBoxProductosEditar.ValueMember = "Id_Producto";
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            // Obtener el ID del producto seleccionado en el comboBoxEditar
            int idProducto = (int)comboBoxProductosEditar.SelectedValue;

            // Obtener los valores de los TextBox
            string descripcion = textBoxDescripcion.Text;
            int cantidad = int.Parse(textBoxCantidad.Text);
            double precio = double.Parse(textBoxPrecio.Text);

            // Llamar al método EditarProducto
            ProductoCon productoCon = new ProductoCon();
            bool productoEditado = productoCon.EditarProducto(idProducto, descripcion, cantidad, precio);
            CargarProductos();
            if (productoEditado)
            {
                // Limpiar el contenido de los TextBox
                textBoxDescripcion.Text = string.Empty;
                textBoxCantidad.Text = string.Empty;
                textBoxPrecio.Text = string.Empty;

                // Mostrar un mensaje de éxito
                MessageBox.Show("El producto ha sido editado correctamente.");
            }
            else
            {
                MessageBox.Show("No se pudo editar el producto.", "Editar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buttonCargar_Click(object sender, EventArgs e)
        {
            // Obtener el id del producto seleccionado en el ComboBox
            int idProducto = (int)comboBoxProductosEditar.SelectedValue;

            // Crear una instancia de la clase ProductoCon
            ProductoCon productoCon = new ProductoCon();

            // Obtener el producto seleccionado a partir de su id
            DataTable productoSeleccionado = productoCon.ObtenerProducto(idProducto);

            // Verificar si se encontró un producto con el id seleccionado
            if (productoSeleccionado.Rows.Count > 0)
            {
                // Obtener los datos del producto desde la primera fila del DataTable
                string descripcion = productoSeleccionado.Rows[0]["Descripcion"].ToString();
                int precio = Convert.ToInt32(productoSeleccionado.Rows[0]["Precio"]);
                int cantidad = Convert.ToInt32(productoSeleccionado.Rows[0]["Cantidad"]);


                // Cargar los datos del producto en los TextBox
                textBoxDescripcion.Text = descripcion;
                textBoxCantidad.Text = cantidad.ToString();
                textBoxPrecio.Text = precio.ToString();
            }
            else
            {
                // Limpiar los TextBox si no se encontró un producto
                textBoxDescripcion.Text = string.Empty;
                textBoxCantidad.Text = string.Empty;
                textBoxPrecio.Text = string.Empty;
            }
        }
        #endregion

        #region ELIMINAR_PRODUCTO
        private void eliminarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxEP.Visible = false;
            groupBoxVender.Visible = false;
            // Obtener el tamaño de la ventana del formulario
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Obtener el tamaño del groupBox
            int groupBoxWidth = groupBoxELIMINAR.Width;
            int groupBoxHeight = groupBoxELIMINAR.Height;

            // Calcular las coordenadas para centrar el groupBox en el medio de la ventana
            int groupBoxX = (formWidth - groupBoxWidth) / 2;
            int groupBoxY = (formHeight - groupBoxHeight) / 2;

            // Establecer la ubicación del groupBox
            groupBoxELIMINAR.Location = new Point(groupBoxX, groupBoxY);
            groupBoxELIMINAR.Visible = true;

            ProductoCon productoCon = new ProductoCon(); // genero la conexion
            DataTable productos = productoCon.DevolverTodosLosProductos();


            comboBoxEliminar.DataSource = productos;
            comboBoxEliminar.DisplayMember = "Descripcion";
            comboBoxEliminar.ValueMember = "Id_Producto";

        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un producto
            if (comboBoxEliminar.SelectedItem != null)
            {
                // Obtener el ID del producto seleccionado en el comboBoxEliminar
                int idProducto = (int)comboBoxEliminar.SelectedValue;

                // Borrar el producto de SQL
                ProductoCon productoCon = new ProductoCon();
                bool productoBorrado = productoCon.BorrarProducto(idProducto);

                if (productoBorrado == true)
                {
                    // Obtener el índice del elemento seleccionado en el ComboBox
                    int indiceSeleccionado = comboBoxEliminar.SelectedIndex;

                    // Volver a cargar los productos en el ComboBox
                    CargarProductos();

                    // Seleccionar el elemento posterior al eliminado
                    if (comboBoxEliminar.Items.Count > 0)
                    {
                        if (indiceSeleccionado >= comboBoxEliminar.Items.Count)
                        {
                            indiceSeleccionado = comboBoxEliminar.Items.Count - 1;
                        }
                        comboBoxEliminar.SelectedIndex = indiceSeleccionado;
                    }

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("El producto ha sido eliminado correctamente.");
                }
                else
                {
                    MessageBox.Show("No se pudo borrar el producto.", "Borrar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para eliminar.");
            }
        }
        #endregion

        private void CargarProductos()
        {
            ProductoCon productoCon = new ProductoCon();
            // Obtener los datos de los productos desde la fuente de datos (base de datos, por ejemplo)
            DataTable dtProductos = productoCon.DevolverTodosLosProductos(); // Llamada al método que obtiene los productos

            // Configurar el ComboBox con los datos de los productos
            comboBoxEliminar.DataSource = dtProductos;
            comboBoxEliminar.DisplayMember = "Descripcion";
            comboBoxEliminar.ValueMember = "Id_Producto";

            comboBoxProductosEditar.DataSource = dtProductos;
            comboBoxProductosEditar.DisplayMember = "Descripcion";
            comboBoxProductosEditar.ValueMember = "Id_Producto";

            comboBoxProductosVenta.DataSource = dtProductos;
            comboBoxProductosVenta.DisplayMember = "Descripcion";
            comboBoxProductosVenta.ValueMember = "Id_Producto";
        }




        private void venderProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxEP.Visible = false;
            groupBoxELIMINAR.Visible = false;
            labelUsername.Text = "VENDEDOR";
            // Obtener el tamaño de la ventana del formulario
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Obtener el tamaño del groupBox
            int groupBoxWidth = groupBoxVender.Width;
            int groupBoxHeight = groupBoxVender.Height;

            // Calcular las coordenadas para centrar el groupBox en el medio de la ventana
            int groupBoxX = (formWidth - groupBoxWidth) / 2;
            int groupBoxY = (formHeight - groupBoxHeight) / 2;

            // Establecer la ubicación del groupBox
            groupBoxVender.Location = new Point(groupBoxX, groupBoxY);
            groupBoxVender.Visible = true;
            // Obtener el valor del campo "Username" de la primera fila de la DataTable
            string username = dtUsuario.Rows[0]["Username"].ToString();

            // Convertir el valor a mayúsculas
            username = username.ToUpper();

            // Asignar el valor al Text del Label
            labelUsername.Text += "  -> "+username;
        }

        private void buttonVender_Click(object sender, EventArgs e)
        {
            int idDireccion = ObtenerIdDireccion(); // Obtener el ID de dirección desde algún lugar
            int dni = Convert.ToInt32(textBoxDNI.Text);
            string nombre = textBoxNombreVenta.Text;
            string apellido = textBoxApellidoVenta.Text;
            string email = textBoxeMailVenta.Text;
            int telefono = Convert.ToInt32(textBoxTelefonoVenta.Text);
            // creacion del Objeto clienteCon, de la clase ClienteCon
            ClienteCon clienteCon = new ClienteCon();
            bool clienteRegistrado = clienteCon.RegistrarCliente(idDireccion, dni, nombre, apellido, email, telefono);
            if (clienteRegistrado==true)
            {
                MessageBox.Show("Cliente registrado correctamente");
            }
            else
            {
                MessageBox.Show("Error al registrar el cliente");
            }

            int idProducto = Convert.ToInt32(comboBoxProductosVenta.SelectedValue);

            // Crear una instancia de la clase ProductoCon
            ProductoCon productoCon = new ProductoCon();

            // Obtener el producto seleccionado a partir de su id
            DataTable productoSeleccionado = productoCon.ObtenerProducto(idProducto);

            // Obtener los datos del producto desde la primera fila del DataTable
            
            int precio = Convert.ToInt32(productoSeleccionado.Rows[0]["Precio"]);
            int id_cliente = clienteCon.ObtenerIdCliente(nombre,dni);
            int id_vendedor = Convert.ToInt32(dtUsuario.Rows[0]["Id_Usuario"]);
            DateTime fecha = DateTime.Now;

            decimal total = totalVenta; // Aquí puedes calcular el total de la venta según tus necesidades

            VentaCon ventaCon = new VentaCon();
            bool ventaRegistrada = ventaCon.RegistrarVenta(id_cliente, id_vendedor, fecha, total);

            if (ventaRegistrada)
            {
                MessageBox.Show("Venta registrada correctamente");
            }
            else
            {
                MessageBox.Show("Error al registrar la venta");
            }

        }
        private int ObtenerIdDireccion()  // Esto funciona para el id_direccion del cliente
        {
            Random random = new Random();
            int idDireccion = random.Next(10, 51); // Genera un número aleatorio entre 10 y 50 (inclusive)
            return idDireccion;
        }

        private void buttonCargarProducto_Click(object sender, EventArgs e)
        {
            ProductoCon productoCon = new ProductoCon();
            string descripcion = comboBoxProductosVenta.Text;
            decimal precio = productoCon.ObtenerPrecioPorDescripcion(descripcion);
            int cantidad = productoCon.ObtenerCantidadPorDescripcion(descripcion);
           
            if (precio > 0)
            {
                string productoSeleccionado = descripcion + " - $" + precio.ToString()+" - Cantidad = "+ cantidad.ToString();
                listBox1.Items.Add(productoSeleccionado);
                totalVenta += precio;
                labelTotalAcumulado.Text = "$"+totalVenta.ToString();
            }
            else
            {
                MessageBox.Show("No se encontró el precio para la descripción del producto");
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login ingreso = new Login();
            ingreso.Show();
        }

        private void groupBoxVender_Enter(object sender, EventArgs e)
        {

        }
    }
}
