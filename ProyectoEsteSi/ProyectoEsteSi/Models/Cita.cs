using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoEsteSi.Models
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }
        public int IdDosis { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        [DataType(DataType.Date)]   
        public DateTime Fecha_proxima { get; set; }

        [ForeignKey("IdDosis")]
        public Dosi Dosis { get; set; }
    }
}
