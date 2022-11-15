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
    public class EnfermedadPacienteController : Controller
    {
        private readonly GuanaHospiContext _context;

        public EnfermedadPacienteController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: EnfermedadPaciente
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.EnfermedadPacientes.Include(e => e.IdEnfermedadNavigation).Include(e => e.IdPacienteNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        // GET: EnfermedadPaciente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedadPaciente = await _context.EnfermedadPacientes
                .Include(e => e.IdEnfermedadNavigation)
                .Include(e => e.IdPacienteNavigation)
                .FirstOrDefaultAsync(m => m.IdEnfermedadPaciente == id);
            if (enfermedadPaciente == null)
            {
                return NotFound();
            }

            return View(enfermedadPaciente);
        }

        // GET: EnfermedadPaciente/Create
        public IActionResult Create()
        {
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermedades, "IdEnfermedad", "Nombre");
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Cedula");
            return View();
        }

        // POST: EnfermedadPaciente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEnfermedadPaciente,IdEnfermedad,IdPaciente")] EnfermedadPaciente enfermedadPaciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enfermedadPaciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermedades, "IdEnfermedad", "Nombre", enfermedadPaciente.IdEnfermedad);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Cedula", enfermedadPaciente.IdPaciente);
            return View(enfermedadPaciente);
        }

        // GET: EnfermedadPaciente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedadPaciente = await _context.EnfermedadPacientes.FindAsync(id);
            if (enfermedadPaciente == null)
            {
                return NotFound();
            }
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermedades, "IdEnfermedad", "Nombre", enfermedadPaciente.IdEnfermedad);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Cedula", enfermedadPaciente.IdPaciente);
            return View(enfermedadPaciente);
        }

        // POST: EnfermedadPaciente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEnfermedadPaciente,IdEnfermedad,IdPaciente")] EnfermedadPaciente enfermedadPaciente)
        {
            if (id != enfermedadPaciente.IdEnfermedadPaciente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enfermedadPaciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermedadPacienteExists(enfermedadPaciente.IdEnfermedadPaciente))
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
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermedades, "IdEnfermedad", "Nombre", enfermedadPaciente.IdEnfermedad);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Cedula", enfermedadPaciente.IdPaciente);
            return View(enfermedadPaciente);
        }

        // GET: EnfermedadPaciente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedadPaciente = await _context.EnfermedadPacientes
                .Include(e => e.IdEnfermedadNavigation)
                .Include(e => e.IdPacienteNavigation)
                .FirstOrDefaultAsync(m => m.IdEnfermedadPaciente == id);
            if (enfermedadPaciente == null)
            {
                return NotFound();
            }

            return View(enfermedadPaciente);
        }

        // POST: EnfermedadPaciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enfermedadPaciente = await _context.EnfermedadPacientes.FindAsync(id);
            _context.EnfermedadPacientes.Remove(enfermedadPaciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermedadPacienteExists(int id)
        {
            return _context.EnfermedadPacientes.Any(e => e.IdEnfermedadPaciente == id);
        }
    }
}
