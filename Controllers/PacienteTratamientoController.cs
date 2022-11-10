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
    public class PacienteTratamientoController : Controller
    {
        private readonly GuanaHospiContext _context;

        public PacienteTratamientoController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: PacienteTratamiento
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.PacienteTratamientos.Include(p => p.IdPacienteNavigation).Include(p => p.IdTratamientoNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        // GET: PacienteTratamiento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteTratamiento = await _context.PacienteTratamientos
                .Include(p => p.IdPacienteNavigation)
                .Include(p => p.IdTratamientoNavigation)
                .FirstOrDefaultAsync(m => m.IdPacienteTratamiento == id);
            if (pacienteTratamiento == null)
            {
                return NotFound();
            }

            return View(pacienteTratamiento);
        }

        // GET: PacienteTratamiento/Create
        public IActionResult Create()
        {
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido1");
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Descripcion");
            return View();
        }

        // POST: PacienteTratamiento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPacienteTratamiento,IdPaciente,IdTratamiento")] PacienteTratamiento pacienteTratamiento)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_InsertarPacienteTratamiento";
                cmd.Parameters.Add("@Id_Paciente", System.Data.SqlDbType.Int).Value = pacienteTratamiento.IdPaciente;
                cmd.Parameters.Add("@Id_Tratamiento", System.Data.SqlDbType.Int).Value = pacienteTratamiento.IdTratamiento;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido1", pacienteTratamiento.IdPaciente);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Descripcion", pacienteTratamiento.IdTratamiento);
            return View(pacienteTratamiento);
        }

        // GET: PacienteTratamiento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteTratamiento = await _context.PacienteTratamientos.FindAsync(id);
            if (pacienteTratamiento == null)
            {
                return NotFound();
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido1", pacienteTratamiento.IdPaciente);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Descripcion", pacienteTratamiento.IdTratamiento);
            return View(pacienteTratamiento);
        }

        // POST: PacienteTratamiento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPacienteTratamiento,IdPaciente,IdTratamiento")] PacienteTratamiento pacienteTratamiento)
        {
            if (id != pacienteTratamiento.IdPacienteTratamiento)
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
                    cmd.CommandText = "SP_Actualizar_PacienteTratamiento";
                    cmd.Parameters.Add("@Id_PacienteTratamiento", System.Data.SqlDbType.Int).Value = pacienteTratamiento.IdPacienteTratamiento;
                    cmd.Parameters.Add("@Id_Paciente", System.Data.SqlDbType.Int).Value = pacienteTratamiento.IdPaciente;
                    cmd.Parameters.Add("@Id_Tratamiento", System.Data.SqlDbType.Int).Value = pacienteTratamiento.IdTratamiento;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteTratamientoExists(pacienteTratamiento.IdPacienteTratamiento))
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
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido1", pacienteTratamiento.IdPaciente);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Descripcion", pacienteTratamiento.IdTratamiento);
            return View(pacienteTratamiento);
        }

        // GET: PacienteTratamiento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteTratamiento = await _context.PacienteTratamientos
                .Include(p => p.IdPacienteNavigation)
                .Include(p => p.IdTratamientoNavigation)
                .FirstOrDefaultAsync(m => m.IdPacienteTratamiento == id);
            if (pacienteTratamiento == null)
            {
                return NotFound();
            }

            return View(pacienteTratamiento);
        }

        // POST: PacienteTratamiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_EliminarPacienteTratamiento";
            cmd.Parameters.Add("@Id_PacienteTratamiento", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteTratamientoExists(int id)
        {
            return _context.PacienteTratamientos.Any(e => e.IdPacienteTratamiento == id);
        }
    }
}
