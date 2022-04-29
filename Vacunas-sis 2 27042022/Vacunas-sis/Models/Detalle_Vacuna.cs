using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vacunas_sis.Models
{
    public class Detalle_Vacuna
    {

        [Key]
        public int Id_detalle_vacuna { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Fecha aplicacion de vacuna:")]
        
        public DateTime Fecha_aplicacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Dosis aplicada:")]
        public string Numero_dosis_aplicada { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Dosis restantes:")]
        public int Numero_dosis_restantes { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Fecha proxima dosis:")]
        public DateTime Fecha_proxima_dosis { get; set; }

        [Display(Name = "Vacuna:")]
        public int Id_vacuna { get; set; }

        [ForeignKey("Id_vacuna")]
        public Vacunas Vacunas { get; set; }

        public IEnumerable<Registro> Registros { get; set; }
    }
}
