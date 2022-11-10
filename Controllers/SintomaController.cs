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
    public class SintomaController : Controller
    {
        private readonly GuanaHospiContext _context;

        public SintomaController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: Sintoma
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sintomas.ToListAsync());
        }

        // GET: Sintoma/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sintoma = await _context.Sintomas
                .FirstOrDefaultAsync(m => m.IdSintoma == id);
            if (sintoma == null)
            {
                return NotFound();
            }

            return View(sintoma);
        }

        // GET: Sintoma/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sintoma/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSintoma,Nombre,Descripcion")] Sintoma sintoma)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_InsertarSintoma";
                cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 15).Value = sintoma.Nombre;
                cmd.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar, 15).Value = sintoma.Descripcion;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            return View(sintoma);
        }

        // GET: Sintoma/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sintoma = await _context.Sintomas.FindAsync(id);
            if (sintoma == null)
            {
                return NotFound();
            }
            return View(sintoma);
        }

        // POST: Sintoma/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSintoma,Nombre,Descripcion")] Sintoma sintoma)
        {
            if (id != sintoma.IdSintoma)
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
                    cmd.CommandText = "SP_Actualizar_Sintoma";
                    cmd.Parameters.Add("@Id_Sintoma", System.Data.SqlDbType.Int).Value = sintoma.IdSintoma;
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 15).Value = sintoma.Nombre;
                    cmd.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar, 15).Value = sintoma.Descripcion;
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SintomaExists(sintoma.IdSintoma))
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
            return View(sintoma);
        }

        // GET: Sintoma/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sintoma = await _context.Sintomas
                .FirstOrDefaultAsync(m => m.IdSintoma == id);
            if (sintoma == null)
            {
                return NotFound();
            }

            return View(sintoma);
        }

        // POST: Sintoma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_EliminarSintoma";
            cmd.Parameters.Add("@Id_Sintoma", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SintomaExists(int id)
        {
            return _context.Sintomas.Any(e => e.IdSintoma == id);
        }
    }
}
