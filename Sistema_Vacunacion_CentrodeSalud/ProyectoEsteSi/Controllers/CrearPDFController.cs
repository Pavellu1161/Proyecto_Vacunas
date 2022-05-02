using Microsoft.AspNetCore.Mvc;
using ProyectoEsteSi.Models;
using Rotativa.AspNetCore;
using System.Threading.Tasks;
using ProyectoEsteSi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ProyectoEsteSi.ViewModels;

namespace Sistema_Vacunacion_CentrodeSalud.Controllers
{
    public class CrearPDFController : Controller
    {
        private readonly ApplicationDbContext _context; //Leer Conexion

        
        public CrearPDFController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET PACIENTE
        public async Task<IActionResult> PacientePDF()
        {

            /*
            var informacion_paciente = await _context.Pacientes
                .Include(i => i.Direcciones)
                .FirstOrDefaultAsync(m => m.Id_nino == id);
            */
            return new ViewAsPdf(await _context.Pacientes.Include(i =>i.Direcciones).ToListAsync())
            {
                // Establece el número de página.
                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12"
            };
        }

        //GET VACUNAS 
        public async Task<IActionResult> VacunasPDF()
        {

           
            return new ViewAsPdf(await _context.Vacunas.ToListAsync())
            {
            
                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12"
            };
        }


        //GET VACUNAS 
        public async Task<IActionResult> CitasPDF()
        {

            var citas = await _context.Citas.Include(c => c.Dosis).ToListAsync();

            List<RepCitaViewModel> repcitasViewModel = new List<RepCitaViewModel>();



            foreach (var item in citas)
            {
                RepCitaViewModel repcitaViewModel = new RepCitaViewModel();
                repcitaViewModel.Id = item.IdCita;
                repcitaViewModel.Fecha = item.Fecha;
                repcitaViewModel.Fecha_proxima = item.Fecha_proxima;
              

                var dosis = await _context.Dosis.Where(d => d.IdDosis == item.IdDosis).Include(d => d.Pacientes).FirstOrDefaultAsync();

                repcitaViewModel.Numero_identidad = dosis.Pacientes.Numero_identidad;
                repcitaViewModel.restante = dosis.restante;

                var dosisvacuna = await _context.Dosis.Where(v => v.Id_vacuna == item.Dosis.Id_vacuna).Include(d => d.Vacunas).FirstOrDefaultAsync();

                var informacion_paciente = await _context.Pacientes.Include(i => i.Direcciones).FirstOrDefaultAsync();
                if (informacion_paciente == null)
                {
                    return NotFound();
                }


                repcitaViewModel.Nombre_vacuna = dosisvacuna.Vacunas.Nombre_vacuna;

                repcitaViewModel.Nombre_Paciente = dosis.Pacientes.Nombre_nino;

                repcitaViewModel.Tel = dosis.Pacientes.Contacto;

                repcitaViewModel.DetalleDir = informacion_paciente.Direcciones.Detalle_direccion;

                repcitasViewModel.Add(repcitaViewModel);
            }






            return new ViewAsPdf(repcitasViewModel)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,

                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12"
            };

        }

        public async Task<IActionResult> ProximaPDF()
        {
            var applicationDbContext = _context.Dosis
                .Include(d => d.Pacientes)
                .Include(d => d.Vacunas)
                .Include(d => d.Citas.ToList()
                .Where(d => DateTime.Now.Day - d.Fecha_proxima.Day <= 7
                    & DateTime.Now.Day - d.Fecha_proxima.Day >= 0
                 ));

            return  new ViewAsPdf(await applicationDbContext.ToListAsync())
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,

                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12"
            };
        }



    }
}
