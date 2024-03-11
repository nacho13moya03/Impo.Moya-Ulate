using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoSC_601.Entities
{
    public class PedidoEnt
    {
        public long ID_Pedido { get; set; }

        public long ID_Cliente { get; set; }
        public string ID_Transaccion { get; set; }

        public long ID_Factura { get; set; }

        public  int Estado { get; set; }

        public string direccionPedido { get; set; }

        public string identificacionCliente { get; set; }


    }
}