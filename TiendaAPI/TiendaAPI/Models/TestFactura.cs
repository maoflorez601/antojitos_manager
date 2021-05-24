using System;
using System.Collections.Generic;

#nullable disable

namespace TiendaAPI.Models
{
    public partial class TestFactura
    {
        public TestFactura()
        {
            TestFacturaDetalles = new HashSet<TestFacturaDetalle>();
        }

        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal ValorTotal { get; set; }

        public virtual TestCliente IdClienteNavigation { get; set; }
        public virtual ICollection<TestFacturaDetalle> TestFacturaDetalles { get; set; }
    }
}
