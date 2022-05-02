﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoEsteSi.Common;
using ProyectoEsteSi.Data;
using ProyectoEsteSi.Models;
using ProyectoEsteSi.ViewModels;

namespace ProyectoEsteSi.Controllers
{
    public class CitasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int RecordsPerpage = 10;
        private Pagination<CitaViewModel> paginationPaquete;

        public CitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            var citas = await _context.Citas.Include(c => c.Dosis).ToListAsync();

            List<CitaViewModel> citasViewModel = new List<CitaViewModel>();

           
          
            foreach (var item in citas)
            {
                CitaViewModel citaViewModel = new CitaViewModel();
                citaViewModel.Id = item.IdCita;
                citaViewModel.Fecha = item.Fecha;
                citaViewModel.Fecha_proxima = item.Fecha_proxima;

                var dosis = await _context.Dosis.Where(d => d.IdDosis == item.IdDosis).Include(d => d.Pacientes).FirstOrDefaultAsync();

                citaViewModel.Numero_identidad = dosis.Pacientes.Numero_identidad;
                citaViewModel.restante = dosis.restante;

                var dosisvacuna = await _context.Dosis.Where(v => v.Id_vacuna == item.Dosis.Id_vacuna).Include(d => d.Vacunas).FirstOrDefaultAsync();

                citaViewModel.Nombre_vacuna = dosisvacuna.Vacunas.Nombre_vacuna;

                citasViewModel.Add(citaViewModel);
            }
           // var applicationDbContext = _context.Citas.Include(c => c.Dosis);

            return View(citasViewModel);
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Dosis)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewData["IdDosis"] = new SelectList(_context.Dosis, "IdDosis", "Id_nino");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,IdDosis,Fecha,Fecha_proxima, Id_nino")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["IdDosis"] = new SelectList(_context.Dosis, "IdDosis", "Id_nino", cita.IdDosis);
            ViewData["Id_nino"] = new SelectList(_context.Dosis, "Id_nino", "Numero_identidad", cita.IdDosis);
            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            ViewData["IdDosis"] = new SelectList(_context.Dosis, "IdDosis", "Id_nino", cita.IdDosis);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,IdDosis,Fecha,Fecha_proxima")] Cita cita)
        {
            if (id != cita.IdCita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.IdCita))
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
            ViewData["IdDosis"] = new SelectList(_context.Dosis, "IdDosis", "IdDosis", cita.IdDosis);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Dosis)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.IdCita == id);
        }
    }
}
