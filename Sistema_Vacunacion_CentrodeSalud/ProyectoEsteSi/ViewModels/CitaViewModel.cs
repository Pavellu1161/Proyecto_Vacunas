using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoEsteSi.ViewModels
{
    public class CitaViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Identidad:")]
        public string Numero_identidad { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha aplicación:")]
        public DateTime Fecha { get; set; }
        [Display(Name = "Nombre vacuna:")]
        public string Nombre_vacuna { get; set; }
        [Display(Name = "Dosis restantes:")]
        public int restante { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha proxima aplicación:")]
        public DateTime Fecha_proxima { get; set; }
    }
}
