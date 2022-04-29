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
        [Display(Name = "Nombre:")]
        public string Nombre_nino { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Responsable:")]
        public string Nombre_responsabe { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Nacimineto:")]
        [DataType(DataType.Date)]

        public DateTime Fecha_nacimineto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Edad de captacion:")]
        public string Edad_cap { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Contacto:")]

        public int Contacto { get; set; }
        public int Id_direccion { get; set; }

        [ForeignKey("Id_direccion")]
        public Direccion Direcciones { get; set; }

        public IEnumerable<Dosi> Dosis { get; set; }
    }
}
