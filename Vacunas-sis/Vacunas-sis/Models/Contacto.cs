using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vacunas_sis.Models
{
    public class Contacto
    {

        [Key]
        public int Id_contacto { get; set; }

        [Display(Name = "Nombre del encargado:")]
        public string Nombre_responsable { get; set; }

        [Display(Name = "Tipo contacto:")]
        public string Tipo_de_contacto { get; set; }

        [Display(Name = "Detalle de contacto:")]
        public string Detalle { get; set; }

        public IEnumerable<Informacion_nino> Ninos { get; set; }


    }
}
