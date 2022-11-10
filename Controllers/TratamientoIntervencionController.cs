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
    public class TratamientoIntervencionController : Controller
    {
        private readonly GuanaHospiContext _context;

        public TratamientoIntervencionController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: TratamientoIntervencion
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.TratamientoIntervenciones.Include(t => t.IdIntervencionNavigation).Include(t => t.IdTratamientoNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        // GET: TratamientoIntervencion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientoIntervencion = await _context.TratamientoIntervenciones
                .Include(t => t.IdIntervencionNavigation)
                .Include(t => t.IdTratamientoNavigation)
                .FirstOrDefaultAsync(m => m.IdTratamientoIntervencion == id);
            if (tratamientoIntervencion == null)
            {
                return NotFound();
            }

            return View(tratamientoIntervencion);
        }

        // GET: TratamientoIntervencion/Create
        public IActionResult Create()
        {
            ViewData["IdIntervencion"] = new SelectList(_context.Intervencions, "IdIntervencion", "Descripcion");
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Descripcion");
            return View();
        }

        // POST: TratamientoIntervencion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTratamientoIntervencion,IdIntervencion,IdTratamiento")] TratamientoIntervencion tratamientoIntervencion)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_InsertarTratamientoIntervencion";
                cmd.Parameters.Add("@Id_Intervencion", System.Data.SqlDbType.Int).Value = tratamientoIntervencion.IdIntervencion;
                cmd.Parameters.Add("@Id_Tratamiento", System.Data.SqlDbType.Int).Value = tratamientoIntervencion.IdTratamiento;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdIntervencion"] = new SelectList(_context.Intervencions, "IdIntervencion", "Descripcion", tratamientoIntervencion.IdIntervencion);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Descripcion", tratamientoIntervencion.IdTratamiento);
            return View(tratamientoIntervencion);
        }

        // GET: TratamientoIntervencion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientoIntervencion = await _context.TratamientoIntervenciones.FindAsync(id);
            if (tratamientoIntervencion == null)
            {
                return NotFound();
            }
            ViewData["IdIntervencion"] = new SelectList(_context.Intervencions, "IdIntervencion", "Descripcion", tratamientoIntervencion.IdIntervencion);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Descripcion", tratamientoIntervencion.IdTratamiento);
            return View(tratamientoIntervencion);
        }

        // POST: TratamientoIntervencion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTratamientoIntervencion,IdIntervencion,IdTratamiento")] TratamientoIntervencion tratamientoIntervencion)
        {
            if (id != tratamientoIntervencion.IdTratamientoIntervencion)
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
                    cmd.CommandText = "SP_Actualizar_TratamientoIntervencion";
                    cmd.Parameters.Add("@Id_TratamientoIntervencion", System.Data.SqlDbType.Int).Value = tratamientoIntervencion.IdTratamientoIntervencion;
                    cmd.Parameters.Add("@Id_Intervencion", System.Data.SqlDbType.Int).Value = tratamientoIntervencion.IdIntervencion;
                    cmd.Parameters.Add("@Id_Tratamiento", System.Data.SqlDbType.Int).Value = tratamientoIntervencion.IdTratamiento;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TratamientoIntervencionExists(tratamientoIntervencion.IdTratamientoIntervencion))
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
            ViewData["IdIntervencion"] = new SelectList(_context.Intervencions, "IdIntervencion", "Descripcion", tratamientoIntervencion.IdIntervencion);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Descripcion", tratamientoIntervencion.IdTratamiento);
            return View(tratamientoIntervencion);
        }

        // GET: TratamientoIntervencion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientoIntervencion = await _context.TratamientoIntervenciones
                .Include(t => t.IdIntervencionNavigation)
                .Include(t => t.IdTratamientoNavigation)
                .FirstOrDefaultAsync(m => m.IdTratamientoIntervencion == id);
            if (tratamientoIntervencion == null)
            {
                return NotFound();
            }

            return View(tratamientoIntervencion);
        }

        // POST: TratamientoIntervencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_EliminarTratamientoIntervencion";
            cmd.Parameters.Add("@Id_TratamientoIntervencion", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            return RedirectToAction(nameof(Index));
        }

        private bool TratamientoIntervencionExists(int id)
        {
            return _context.TratamientoIntervenciones.Any(e => e.IdTratamientoIntervencion == id);
        }
    }
}
