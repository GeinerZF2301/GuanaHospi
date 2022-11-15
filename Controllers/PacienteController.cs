using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuanaHospi.Data;
using GuanaHospi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace GuanaHospi.Controllers
{
    public class PacienteController : Controller
    {
        private readonly GuanaHospiContext _context;

        public PacienteController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: Paciente
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.Pacientes.Include(p => p.IdUnidadNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        public async Task<IActionResult> PDF()
        {
            return new ViewAsPdf(await _context.Pacientes.Include(p => p.IdUnidadNavigation).ToListAsync())
            {
                FileName = "Pacientes.pdf"
            };
        }
        // GET: Paciente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.IdUnidadNavigation)
                .FirstOrDefaultAsync(m => m.IdPaciente == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Paciente/Create
        public IActionResult Create()
        {
            ViewData["IdUnidad"] = new SelectList(_context.Unidades, "IdUnidad", "Nombre");
            return View();
        }

        // POST: Paciente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,Nombre,Apellido1,Apellido2,FechaNacimiento,Edad,Carnet,IdUnidad")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_InsertarPaciente";
                cmd.Parameters.Add("@Cedula", System.Data.SqlDbType.VarChar, 10).Value = paciente.Cedula;
                cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 30).Value = paciente.Nombre;
                cmd.Parameters.Add("@Apellido1", System.Data.SqlDbType.VarChar, 30).Value = paciente.Apellido1;
                cmd.Parameters.Add("@Apellido2", System.Data.SqlDbType.VarChar, 30).Value = paciente.Apellido2;
                cmd.Parameters.Add("@FechaNacimiento", System.Data.SqlDbType.Date).Value = paciente.FechaNacimiento;
                cmd.Parameters.Add("@Edad", System.Data.SqlDbType.VarChar,3).Value = paciente.Edad;
                cmd.Parameters.Add("@Carnet", System.Data.SqlDbType.VarChar, 10).Value = paciente.Carnet;
                cmd.Parameters.Add("@Id_Unidad", System.Data.SqlDbType.Int).Value = paciente.IdUnidad;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(paciente);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUnidad"] = new SelectList(_context.Unidades, "IdUnidad", "Nombre", paciente.IdUnidad);
            return View(paciente);
        }

        // GET: Paciente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            ViewData["IdUnidad"] = new SelectList(_context.Unidades, "IdUnidad", "Nombre", paciente.IdUnidad);
            return View(paciente);
        }

        // POST: Paciente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPaciente,Cedula,Nombre,Apellido1,Apellido2,FechaNacimiento,Edad,Carnet,IdUnidad")] Paciente paciente)
        {
            if (id != paciente.IdPaciente)
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
                    cmd.CommandText = "SP_Actualizar_Paciente";
                    cmd.Parameters.Add("@Id_Paciente", System.Data.SqlDbType.Int).Value = paciente.IdUnidad;
                    cmd.Parameters.Add("@Cedula", System.Data.SqlDbType.VarChar, 10).Value = paciente.Cedula;
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 30).Value = paciente.Nombre;
                    cmd.Parameters.Add("@Apellido1", System.Data.SqlDbType.VarChar, 30).Value = paciente.Apellido1;
                    cmd.Parameters.Add("@Apellido2", System.Data.SqlDbType.VarChar, 30).Value = paciente.Apellido2;
                    cmd.Parameters.Add("@FechaNacimiento", System.Data.SqlDbType.Date).Value = paciente.FechaNacimiento;
                    cmd.Parameters.Add("@Edad", System.Data.SqlDbType.VarChar, 3).Value = paciente.Edad;
                    cmd.Parameters.Add("@Carnet", System.Data.SqlDbType.VarChar, 10).Value = paciente.Carnet;
                    cmd.Parameters.Add("@Id_Unidad", System.Data.SqlDbType.Int).Value = paciente.IdUnidad;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(paciente);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.IdPaciente))
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
            ViewData["IdUnidad"] = new SelectList(_context.Unidades, "IdUnidad", "Nombre", paciente.IdUnidad);
            return View(paciente);
        }

        // GET: Paciente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.IdUnidadNavigation)
                .FirstOrDefaultAsync(m => m.IdPaciente == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Paciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_EliminarPaciente";
            cmd.Parameters.Add("@Id_Paciente", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var paciente = await _context.Pacientes.FindAsync(id);
            //_context.Pacientes.Remove(paciente);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.IdPaciente == id);
        }
    }
}
