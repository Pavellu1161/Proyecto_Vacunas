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
    public class Informacion_ninoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Informacion_ninoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Informacion_nino
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Informacion_nino.Include(i => i.Direcciones);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Informacion_nino/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacion_nino = await _context.Informacion_nino
                .Include(i => i.Direcciones)
                .FirstOrDefaultAsync(m => m.Id_nino == id);
            if (informacion_nino == null)
            {
                return NotFound();
            }

            return View(informacion_nino);
        }

        // GET: Informacion_nino/Create
        public IActionResult Create()
        {

            ViewData["Id_direccion"] = new SelectList(_context.Direccion, "Id_direccion", "Cuidad","Barrio", "Detalle_direccion");
            return View();
        }

        // POST: Informacion_nino/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_nino,Numero_identidad,Nombre_nino,Nombre_responsabe,Fecha_nacimineto,Edad_cap,Id_contacto,Id_direccion")] Informacion_nino informacion_nino)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informacion_nino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_direccion"] = new SelectList(_context.Direccion, "Id_direccion", "Detalle_direccion", informacion_nino.Id_direccion);
            return View(informacion_nino);
        }

        // GET: Informacion_nino/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacion_nino = await _context.Informacion_nino.FindAsync(id);
            if (informacion_nino == null)
            {
                return NotFound();
            }
            ViewData["Id_direccion"] = new SelectList(_context.Direccion, "Id_direccion", "Detalle_direccion", informacion_nino.Id_direccion);
            return View(informacion_nino);
        }

        // POST: Informacion_nino/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_nino,Numero_identidad,Nombre_nino,Nombre_responsabe,Fecha_nacimineto,Edad_cap,Id_contacto,Id_direccion")] Informacion_nino informacion_nino)
        {
            if (id != informacion_nino.Id_nino)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informacion_nino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Informacion_ninoExists(informacion_nino.Id_nino))
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
            
            ViewData["Id_direccion"] = new SelectList(_context.Direccion, "Id_direccion", "Detalle_direccion", informacion_nino.Id_direccion);
            return View(informacion_nino);
        }

        // GET: Informacion_nino/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacion_nino = await _context.Informacion_nino
                .Include(i => i.Direcciones)
                .FirstOrDefaultAsync(m => m.Id_nino == id);
            if (informacion_nino == null)
            {
                return NotFound();
            }

            return View(informacion_nino);
        }

        // POST: Informacion_nino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var informacion_nino = await _context.Informacion_nino.FindAsync(id);
            _context.Informacion_nino.Remove(informacion_nino);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Informacion_ninoExists(int id)
        {
            return _context.Informacion_nino.Any(e => e.Id_nino == id);
        }
    }
}
