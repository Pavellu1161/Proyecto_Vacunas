using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoEsteSi.ViewModels
{
    public class RepCitaViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Identidad:")]
        public string Numero_identidad { get; set; }

        [Display(Name = "Nombre:")]
        public string Nombre_Paciente { get; set; }
      
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

        [Display(Name = "Direccion:")]
        public string DetalleDir { get; set; }

        [Display(Name = "Telefono:")]
        public int Tel { get; set; }
        internal void Add(RepCitaViewModel repcitaViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
