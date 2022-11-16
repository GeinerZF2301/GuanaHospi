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
    public class TratamientoTipoController : Controller
    {
        private readonly GuanaHospiContext _context;

        public TratamientoTipoController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: TratamientoTipo
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.TratamientoTipos.Include(t => t.IdTipoTratamientoNavigation).Include(t => t.IdTratamientoNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        // GET: TratamientoTipo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientoTipo = await _context.TratamientoTipos
                .Include(t => t.IdTipoTratamientoNavigation)
                .Include(t => t.IdTratamientoNavigation)
                .FirstOrDefaultAsync(m => m.IdTratamientoTipo == id);
            if (tratamientoTipo == null)
            {
                return NotFound();
            }

            return View(tratamientoTipo);
        }

        // GET: TratamientoTipo/Create
        public IActionResult Create()
        {
            ViewData["IdTipoTratamiento"] = new SelectList(_context.TipoTratamientos, "IdTipoTratamiento", "NombreTipo");
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre");
            return View();
        }

        // POST: TratamientoTipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTratamientoTipo,IdTipoTratamiento,IdTratamiento")] TratamientoTipo tratamientoTipo)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_InsertarTratamientoTipo";
                cmd.Parameters.Add("@Id_TipoTratamiento", System.Data.SqlDbType.Int).Value = tratamientoTipo.IdTipoTratamiento;
                cmd.Parameters.Add("@Id_Tratamiento", System.Data.SqlDbType.Int).Value = tratamientoTipo.IdTratamiento;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoTratamiento"] = new SelectList(_context.TipoTratamientos, "IdTipoTratamiento", "NombreTipo", tratamientoTipo.IdTipoTratamiento);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre", tratamientoTipo.IdTratamiento);
            return View(tratamientoTipo);
        }

        // GET: TratamientoTipo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientoTipo = await _context.TratamientoTipos.FindAsync(id);
            if (tratamientoTipo == null)
            {
                return NotFound();
            }
            ViewData["IdTipoTratamiento"] = new SelectList(_context.TipoTratamientos, "IdTipoTratamiento", "NombreTipo", tratamientoTipo.IdTipoTratamiento);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre", tratamientoTipo.IdTratamiento);
            return View(tratamientoTipo);
        }

        // POST: TratamientoTipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTratamientoTipo,IdTipoTratamiento,IdTratamiento")] TratamientoTipo tratamientoTipo)
        {
            if (id != tratamientoTipo.IdTratamientoTipo)
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
                    cmd.CommandText = "SP_Actualizar_TratamientoTipo";
                    cmd.Parameters.Add("@Id_TratamientoTipo", System.Data.SqlDbType.Int).Value = tratamientoTipo.IdTratamientoTipo;
                    cmd.Parameters.Add("@Id_TipoTratamiento", System.Data.SqlDbType.Int).Value = tratamientoTipo.IdTipoTratamiento;
                    cmd.Parameters.Add("@Id_Tratamiento", System.Data.SqlDbType.Int).Value = tratamientoTipo.IdTratamiento;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TratamientoTipoExists(tratamientoTipo.IdTratamientoTipo))
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
            ViewData["IdTipoTratamiento"] = new SelectList(_context.TipoTratamientos, "IdTipoTratamiento", "NombreTipo", tratamientoTipo.IdTipoTratamiento);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre", tratamientoTipo.IdTratamiento);
            return View(tratamientoTipo);
        }

        // GET: TratamientoTipo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientoTipo = await _context.TratamientoTipos
                .Include(t => t.IdTipoTratamientoNavigation)
                .Include(t => t.IdTratamientoNavigation)
                .FirstOrDefaultAsync(m => m.IdTratamientoTipo == id);
            if (tratamientoTipo == null)
            {
                return NotFound();
            }

            return View(tratamientoTipo);
        }

        // POST: TratamientoTipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_Actualizar_TratamientoTipo";
            cmd.Parameters.Add("@Id_TratamientoTipo", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            return RedirectToAction(nameof(Index));
        }

        private bool TratamientoTipoExists(int id)
        {
            return _context.TratamientoTipos.Any(e => e.IdTratamientoTipo == id);
        }
    }
}
