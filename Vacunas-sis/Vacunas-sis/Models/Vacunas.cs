using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vacunas_sis.Models
{
    public class Vacunas
    {
        [Key]
        public int Id_vacuna { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Nombre de la Vacuna:")]
        [StringLength(60, ErrorMessage = "No debe tener más de 30 caracteres.")]
        [MinLength(3, ErrorMessage = "Debe tener más de 1 caracteres.")]
        public string Nombre_vacuna { get; set; }

        [Display(Name = "Abreviacion de la vacuna")]
        public string Abreviatura_vacuna { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Numero de dosis aplicables:")]
        public int Dosis_aplicables { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Edad para la aplicacion de vacuna:")]
        public string Edad_aplicable { get; set; }

        public IEnumerable<Detalle_Vacuna> Detalles { get; set; }


    }
}
