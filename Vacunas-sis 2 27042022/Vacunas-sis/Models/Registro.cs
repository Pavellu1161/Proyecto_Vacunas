using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vacunas_sis.Models
{
    public class Registro
    {
        [Key]
        public int Id_registro { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Historia clinica:")]
        public string Numero_historia { get; set; }

        [Display(Name = "Número de dentidad paciente:")]
        public int Id_nino { get; set; }

        [Display(Name = "Fecha de aplicacion vacuna:")]
        public int Id_detalle_vacuna { get; set; }

        [ForeignKey("Id_nino")]
        public Informacion_nino Ninos { get; set; }

        [ForeignKey("Id_detalle_vacuna")]
        public Detalle_Vacuna Detalles { get; set; }


    }
}
