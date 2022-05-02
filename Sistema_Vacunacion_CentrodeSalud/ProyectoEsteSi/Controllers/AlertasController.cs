using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoEsteSi.Data;
using ProyectoEsteSi.Models;

namespace ProyectoEsteSi.Controllers
{
    public class AlertasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlertasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alertas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Dosis
                .Include(d => d.Pacientes)
                .Include(d => d.Vacunas)
                .Include(d => d.Citas.ToList()
                .Where(d => DateTime.Now.Day - d.Fecha_proxima.Day <= 7
                    & DateTime.Now.Day - d.Fecha_proxima.Day >= 0
                 ));

            return View(await applicationDbContext.ToListAsync());
        }
    }
}
