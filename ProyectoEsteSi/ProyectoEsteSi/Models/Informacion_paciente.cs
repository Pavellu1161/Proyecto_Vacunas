using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoEsteSi.Models
{
    public class Informacion_paciente
    {
        [Key]
        public int Id_nino { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Identidad:")]

        public string Numero_identidad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Nombre completo:")]
        public string Nombre_nino { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Nombre del responsable:")]
        public string Nombre_responsabe { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Fecha de nacimineto:")]
        [DataType(DataType.Date)]

        public DateTime Fecha_nacimineto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Edad de captacion:")]
        public string Edad_cap { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Numero de contacto:")]

        public int Contacto { get; set; }
        public int Id_direccion { get; set; }

        [ForeignKey("Id_direccion")]
        public Direccion Direcciones { get; set; }

        public IEnumerable<Dosi> Dosis { get; set; }
    }
}
