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

namespace NegocioIndumentariaEscritorio
{
    public partial class MenuPrincipalGerente : Form
    {
        List<Venta> listaVentas;

        public MenuPrincipalGerente()
        {
            InitializeComponent();
            groupBoxReporte.Visible = false;
            listaVentas = new List<Venta>();
            
        }

        private void verReporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            // Obtener el tamaño de la ventana del formulario
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Obtener el tamaño del groupBox
            int groupBoxWidth = groupBoxReporte.Width;
            int groupBoxHeight = groupBoxReporte.Height;

            // Calcular las coordenadas para centrar el groupBox en el medio de la ventana
            int groupBoxX = (formWidth - groupBoxWidth) / 2;
            int groupBoxY = (formHeight - groupBoxHeight) / 2;

            // Establecer la ubicación del groupBox
            groupBoxReporte.Location = new Point(groupBoxX, groupBoxY);

            groupBoxReporte.Visible = true;

            groupBoxReporte.Visible = true;
            VentaCon ventaConexion = new VentaCon();
            listaVentas.AddRange(ventaConexion.ObtenerTodasLasVentas());

            // Extraer los nombres de los clientes y usuarios
            List<string> nombresClientes = listaVentas.Select(v => v.clienteV.Nombre).ToList();
            List<string> nombresUsuarios = listaVentas.Select(v => v.usuarioV.Nombre).ToList();

            // Configurar las columnas del DataGridView
            dataGridViewReporte.AutoGenerateColumns = false;
            dataGridViewReporte.Columns.Clear();

            // Columna ID Venta
            DataGridViewTextBoxColumn idVentaColumn = new DataGridViewTextBoxColumn();
            idVentaColumn.DataPropertyName = "IdVenta";
            idVentaColumn.HeaderText = "ID Venta";
            dataGridViewReporte.Columns.Add(idVentaColumn);

            // Columna Fecha Venta
            DataGridViewTextBoxColumn fechaVentaColumn = new DataGridViewTextBoxColumn();
            fechaVentaColumn.DataPropertyName = "Fecha";
            fechaVentaColumn.HeaderText = "Fecha Venta";
            dataGridViewReporte.Columns.Add(fechaVentaColumn);

            // Columna Total
            DataGridViewTextBoxColumn totalColumn = new DataGridViewTextBoxColumn();
            totalColumn.DataPropertyName = "Total";
            totalColumn.HeaderText = "Total";
            dataGridViewReporte.Columns.Add(totalColumn);

            // Columna Cliente (con nombres de clientes)
            DataGridViewTextBoxColumn clienteColumn = new DataGridViewTextBoxColumn();
            clienteColumn.HeaderText = "Cliente";
            dataGridViewReporte.Columns.Add(clienteColumn);

            // Columna Usuario (con nombres de usuarios)
            DataGridViewTextBoxColumn usuarioColumn = new DataGridViewTextBoxColumn();
            usuarioColumn.HeaderText = "Usuario";
            dataGridViewReporte.Columns.Add(usuarioColumn);

            // Agregar las filas necesarias al DataGridView
            for (int i = 0; i < listaVentas.Count; i++)
            {
                dataGridViewReporte.Rows.Add();
            }

            // Asignar los nombres de clientes y usuarios a las celdas correspondientes
            for (int i = 0; i < listaVentas.Count; i++)
            {
                dataGridViewReporte.Rows[i].Cells[0].Value = listaVentas[i].IdVenta;
                dataGridViewReporte.Rows[i].Cells[1].Value = listaVentas[i].Fecha;
                dataGridViewReporte.Rows[i].Cells[2].Value = listaVentas[i].Total;
                dataGridViewReporte.Rows[i].Cells[3].Value = nombresClientes[i];
                dataGridViewReporte.Rows[i].Cells[4].Value = nombresUsuarios[i];
            }

            // Asignar la lista de ventas como fuente de datos del DataGridView
            //dataGridViewReporte.DataSource = listaVentas;

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login ingreso = new Login();
            ingreso.Show();
        }
    }
}
