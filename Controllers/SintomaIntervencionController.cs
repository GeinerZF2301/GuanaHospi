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
    public class SintomaIntervencionController : Controller
    {
        private readonly GuanaHospiContext _context;

        public SintomaIntervencionController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: SintomaIntervencion
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.SintomaIntervencions.Include(s => s.IdIntervencionNavigation).Include(s => s.IdSintomaNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        // GET: SintomaIntervencion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sintomaIntervencion = await _context.SintomaIntervencions
                .Include(s => s.IdIntervencionNavigation)
                .Include(s => s.IdSintomaNavigation)
                .FirstOrDefaultAsync(m => m.IdSintomaIntervencion == id);
            if (sintomaIntervencion == null)
            {
                return NotFound();
            }

            return View(sintomaIntervencion);
        }

        // GET: SintomaIntervencion/Create
        public IActionResult Create()
        {
            ViewData["IdIntervencion"] = new SelectList(_context.Intervenciones, "IdIntervencion", "Descripcion");
            ViewData["IdSintoma"] = new SelectList(_context.Sintomas, "IdSintoma", "Descripcion");
            return View();
        }

        // POST: SintomaIntervencion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSintomaIntervencion,IdIntervencion,IdSintoma")] SintomaIntervencion sintomaIntervencion)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_InsertarSintomaIntervencion";
                cmd.Parameters.Add("@Id_Intervencion", System.Data.SqlDbType.Int).Value = sintomaIntervencion.IdIntervencion;
                cmd.Parameters.Add("@Id_Sintoma", System.Data.SqlDbType.Int).Value = sintomaIntervencion.IdSintoma;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdIntervencion"] = new SelectList(_context.Intervenciones, "IdIntervencion", "Descripcion", sintomaIntervencion.IdIntervencion);
            ViewData["IdSintoma"] = new SelectList(_context.Sintomas, "IdSintoma", "Descripcion", sintomaIntervencion.IdSintoma);
            return View(sintomaIntervencion);
        }

        // GET: SintomaIntervencion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sintomaIntervencion = await _context.SintomaIntervencions.FindAsync(id);
            if (sintomaIntervencion == null)
            {
                return NotFound();
            }
            ViewData["IdIntervencion"] = new SelectList(_context.Intervenciones, "IdIntervencion", "Descripcion", sintomaIntervencion.IdIntervencion);
            ViewData["IdSintoma"] = new SelectList(_context.Sintomas, "IdSintoma", "Descripcion", sintomaIntervencion.IdSintoma);
            return View(sintomaIntervencion);
        }

        // POST: SintomaIntervencion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSintomaIntervencion,IdIntervencion,IdSintoma")] SintomaIntervencion sintomaIntervencion)
        {
            if (id != sintomaIntervencion.IdSintomaIntervencion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "SP_Actualizar_SintomaIntervencion";
                    cmd.Parameters.Add("@Id_SintomaIntervencion", System.Data.SqlDbType.Int).Value = sintomaIntervencion.IdSintomaIntervencion;
                    cmd.Parameters.Add("@Id_Intervencion", System.Data.SqlDbType.Int).Value = sintomaIntervencion.IdIntervencion;
                    cmd.Parameters.Add("@Id_Sintoma", System.Data.SqlDbType.Int).Value = sintomaIntervencion.IdSintoma;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SintomaIntervencionExists(sintomaIntervencion.IdSintomaIntervencion))
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
            ViewData["IdIntervencion"] = new SelectList(_context.Intervenciones, "IdIntervencion", "Descripcion", sintomaIntervencion.IdIntervencion);
            ViewData["IdSintoma"] = new SelectList(_context.Sintomas, "IdSintoma", "Descripcion", sintomaIntervencion.IdSintoma);
            return View(sintomaIntervencion);
        }

        // GET: SintomaIntervencion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sintomaIntervencion = await _context.SintomaIntervencions
                .Include(s => s.IdIntervencionNavigation)
                .Include(s => s.IdSintomaNavigation)
                .FirstOrDefaultAsync(m => m.IdSintomaIntervencion == id);
            if (sintomaIntervencion == null)
            {
                return NotFound();
            }

            return View(sintomaIntervencion);
        }

        // POST: SintomaIntervencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_EliminarSintomaIntervencion";
            cmd.Parameters.Add("@Id_SintomaIntervencion", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            return RedirectToAction(nameof(Index));
        }

        private bool SintomaIntervencionExists(int id)
        {
            return _context.SintomaIntervencions.Any(e => e.IdSintomaIntervencion == id);
        }
    }
}
