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
    public class TipoTratamientoController : Controller
    {
        private readonly GuanaHospiContext _context;

        public TipoTratamientoController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: TipoTratamiento
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoTratamientos.ToListAsync());
        }

        // GET: TipoTratamiento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTratamiento = await _context.TipoTratamientos
                .FirstOrDefaultAsync(m => m.IdTipoTratamiento == id);
            if (tipoTratamiento == null)
            {
                return NotFound();
            }

            return View(tipoTratamiento);
        }

        // GET: TipoTratamiento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoTratamiento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoTratamiento,NombreTipo")] TipoTratamiento tipoTratamiento)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_InsertarTipoTratamiento";
                cmd.Parameters.Add("@NombreTipo", System.Data.SqlDbType.VarChar, 15).Value = tipoTratamiento.NombreTipo;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();

                return RedirectToAction(nameof(Index));
            }
            return View(tipoTratamiento);
        }

        // GET: TipoTratamiento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTratamiento = await _context.TipoTratamientos.FindAsync(id);
            if (tipoTratamiento == null)
            {
                return NotFound();
            }
            return View(tipoTratamiento);
        }

        // POST: TipoTratamiento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoTratamiento,NombreTipo")] TipoTratamiento tipoTratamiento)
        {
            if (id != tipoTratamiento.IdTipoTratamiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoTratamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoTratamientoExists(tipoTratamiento.IdTipoTratamiento))
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
            return View(tipoTratamiento);
        }

        // GET: TipoTratamiento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTratamiento = await _context.TipoTratamientos
                .FirstOrDefaultAsync(m => m.IdTipoTratamiento == id);
            if (tipoTratamiento == null)
            {
                return NotFound();
            }

            return View(tipoTratamiento);
        }

        // POST: TipoTratamiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoTratamiento = await _context.TipoTratamientos.FindAsync(id);
            _context.TipoTratamientos.Remove(tipoTratamiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoTratamientoExists(int id)
        {
            return _context.TipoTratamientos.Any(e => e.IdTipoTratamiento == id);
        }
    }
}
