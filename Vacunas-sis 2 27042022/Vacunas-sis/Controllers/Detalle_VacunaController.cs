using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vacunas_sis.Data;
using Vacunas_sis.Models;

namespace Vacunas_sis.Controllers
{
    public class Detalle_VacunaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Detalle_VacunaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Detalle_Vacuna
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Detalle_Vacuna.Include(d => d.Vacunas);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Detalle_Vacuna/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle_Vacuna = await _context.Detalle_Vacuna
                .Include(d => d.Vacunas)
                .FirstOrDefaultAsync(m => m.Id_detalle_vacuna == id);
            if (detalle_Vacuna == null)
            {
                return NotFound();
            }

            return View(detalle_Vacuna);
        }

        // GET: Detalle_Vacuna/Create
        public IActionResult Create()
        {
            ViewData["Id_vacuna"] = new SelectList(_context.Set<Vacunas>(), "Id_vacuna", "Nombre_vacuna", "Abreviatura_vacuna");
            return View();
        }

        // POST: Detalle_Vacuna/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_detalle_vacuna,Fecha_aplicacion,Numero_dosis_aplicada,Numero_dosis_restantes,Fecha_proxima_dosis,Id_vacuna")] Detalle_Vacuna detalle_Vacuna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalle_Vacuna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_vacuna"] = new SelectList(_context.Set<Vacunas>(), "Id_vacuna", "Nombre_vacuna", detalle_Vacuna.Id_vacuna);
            return View(detalle_Vacuna);
        }

        // GET: Detalle_Vacuna/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle_Vacuna = await _context.Detalle_Vacuna.FindAsync(id);
            if (detalle_Vacuna == null)
            {
                return NotFound();
            }
            ViewData["Id_vacuna"] = new SelectList(_context.Set<Vacunas>(), "Id_vacuna", "Nombre_vacuna", detalle_Vacuna.Id_vacuna);
            return View(detalle_Vacuna);
        }

        // POST: Detalle_Vacuna/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_detalle_vacuna,Fecha_aplicacion,Numero_dosis_aplicada,Numero_dosis_restantes,Fecha_proxima_dosis,Id_vacuna")] Detalle_Vacuna detalle_Vacuna)
        {
            if (id != detalle_Vacuna.Id_detalle_vacuna)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalle_Vacuna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Detalle_VacunaExists(detalle_Vacuna.Id_detalle_vacuna))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_vacuna"] = new SelectList(_context.Set<Vacunas>(), "Id_vacuna", "Nombre_vacuna", detalle_Vacuna.Id_vacuna);
            return View(detalle_Vacuna);
        }

        // GET: Detalle_Vacuna/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle_Vacuna = await _context.Detalle_Vacuna
                .Include(d => d.Vacunas)
                .FirstOrDefaultAsync(m => m.Id_detalle_vacuna == id);
            if (detalle_Vacuna == null)
            {
                return NotFound();
            }

            return View(detalle_Vacuna);
        }

        // POST: Detalle_Vacuna/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalle_Vacuna = await _context.Detalle_Vacuna.FindAsync(id);
            _context.Detalle_Vacuna.Remove(detalle_Vacuna);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Detalle_VacunaExists(int id)
        {
            return _context.Detalle_Vacuna.Any(e => e.Id_detalle_vacuna == id);
        }
    }
}
