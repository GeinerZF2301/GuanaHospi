using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuanaHospi.Data;
using GuanaHospi.Models;
using Microsoft.Data.SqlClient;

namespace GuanaHospi.Controllers
{
    public class TipoIntervencionController : Controller
    {
        private readonly GuanaHospiContext _context;

        public TipoIntervencionController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: TipoIntervencion
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoIntervenciones.ToListAsync());
        }

        // GET: TipoIntervencion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoIntervencion = await _context.TipoIntervenciones
                .FirstOrDefaultAsync(m => m.IdTipoIntervencion == id);
            if (tipoIntervencion == null)
            {
                return NotFound();
            }

            return View(tipoIntervencion);
        }

        // GET: TipoIntervencion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoIntervencion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoIntervencion,NombreTipo")] TipoIntervencion tipoIntervencion)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarGenero";
                cmd.Parameters.Add("@NombreTipo", System.Data.SqlDbType.VarChar, 30).Value = tipoIntervencion.NombreTipo + "Insercion_SP";
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoIntervencion);
        }

        // GET: TipoIntervencion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoIntervencion = await _context.TipoIntervenciones.FindAsync(id);
            if (tipoIntervencion == null)
            {
                return NotFound();
            }
            return View(tipoIntervencion);
        }

        // POST: TipoIntervencion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoIntervencion,NombreTipo")] TipoIntervencion tipoIntervencion)
        {
            if (id != tipoIntervencion.IdTipoIntervencion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoIntervencion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoIntervencionExists(tipoIntervencion.IdTipoIntervencion))
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
            return View(tipoIntervencion);
        }

        // GET: TipoIntervencion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoIntervencion = await _context.TipoIntervenciones
                .FirstOrDefaultAsync(m => m.IdTipoIntervencion == id);
            if (tipoIntervencion == null)
            {
                return NotFound();
            }

            return View(tipoIntervencion);
        }

        // POST: TipoIntervencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoIntervencion = await _context.TipoIntervenciones.FindAsync(id);
            _context.TipoIntervenciones.Remove(tipoIntervencion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoIntervencionExists(int id)
        {
            return _context.TipoIntervenciones.Any(e => e.IdTipoIntervencion == id);
        }
    }
}
