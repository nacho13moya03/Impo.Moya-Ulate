using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSC_601.Entities
{
    public class ComprasEnt
    {
        public int IdCompras { get; set; }

        public long Empresa { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public string Concepto { get; set; }

        public int Cantidad { get; set; }

        public decimal Total { get; set; }
    }
}