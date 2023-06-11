using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaClasesProyectoVentas;

namespace BibliotecaClasesProyectoVentas
{
    public class Venta
    {
            public int IdVenta { get; set; }
            public Usuario usuarioV { get; set; }
            public Cliente clienteV{ get; set; }
            public DateTime Fecha { get; set; }
            public decimal Total { get; set; }

            public Venta(int IdVenta, DateTime fecha, decimal total)
            {
                this.IdVenta = IdVenta;
                Fecha = fecha;
                Total = total;
            }
        }
}
