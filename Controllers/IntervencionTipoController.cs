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
    public class IntervencionTipoController : Controller
    {
        private readonly GuanaHospiContext _context;

        public IntervencionTipoController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: IntervencionTipo
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.IntervencionTipos.Include(i => i.IdIntervencionNavigation).Include(i => i.IdTipoIntervencionNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        // GET: IntervencionTipo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervencionTipo = await _context.IntervencionTipos
                .Include(i => i.IdIntervencionNavigation)
                .Include(i => i.IdTipoIntervencionNavigation)
                .FirstOrDefaultAsync(m => m.IdIntervencionTipo == id);
            if (intervencionTipo == null)
            {
                return NotFound();
            }

            return View(intervencionTipo);
        }

        // GET: IntervencionTipo/Create
        public IActionResult Create()
        {
            ViewData["IdIntervencion"] = new SelectList(_context.Intervencions, "IdIntervencion", "Descripcion");
            ViewData["IdTipoIntervencion"] = new SelectList(_context.TipoIntervenciones, "IdTipoIntervencion", "NombreTipo");
            return View();
        }

        // POST: IntervencionTipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIntervencionTipo,IdIntervencion,IdTipoIntervencion")] IntervencionTipo intervencionTipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intervencionTipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdIntervencion"] = new SelectList(_context.Intervencions, "IdIntervencion", "Descripcion", intervencionTipo.IdIntervencion);
            ViewData["IdTipoIntervencion"] = new SelectList(_context.TipoIntervenciones, "IdTipoIntervencion", "NombreTipo", intervencionTipo.IdTipoIntervencion);
            return View(intervencionTipo);
        }

        // GET: IntervencionTipo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervencionTipo = await _context.IntervencionTipos.FindAsync(id);
            if (intervencionTipo == null)
            {
                return NotFound();
            }
            ViewData["IdIntervencion"] = new SelectList(_context.Intervencions, "IdIntervencion", "Descripcion", intervencionTipo.IdIntervencion);
            ViewData["IdTipoIntervencion"] = new SelectList(_context.TipoIntervenciones, "IdTipoIntervencion", "NombreTipo", intervencionTipo.IdTipoIntervencion);
            return View(intervencionTipo);
        }

        // POST: IntervencionTipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdIntervencionTipo,IdIntervencion,IdTipoIntervencion")] IntervencionTipo intervencionTipo)
        {
            if (id != intervencionTipo.IdIntervencionTipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intervencionTipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntervencionTipoExists(intervencionTipo.IdIntervencionTipo))
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
            ViewData["IdIntervencion"] = new SelectList(_context.Intervencions, "IdIntervencion", "Descripcion", intervencionTipo.IdIntervencion);
            ViewData["IdTipoIntervencion"] = new SelectList(_context.TipoIntervenciones, "IdTipoIntervencion", "NombreTipo", intervencionTipo.IdTipoIntervencion);
            return View(intervencionTipo);
        }

        // GET: IntervencionTipo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervencionTipo = await _context.IntervencionTipos
                .Include(i => i.IdIntervencionNavigation)
                .Include(i => i.IdTipoIntervencionNavigation)
                .FirstOrDefaultAsync(m => m.IdIntervencionTipo == id);
            if (intervencionTipo == null)
            {
                return NotFound();
            }

            return View(intervencionTipo);
        }

        // POST: IntervencionTipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intervencionTipo = await _context.IntervencionTipos.FindAsync(id);
            _context.IntervencionTipos.Remove(intervencionTipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntervencionTipoExists(int id)
        {
            return _context.IntervencionTipos.Any(e => e.IdIntervencionTipo == id);
        }
    }
}
