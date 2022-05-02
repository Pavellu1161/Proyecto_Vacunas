using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoEsteSi.Common;
using ProyectoEsteSi.Data;
using ProyectoEsteSi.Models;

namespace ProyectoEsteSi.Controllers
{
    public class DosisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int RecordsPerpage = 10;
        private Pagination<Dosi> paginationPaquete;

        public DosisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dosis
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int totalRecords = 0;

            if (search == null)
            {
                search = "";
            }

            totalRecords = await _context.Dosis.CountAsync(
                d => d.Id_nino.ToString().Contains(search));

            var paquetes = await _context.Dosis.Where(d => d.Id_nino.ToString().Contains(search)).Include(d => d.Pacientes)
                .Include(d => d.Vacunas).ToListAsync();

            var paqueteresult = paquetes.OrderBy(o => o.Id_nino.ToString())
                .Skip((page - 1) * RecordsPerpage)
                .Take(RecordsPerpage);

            var totalPages = (int)Math.Ceiling((double)totalRecords / RecordsPerpage);

            paginationPaquete = new Pagination<Dosi>()
            {
                RecordsPerPage = this.RecordsPerpage,
                TotalRecords = totalRecords,
                TotalPage = totalPages,
                CurrentPage = page,
                Search = search,
                Result = paqueteresult
            };


            return View(paginationPaquete);
        }

        // GET: Dosis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dosi = await _context.Dosis
                .Include(d => d.Pacientes)
                .Include(d => d.Vacunas)
                .FirstOrDefaultAsync(m => m.IdDosis == id);
            if (dosi == null)
            {
                return NotFound();
            }

            return View(dosi);
        }

        // GET: Dosis/Create
        public IActionResult Create()
        {
            ViewData["Id_nino"] = new SelectList(_context.Pacientes, "Id_nino", "Numero_identidad");
            ViewData["Id_vacuna"] = new SelectList(_context.Vacunas, "Id_vacuna", "Nombre_vacuna");
            return View();
        }

        // POST: Dosis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDosis,Id_nino,Id_vacuna,restante")] Dosi dosi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dosi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_nino"] = new SelectList(_context.Pacientes, "Id_nino", "Numero_identidad", dosi.Id_nino);
            ViewData["Id_vacuna"] = new SelectList(_context.Vacunas, "Id_vacuna", "Nombre_vacuna", dosi.Id_vacuna);
            return View(dosi);
        }

        // GET: Dosis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dosi = await _context.Dosis.FindAsync(id);
            if (dosi == null)
            {
                return NotFound();
            }
            ViewData["Id_nino"] = new SelectList(_context.Pacientes, "Id_nino", "Numero_identidad", dosi.Id_nino);
            ViewData["Id_vacuna"] = new SelectList(_context.Vacunas, "Id_vacuna", "Nombre_vacuna", dosi.Id_vacuna);
            return View(dosi);
        }

        // POST: Dosis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDosis,Id_nino,Id_vacuna,restante")] Dosi dosi)
        {
            if (id != dosi.IdDosis)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dosi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DosiExists(dosi.IdDosis))
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
            ViewData["Id_nino"] = new SelectList(_context.Pacientes, "Id_nino", "Numero_identidad", dosi.Id_nino);
            ViewData["Id_vacuna"] = new SelectList(_context.Vacunas, "Id_vacuna", "Nombre_vacuna", dosi.Id_vacuna);
            return View(dosi);
        }

        // GET: Dosis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dosi = await _context.Dosis
                .Include(d => d.Pacientes)
                .Include(d => d.Vacunas)
                .FirstOrDefaultAsync(m => m.IdDosis == id);
            if (dosi == null)
            {
                return NotFound();
            }

            return View(dosi);
        }

        // POST: Dosis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dosi = await _context.Dosis.FindAsync(id);
            _context.Dosis.Remove(dosi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DosiExists(int id)
        {
            return _context.Dosis.Any(e => e.IdDosis == id);
        }
    }
}
