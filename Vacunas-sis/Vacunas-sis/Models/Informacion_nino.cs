using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vacunas_sis.Models
{
    public class Informacion_nino
    {
        [Key]
        public int Id_nino { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Número de identidad del niño:")]
        [StringLength(60, ErrorMessage = "No debe tener más de 15 caracteres.")]
        [MinLength(3, ErrorMessage = "Debe tener más de 1 caracteres.")]
        public string Numero_identidad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Nombre completo del niño:")]
        [StringLength(60, ErrorMessage = "No debe tener más de 30 caracteres.")]
        [MinLength(3, ErrorMessage = "Debe tener más de 5 caracteres.")]
        public string Nombre_nino { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Nombre del responsable:")]
        [StringLength(60, ErrorMessage = "No debe tener más de 30 caracteres.")]
        [MinLength(3, ErrorMessage = "Debe tener más de 5 caracteres.")]
        public string Nombre_responsabe { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Fecha de nacimineto:")]
        public DateTime Fecha_nacimineto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Edad de captacion:")]
        public string Edad_cap { get; set; }

        public int Id_contacto { get; set; }
        public int Id_direccion { get; set; }

        [ForeignKey("Id_contacto")]
        public Contacto Contactos { get; set; }

        [ForeignKey("Id_direccion")]
        public Direccion Direcciones { get; set; }

        public IEnumerable<Registro> Registros { get; set; }
    }
}
