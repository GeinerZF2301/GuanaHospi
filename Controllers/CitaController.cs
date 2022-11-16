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
    public class CitaController : Controller
    {
        private readonly GuanaHospiContext _context;

        public CitaController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: Cita
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.Cita.Include(c => c.IdDoctorNavigation).Include(c => c.IdPacienteNavigation).Include(c => c.IdSecretariaNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        // GET: Cita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Cita
                .Include(c => c.IdDoctorNavigation)
                .Include(c => c.IdPacienteNavigation)
                .Include(c => c.IdSecretariaNavigation)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Cita/Create
        public IActionResult Create()
        {
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor","Cedula");
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Cedula");
            ViewData["IdSecretaria"] = new SelectList(_context.Secretaria, "IdSecretaria", "Cedula");
            return View();
        }

        // POST: Cita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,FechaIngreso,FechaSalida,Observaciones,IdDoctor,IdPaciente,IdSecretaria")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();

                _context.Add(cita);
                await _context.SaveChangesAsync();

                //conn.Open();
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandText = "SP_InsertarCita";
                //cmd.Parameters.Add("@FechaIngreso", System.Data.SqlDbType.Date).Value = cita.FechaIngreso;
                //cmd.Parameters.Add("@FechaSalida", System.Data.SqlDbType.Date).Value = cita.FechaSalida;
                //cmd.Parameters.Add("@Observaciones", System.Data.SqlDbType.VarChar, 15).Value = cita.Observaciones;
                //cmd.Parameters.Add("@Id_Doctor", System.Data.SqlDbType.Int).Value = cita.IdDoctor;
                //cmd.Parameters.Add("@Id_Paciente", System.Data.SqlDbType.Int).Value = cita.IdPaciente;
                //cmd.Parameters.Add("@Id_Secretaria", System.Data.SqlDbType.Int).Value = cita.IdSecretaria;
                //await cmd.ExecuteNonQueryAsync();
                //conn.Close();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula", cita.IdDoctor);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Cedula", cita.IdPaciente);
            ViewData["IdSecretaria"] = new SelectList(_context.Secretaria, "IdSecretaria", "Cedula", cita.IdSecretaria);
            return View(cita);
        }

        // GET: Cita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Cita.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula", cita.IdDoctor);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Cedula", cita.IdPaciente);
            ViewData["IdSecretaria"] = new SelectList(_context.Secretaria, "IdSecretaria", "Cedula", cita.IdSecretaria);
            return View(cita);
        }

        // POST: Cita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,FechaIngreso,FechaSalida,Observaciones,IdDoctor,IdPaciente,IdSecretaria")] Cita cita)
        {
            if (id != cita.IdCita)
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
                    cmd.CommandText = "SP_Actualizar_Cita";
                    cmd.Parameters.Add("@Id_Cita", System.Data.SqlDbType.Int).Value = cita.IdCita;
                    cmd.Parameters.Add("@FechaIngreso", System.Data.SqlDbType.Date).Value = cita.FechaIngreso;
                    cmd.Parameters.Add("@FechaSalida", System.Data.SqlDbType.Date).Value = cita.FechaSalida;
                    cmd.Parameters.Add("@Observaciones", System.Data.SqlDbType.VarChar, 15).Value = cita.Observaciones;
                    cmd.Parameters.Add("@Id_Doctor", System.Data.SqlDbType.Int).Value = cita.IdDoctor;
                    cmd.Parameters.Add("@Id_Paciente", System.Data.SqlDbType.Int).Value = cita.IdPaciente;
                    cmd.Parameters.Add("@Id_Secretaria", System.Data.SqlDbType.Int).Value = cita.IdSecretaria;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.IdCita))
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
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula", cita.IdDoctor);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Cedula", cita.IdPaciente);
            ViewData["IdSecretaria"] = new SelectList(_context.Secretaria, "IdSecretaria", "Cedula", cita.IdSecretaria);
            return View(cita);
        }

        // GET: Cita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Cita
                .Include(c => c.IdDoctorNavigation)
                .Include(c => c.IdPacienteNavigation)
                .Include(c => c.IdSecretariaNavigation)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Cita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_EliminarCita";
            cmd.Parameters.Add("@Id_Cita", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Cita.Any(e => e.IdCita == id);
        }
    }
}
