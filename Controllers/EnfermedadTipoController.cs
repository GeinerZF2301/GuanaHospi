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
    public class EnfermedadTipoController : Controller
    {
        private readonly GuanaHospiContext _context;

        public EnfermedadTipoController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: EnfermedadTipo
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.EnfermedadTipos.Include(e => e.IdEnfermedadNavigation).Include(e => e.IdTipoEnfermedadNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        // GET: EnfermedadTipo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedadTipo = await _context.EnfermedadTipos
                .Include(e => e.IdEnfermedadNavigation)
                .Include(e => e.IdTipoEnfermedadNavigation)
                .FirstOrDefaultAsync(m => m.IdEnfermedadTipo == id);
            if (enfermedadTipo == null)
            {
                return NotFound();
            }

            return View(enfermedadTipo);
        }

        // GET: EnfermedadTipo/Create
        public IActionResult Create()
        {
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermedades, "IdEnfermedad", "Descripcion");
            ViewData["IdTipoEnfermedad"] = new SelectList(_context.TipoEnfermedades, "IdTipoEnfermedad", "Nombre");
            return View();
        }

        // POST: EnfermedadTipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEnfermedadTipo,IdTipoEnfermedad,IdEnfermedad")] EnfermedadTipo enfermedadTipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enfermedadTipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermedades, "IdEnfermedad", "Descripcion", enfermedadTipo.IdEnfermedad);
            ViewData["IdTipoEnfermedad"] = new SelectList(_context.TipoEnfermedades, "IdTipoEnfermedad", "Nombre", enfermedadTipo.IdTipoEnfermedad);
            return View(enfermedadTipo);
        }

        // GET: EnfermedadTipo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedadTipo = await _context.EnfermedadTipos.FindAsync(id);
            if (enfermedadTipo == null)
            {
                return NotFound();
            }
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermedades, "IdEnfermedad", "Descripcion", enfermedadTipo.IdEnfermedad);
            ViewData["IdTipoEnfermedad"] = new SelectList(_context.TipoEnfermedades, "IdTipoEnfermedad", "Nombre", enfermedadTipo.IdTipoEnfermedad);
            return View(enfermedadTipo);
        }

        // POST: EnfermedadTipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEnfermedadTipo,IdTipoEnfermedad,IdEnfermedad")] EnfermedadTipo enfermedadTipo)
        {
            if (id != enfermedadTipo.IdEnfermedadTipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enfermedadTipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermedadTipoExists(enfermedadTipo.IdEnfermedadTipo))
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
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermedades, "IdEnfermedad", "Descripcion", enfermedadTipo.IdEnfermedad);
            ViewData["IdTipoEnfermedad"] = new SelectList(_context.TipoEnfermedades, "IdTipoEnfermedad", "Nombre", enfermedadTipo.IdTipoEnfermedad);
            return View(enfermedadTipo);
        }

        // GET: EnfermedadTipo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedadTipo = await _context.EnfermedadTipos
                .Include(e => e.IdEnfermedadNavigation)
                .Include(e => e.IdTipoEnfermedadNavigation)
                .FirstOrDefaultAsync(m => m.IdEnfermedadTipo == id);
            if (enfermedadTipo == null)
            {
                return NotFound();
            }

            return View(enfermedadTipo);
        }

        // POST: EnfermedadTipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enfermedadTipo = await _context.EnfermedadTipos.FindAsync(id);
            _context.EnfermedadTipos.Remove(enfermedadTipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermedadTipoExists(int id)
        {
            return _context.EnfermedadTipos.Any(e => e.IdEnfermedadTipo == id);
        }
    }
}
