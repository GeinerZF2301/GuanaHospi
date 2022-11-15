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
    public class TipoEnfermedadController : Controller
    {
        private readonly GuanaHospiContext _context;

        public TipoEnfermedadController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: TipoEnfermedad
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoEnfermedades.ToListAsync());
        }

        // GET: TipoEnfermedad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEnfermedad = await _context.TipoEnfermedades
                .FirstOrDefaultAsync(m => m.IdTipoEnfermedad == id);
            if (tipoEnfermedad == null)
            {
                return NotFound();
            }

            return View(tipoEnfermedad);
        }

        // GET: TipoEnfermedad/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoEnfermedad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoEnfermedad,Nombre")] TipoEnfermedad tipoEnfermedad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoEnfermedad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEnfermedad);
        }

        // GET: TipoEnfermedad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEnfermedad = await _context.TipoEnfermedades.FindAsync(id);
            if (tipoEnfermedad == null)
            {
                return NotFound();
            }
            return View(tipoEnfermedad);
        }

        // POST: TipoEnfermedad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoEnfermedad,Nombre")] TipoEnfermedad tipoEnfermedad)
        {
            if (id != tipoEnfermedad.IdTipoEnfermedad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoEnfermedad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoEnfermedadExists(tipoEnfermedad.IdTipoEnfermedad))
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
            return View(tipoEnfermedad);
        }

        // GET: TipoEnfermedad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEnfermedad = await _context.TipoEnfermedades
                .FirstOrDefaultAsync(m => m.IdTipoEnfermedad == id);
            if (tipoEnfermedad == null)
            {
                return NotFound();
            }

            return View(tipoEnfermedad);
        }

        // POST: TipoEnfermedad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoEnfermedad = await _context.TipoEnfermedades.FindAsync(id);
            _context.TipoEnfermedades.Remove(tipoEnfermedad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoEnfermedadExists(int id)
        {
            return _context.TipoEnfermedades.Any(e => e.IdTipoEnfermedad == id);
        }
    }
}
