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
    public class RegistroesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistroesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Registroes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Registro.Include(r => r.Detalles).Include(r => r.Ninos);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Registroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registro = await _context.Registro
                .Include(r => r.Detalles)
                .Include(r => r.Ninos)
                .FirstOrDefaultAsync(m => m.Id_registro == id);
            if (registro == null)
            {
                return NotFound();
            }

            return View(registro);
        }

        // GET: Registroes/Create
        public IActionResult Create()
        {
            ViewData["Id_detalle_vacuna"] = new SelectList(_context.Detalle_Vacuna, "Id_detalle_vacuna", "Numero_dosis_aplicada");
            ViewData["Id_nino"] = new SelectList(_context.Informacion_nino, "Id_nino", "Edad_cap");
            return View();
        }

        // POST: Registroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_registro,Numero_historia,Id_nino,Id_detalle_vacuna")] Registro registro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_detalle_vacuna"] = new SelectList(_context.Detalle_Vacuna, "Id_detalle_vacuna", "Numero_dosis_aplicada", registro.Id_detalle_vacuna);
            ViewData["Id_nino"] = new SelectList(_context.Informacion_nino, "Id_nino", "Edad_cap", registro.Id_nino);
            return View(registro);
        }

        // GET: Registroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registro = await _context.Registro.FindAsync(id);
            if (registro == null)
            {
                return NotFound();
            }
            ViewData["Id_detalle_vacuna"] = new SelectList(_context.Detalle_Vacuna, "Id_detalle_vacuna", "Numero_dosis_aplicada", registro.Id_detalle_vacuna);
            ViewData["Id_nino"] = new SelectList(_context.Informacion_nino, "Id_nino", "Edad_cap", registro.Id_nino);
            return View(registro);
        }

        // POST: Registroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_registro,Numero_historia,Id_nino,Id_detalle_vacuna")] Registro registro)
        {
            if (id != registro.Id_registro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroExists(registro.Id_registro))
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
            ViewData["Id_detalle_vacuna"] = new SelectList(_context.Detalle_Vacuna, "Id_detalle_vacuna", "Numero_dosis_aplicada", registro.Id_detalle_vacuna);
            ViewData["Id_nino"] = new SelectList(_context.Informacion_nino, "Id_nino", "Edad_cap", registro.Id_nino);
            return View(registro);
        }

        // GET: Registroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registro = await _context.Registro
                .Include(r => r.Detalles)
                .Include(r => r.Ninos)
                .FirstOrDefaultAsync(m => m.Id_registro == id);
            if (registro == null)
            {
                return NotFound();
            }

            return View(registro);
        }

        // POST: Registroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registro = await _context.Registro.FindAsync(id);
            _context.Registro.Remove(registro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroExists(int id)
        {
            return _context.Registro.Any(e => e.Id_registro == id);
        }
    }
}
