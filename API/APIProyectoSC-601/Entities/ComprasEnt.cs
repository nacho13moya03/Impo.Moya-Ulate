using System;

namespace APIProyectoSC_601.Entities
{
    public class ComprasEnt
    {
        public int IdCompras { get; set; }

        public long? Empresa { get; set; }

        public DateTime Fecha { get; set; }

        public string Concepto { get; set; }

        public int Cantidad { get; set; }

        public decimal Total { get; set; }
    }

}