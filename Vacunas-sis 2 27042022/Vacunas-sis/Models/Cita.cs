using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vacunas_sis.Models
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }
        public int IdDosis { get; set; }

        public DateTime Fecha { get; set; }

        public DateTime Fecha_proxima { get; set; }

        [ForeignKey("IdDosis")]
        public Dosis Doses { get; set; }
    }
}
