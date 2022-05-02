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
    public class Informacion_pacienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly int RecordsPerpage = 10;
        private Pagination<Informacion_paciente> paginationPaquete;

        public Informacion_pacienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Informacion_paciente
        public async Task<IActionResult> Index(string search, int page = 1)
        {


            int totalRecords = 0;


            if (search == null)
            {
                search = "";
            }

            totalRecords = await _context.Pacientes.CountAsync(
                d => d.Numero_identidad.Contains(search));



            var paquetes = await _context.Pacientes.Where
                (d => d.Numero_identidad.Contains(search)).Include(i => i.Direcciones).ToListAsync();

            var paqueteresult = paquetes.OrderBy(o => o.Numero_identidad)
                .Skip((page - 1) * RecordsPerpage)
                .Take(RecordsPerpage);

            var totalPages = (int)Math.Ceiling((double)totalRecords / RecordsPerpage);

            paginationPaquete = new Pagination<Informacion_paciente>()
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

        // GET: Informacion_paciente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacion_paciente = await _context.Pacientes
                .Include(i => i.Direcciones)
                .FirstOrDefaultAsync(m => m.Id_nino == id);
            if (informacion_paciente == null)
            {
                return NotFound();
            }

            return View(informacion_paciente);
        }

        // GET: Informacion_paciente/Create
        public IActionResult Create()
        {
            ViewData["Id_direccion"] = new SelectList(_context.Direcciones, "Id_direccion", "Detalle_direccion");
            return View();
        }

        // POST: Informacion_paciente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_nino,Numero_identidad,Nombre_nino,Nombre_responsabe,Fecha_nacimineto,Edad_cap,Contacto,Id_direccion")] Informacion_paciente informacion_paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informacion_paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_direccion"] = new SelectList(_context.Direcciones, "Id_direccion", "Detalle_direccion", informacion_paciente.Id_direccion);
            return View(informacion_paciente);
        }

        // GET: Informacion_paciente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacion_paciente = await _context.Pacientes.FindAsync(id);
            if (informacion_paciente == null)
            {
                return NotFound();
            }
            ViewData["Id_direccion"] = new SelectList(_context.Direcciones, "Id_direccion", "Detalle_direccion", informacion_paciente.Id_direccion);
            return View(informacion_paciente);
        }

        // POST: Informacion_paciente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_nino,Numero_identidad,Nombre_nino,Nombre_responsabe,Fecha_nacimineto,Edad_cap,Contacto,Id_direccion")] Informacion_paciente informacion_paciente)
        {
            if (id != informacion_paciente.Id_nino)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informacion_paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Informacion_pacienteExists(informacion_paciente.Id_nino))
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
            ViewData["Id_direccion"] = new SelectList(_context.Direcciones, "Id_direccion", "Detalle_direccion", informacion_paciente.Id_direccion);
            return View(informacion_paciente);
        }

        // GET: Informacion_paciente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacion_paciente = await _context.Pacientes
                .Include(i => i.Direcciones)
                .FirstOrDefaultAsync(m => m.Id_nino == id);
            if (informacion_paciente == null)
            {
                return NotFound();
            }

            return View(informacion_paciente);
        }

        // POST: Informacion_paciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var informacion_paciente = await _context.Pacientes.FindAsync(id);
            _context.Pacientes.Remove(informacion_paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Informacion_pacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id_nino == id);
        }
    }
}
