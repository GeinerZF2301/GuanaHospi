using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuanaHospi.Data;
using GuanaHospi.Models;

namespace GuanaHospi.Controllers
{
    public class DoctorEspecialidadController : Controller
    {
        private readonly GuanaHospiContext _context;

        public DoctorEspecialidadController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: DoctorEspecialidad
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.DoctorEspecialidades.Include(d => d.IdDoctorNavigation).Include(d => d.IdEspecialidadNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        // GET: DoctorEspecialidad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorEspecialidad = await _context.DoctorEspecialidades
                .Include(d => d.IdDoctorNavigation)
                .Include(d => d.IdEspecialidadNavigation)
                .FirstOrDefaultAsync(m => m.IdDoctorEspecialidad == id);
            if (doctorEspecialidad == null)
            {
                return NotFound();
            }

            return View(doctorEspecialidad);
        }

        // GET: DoctorEspecialidad/Create
        public IActionResult Create()
        {
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula");
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "IdEspecialidad", "Nombre");
            return View();
        }

        // POST: DoctorEspecialidad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDoctorEspecialidad,IdEspecialidad,IdDoctor")] DoctorEspecialidad doctorEspecialidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorEspecialidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula", doctorEspecialidad.IdDoctor);
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "IdEspecialidad", "Nombre", doctorEspecialidad.IdEspecialidad);
            return View(doctorEspecialidad);
        }

        // GET: DoctorEspecialidad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorEspecialidad = await _context.DoctorEspecialidades.FindAsync(id);
            if (doctorEspecialidad == null)
            {
                return NotFound();
            }
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula", doctorEspecialidad.IdDoctor);
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "IdEspecialidad", "Nombre", doctorEspecialidad.IdEspecialidad);
            return View(doctorEspecialidad);
        }

        // POST: DoctorEspecialidad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDoctorEspecialidad,IdEspecialidad,IdDoctor")] DoctorEspecialidad doctorEspecialidad)
        {
            if (id != doctorEspecialidad.IdDoctorEspecialidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorEspecialidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorEspecialidadExists(doctorEspecialidad.IdDoctorEspecialidad))
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
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Apellido1", doctorEspecialidad.IdDoctor);
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "IdEspecialidad", "Descripcion", doctorEspecialidad.IdEspecialidad);
            return View(doctorEspecialidad);
        }

        // GET: DoctorEspecialidad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorEspecialidad = await _context.DoctorEspecialidades
                .Include(d => d.IdDoctorNavigation)
                .Include(d => d.IdEspecialidadNavigation)
                .FirstOrDefaultAsync(m => m.IdDoctorEspecialidad == id);
            if (doctorEspecialidad == null)
            {
                return NotFound();
            }

            return View(doctorEspecialidad);
        }

        // POST: DoctorEspecialidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctorEspecialidad = await _context.DoctorEspecialidades.FindAsync(id);
            _context.DoctorEspecialidades.Remove(doctorEspecialidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorEspecialidadExists(int id)
        {
            return _context.DoctorEspecialidades.Any(e => e.IdDoctorEspecialidad == id);
        }
    }
}
