using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vacunas_sis.Models
{
    public class Direccion
    {
        [Key]
        public int Id_direccion { get; set; }

        [Display(Name = "Nombre de la cuidad donde recide:")]
        public string Cuidad { get; set; }

        [Display(Name = "Detalles de direccion:")]
        public string Detalle_direccion { get; set; }

        public IEnumerable<Informacion_nino> Ninos { get; set; }

    }
}
