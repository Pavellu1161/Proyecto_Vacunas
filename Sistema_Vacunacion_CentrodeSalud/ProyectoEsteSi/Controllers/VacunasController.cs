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
    public class VacunasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int RecordsPerpage = 10;
        private Pagination<Vacuna> paginationPaquete;

        public VacunasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vacunas
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int totalRecords = 0;

            if (search == null)
            {
                search = "";
            }

            totalRecords = await _context.Vacunas.CountAsync(
                d => d.Nombre_vacuna.Contains(search));

            var paquetes = await _context.Vacunas.Where(d => d.Nombre_vacuna.Contains(search)).ToListAsync();

            var paqueteresult = paquetes.OrderBy(o => o.Nombre_vacuna)
                .Skip((page - 1) * RecordsPerpage)
                .Take(RecordsPerpage);

            var totalPages = (int)Math.Ceiling((double)totalRecords / RecordsPerpage);

            paginationPaquete = new Pagination<Vacuna>()
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

        // GET: Vacunas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacuna = await _context.Vacunas
                .FirstOrDefaultAsync(m => m.Id_vacuna == id);
            if (vacuna == null)
            {
                return NotFound();
            }

            return View(vacuna);
        }

        // GET: Vacunas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vacunas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_vacuna,Nombre_vacuna,Abreviatura_vacuna,Dosis_aplicables,Edad_aplicable")] Vacuna vacuna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacuna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vacuna);
        }

        // GET: Vacunas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacuna = await _context.Vacunas.FindAsync(id);
            if (vacuna == null)
            {
                return NotFound();
            }
            return View(vacuna);
        }

        // POST: Vacunas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_vacuna,Nombre_vacuna,Abreviatura_vacuna,Dosis_aplicables,Edad_aplicable")] Vacuna vacuna)
        {
            if (id != vacuna.Id_vacuna)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacuna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacunaExists(vacuna.Id_vacuna))
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
            return View(vacuna);
        }

        // GET: Vacunas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacuna = await _context.Vacunas
                .FirstOrDefaultAsync(m => m.Id_vacuna == id);
            if (vacuna == null)
            {
                return NotFound();
            }

            return View(vacuna);
        }

        // POST: Vacunas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacuna = await _context.Vacunas.FindAsync(id);
            _context.Vacunas.Remove(vacuna);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacunaExists(int id)
        {
            return _context.Vacunas.Any(e => e.Id_vacuna == id);
        }
    }
}
