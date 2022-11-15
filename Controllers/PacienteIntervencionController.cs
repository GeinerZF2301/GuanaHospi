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
    public class PacienteIntervencionController : Controller
    {
        private readonly GuanaHospiContext _context;

        public PacienteIntervencionController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: PacienteIntervencion
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.PacienteIntervenciones.Include(p => p.IdIntervencionNavigation).Include(p => p.IdPacienteNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        // GET: PacienteIntervencion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteIntervencion = await _context.PacienteIntervenciones
                .Include(p => p.IdIntervencionNavigation)
                .Include(p => p.IdPacienteNavigation)
                .FirstOrDefaultAsync(m => m.IdPacienteIntervencion == id);
            if (pacienteIntervencion == null)
            {
                return NotFound();
            }

            return View(pacienteIntervencion);
        }

        // GET: PacienteIntervencion/Create
        public IActionResult Create()
        {
            ViewData["IdIntervencion"] = new SelectList(_context.Intervenciones, "IdIntervencion", "Descripcion");
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido1");
            return View();
        }

        // POST: PacienteIntervencion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPacienteIntervencion,IdIntervencion,IdPaciente")] PacienteIntervencion pacienteIntervencion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacienteIntervencion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdIntervencion"] = new SelectList(_context.Intervenciones, "IdIntervencion", "Descripcion", pacienteIntervencion.IdIntervencion);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido1", pacienteIntervencion.IdPaciente);
            return View(pacienteIntervencion);
        }

        // GET: PacienteIntervencion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteIntervencion = await _context.PacienteIntervenciones.FindAsync(id);
            if (pacienteIntervencion == null)
            {
                return NotFound();
            }
            ViewData["IdIntervencion"] = new SelectList(_context.Intervenciones, "IdIntervencion", "Descripcion", pacienteIntervencion.IdIntervencion);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido1", pacienteIntervencion.IdPaciente);
            return View(pacienteIntervencion);
        }

        // POST: PacienteIntervencion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPacienteIntervencion,IdIntervencion,IdPaciente")] PacienteIntervencion pacienteIntervencion)
        {
            if (id != pacienteIntervencion.IdPacienteIntervencion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacienteIntervencion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteIntervencionExists(pacienteIntervencion.IdPacienteIntervencion))
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
            ViewData["IdIntervencion"] = new SelectList(_context.Intervenciones, "IdIntervencion", "Descripcion", pacienteIntervencion.IdIntervencion);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido1", pacienteIntervencion.IdPaciente);
            return View(pacienteIntervencion);
        }

        // GET: PacienteIntervencion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteIntervencion = await _context.PacienteIntervenciones
                .Include(p => p.IdIntervencionNavigation)
                .Include(p => p.IdPacienteNavigation)
                .FirstOrDefaultAsync(m => m.IdPacienteIntervencion == id);
            if (pacienteIntervencion == null)
            {
                return NotFound();
            }

            return View(pacienteIntervencion);
        }

        // POST: PacienteIntervencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacienteIntervencion = await _context.PacienteIntervenciones.FindAsync(id);
            _context.PacienteIntervenciones.Remove(pacienteIntervencion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteIntervencionExists(int id)
        {
            return _context.PacienteIntervenciones.Any(e => e.IdPacienteIntervencion == id);
        }
    }
}
