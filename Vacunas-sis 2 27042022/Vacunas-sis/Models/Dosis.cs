using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vacunas_sis.Models
{
    public class Dosis
    {
        [Key]
        public int IdDosis { get; set; }
        public int Id_nino { get; set; }
        public int Id_vacuna { get; set; }
        public int restante { get; set; }

        [ForeignKey("Id_nino")]
        public Informacion_nino Ninos { get; set; }

        [ForeignKey("Id_vacuna")]
        public Vacunas Vacunas { get; set; }

        public IEnumerable<Cita> Citas { get; set; }
    }
}
