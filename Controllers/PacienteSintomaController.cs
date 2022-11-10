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
    public class PacienteSintomaController : Controller
    {
        private readonly GuanaHospiContext _context;

        public PacienteSintomaController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: PacienteSintoma
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.PacienteSintomas.Include(p => p.IdPacienteNavigation).Include(p => p.IdSintomaNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        // GET: PacienteSintoma/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteSintoma = await _context.PacienteSintomas
                .Include(p => p.IdPacienteNavigation)
                .Include(p => p.IdSintomaNavigation)
                .FirstOrDefaultAsync(m => m.IdPacienteSintoma == id);
            if (pacienteSintoma == null)
            {
                return NotFound();
            }

            return View(pacienteSintoma);
        }

        // GET: PacienteSintoma/Create
        public IActionResult Create()
        {
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido1");
            ViewData["IdSintoma"] = new SelectList(_context.Sintomas, "IdSintoma", "Descripcion");
            return View();
        }

        // POST: PacienteSintoma/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPacienteSintoma,IdPaciente,IdSintoma")] PacienteSintoma pacienteSintoma)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_InsertarPacienteSintoma";
                cmd.Parameters.Add("@Id_Paciente", System.Data.SqlDbType.Int).Value = pacienteSintoma.IdPaciente;
                cmd.Parameters.Add("@Id_Sintoma", System.Data.SqlDbType.Int).Value = pacienteSintoma.IdSintoma;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido1", pacienteSintoma.IdPaciente);
            ViewData["IdSintoma"] = new SelectList(_context.Sintomas, "IdSintoma", "Descripcion", pacienteSintoma.IdSintoma);
            return View(pacienteSintoma);
        }

        // GET: PacienteSintoma/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteSintoma = await _context.PacienteSintomas.FindAsync(id);
            if (pacienteSintoma == null)
            {
                return NotFound();
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido1", pacienteSintoma.IdPaciente);
            ViewData["IdSintoma"] = new SelectList(_context.Sintomas, "IdSintoma", "Descripcion", pacienteSintoma.IdSintoma);
            return View(pacienteSintoma);
        }

        // POST: PacienteSintoma/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPacienteSintoma,IdPaciente,IdSintoma")] PacienteSintoma pacienteSintoma)
        {
            if (id != pacienteSintoma.IdPacienteSintoma)
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
                    cmd.CommandText = "SP_Actualizar_PacienteSintoma";
                    cmd.Parameters.Add("@Id_PacienteSintoma", System.Data.SqlDbType.Int).Value = pacienteSintoma.IdPacienteSintoma;
                    cmd.Parameters.Add("@Id_Paciente", System.Data.SqlDbType.Int).Value = pacienteSintoma.IdPaciente;
                    cmd.Parameters.Add("@Id_Sintoma", System.Data.SqlDbType.Int).Value = pacienteSintoma.IdSintoma;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteSintomaExists(pacienteSintoma.IdPacienteSintoma))
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
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido1", pacienteSintoma.IdPaciente);
            ViewData["IdSintoma"] = new SelectList(_context.Sintomas, "IdSintoma", "Descripcion", pacienteSintoma.IdSintoma);
            return View(pacienteSintoma);
        }

        // GET: PacienteSintoma/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteSintoma = await _context.PacienteSintomas
                .Include(p => p.IdPacienteNavigation)
                .Include(p => p.IdSintomaNavigation)
                .FirstOrDefaultAsync(m => m.IdPacienteSintoma == id);
            if (pacienteSintoma == null)
            {
                return NotFound();
            }

            return View(pacienteSintoma);
        }

        // POST: PacienteSintoma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_EliminarPacienteSintoma";
            cmd.Parameters.Add("@Id_PacienteSintoma", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteSintomaExists(int id)
        {
            return _context.PacienteSintomas.Any(e => e.IdPacienteSintoma == id);
        }
    }
}
