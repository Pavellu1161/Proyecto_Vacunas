using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoEsteSi.Models
{
    public class Dosi
    {
        [Key]
        public int IdDosis { get; set; }
        public int Id_nino { get; set; }
        public int Id_vacuna { get; set; }
        [Display(Name = "Dosis restante:")]
        public int restante { get; set; }

        [ForeignKey("Id_nino")]
        public Informacion_paciente Pacientes { get; set; }

        [ForeignKey("Id_vacuna")]
        public Vacuna Vacunas { get; set; }

        public IEnumerable<Cita> Citas { get; set; }
    }
}
