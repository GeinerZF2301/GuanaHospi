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
    public class EnfermedadSintomaController : Controller
    {
        private readonly GuanaHospiContext _context;

        public EnfermedadSintomaController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: EnfermedadSintoma
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.EnfermedadSintomas.Include(e => e.IdEnfermedadNavigation).Include(e => e.IdSintomaNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        // GET: EnfermedadSintoma/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedadSintoma = await _context.EnfermedadSintomas
                .Include(e => e.IdEnfermedadNavigation)
                .Include(e => e.IdSintomaNavigation)
                .FirstOrDefaultAsync(m => m.IdEnfermedadSintoma == id);
            if (enfermedadSintoma == null)
            {
                return NotFound();
            }

            return View(enfermedadSintoma);
        }

        // GET: EnfermedadSintoma/Create
        public IActionResult Create()
        {
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermedades, "IdEnfermedad", "Descripcion");
            ViewData["IdSintoma"] = new SelectList(_context.Sintomas, "IdSintoma", "Descripcion");
            return View();
        }

        // POST: EnfermedadSintoma/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEnfermedadSintoma,IdSintoma,IdEnfermedad")] EnfermedadSintoma enfermedadSintoma)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_InsertarEnfermedadSintoma";
                cmd.Parameters.Add("@Id_Sintoma", System.Data.SqlDbType.Int).Value = enfermedadSintoma.IdSintoma;
                cmd.Parameters.Add("@Id_Enfermedad", System.Data.SqlDbType.Int).Value = enfermedadSintoma.IdEnfermedad;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(enfermedadSintoma);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermedades, "IdEnfermedad", "Descripcion", enfermedadSintoma.IdEnfermedad);
            ViewData["IdSintoma"] = new SelectList(_context.Sintomas, "IdSintoma", "Descripcion", enfermedadSintoma.IdSintoma);
            return View(enfermedadSintoma);
        }

        // GET: EnfermedadSintoma/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedadSintoma = await _context.EnfermedadSintomas.FindAsync(id);
            if (enfermedadSintoma == null)
            {
                return NotFound();
            }
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermedades, "IdEnfermedad", "Descripcion", enfermedadSintoma.IdEnfermedad);
            ViewData["IdSintoma"] = new SelectList(_context.Sintomas, "IdSintoma", "Descripcion", enfermedadSintoma.IdSintoma);
            return View(enfermedadSintoma);
        }

        // POST: EnfermedadSintoma/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEnfermedadSintoma,IdSintoma,IdEnfermedad")] EnfermedadSintoma enfermedadSintoma)
        {
            if (id != enfermedadSintoma.IdEnfermedadSintoma)
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
                    cmd.CommandText = "SP_Actualizar_EnfermedadSintoma";
                    cmd.Parameters.Add("@Id_EnfermedadSintoma", System.Data.SqlDbType.Int).Value = enfermedadSintoma.IdEnfermedadSintoma;
                    cmd.Parameters.Add("@Id_Sintoma", System.Data.SqlDbType.Int).Value = enfermedadSintoma.IdSintoma;
                    cmd.Parameters.Add("@Id_Enfermedad", System.Data.SqlDbType.Int).Value = enfermedadSintoma.IdEnfermedad;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermedadSintomaExists(enfermedadSintoma.IdEnfermedadSintoma))
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
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermedades, "IdEnfermedad", "Descripcion", enfermedadSintoma.IdEnfermedad);
            ViewData["IdSintoma"] = new SelectList(_context.Sintomas, "IdSintoma", "Descripcion", enfermedadSintoma.IdSintoma);
            return View(enfermedadSintoma);
        }

        // GET: EnfermedadSintoma/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedadSintoma = await _context.EnfermedadSintomas
                .Include(e => e.IdEnfermedadNavigation)
                .Include(e => e.IdSintomaNavigation)
                .FirstOrDefaultAsync(m => m.IdEnfermedadSintoma == id);
            if (enfermedadSintoma == null)
            {
                return NotFound();
            }

            return View(enfermedadSintoma);
        }

        // POST: EnfermedadSintoma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_EliminarEnfermedadSintoma";
            cmd.Parameters.Add("@Id_EnfermedadSintoma", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermedadSintomaExists(int id)
        {
            return _context.EnfermedadSintomas.Any(e => e.IdEnfermedadSintoma == id);
        }
    }
}
