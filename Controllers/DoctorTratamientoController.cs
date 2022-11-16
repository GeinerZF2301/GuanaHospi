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
    public class DoctorTratamientoController : Controller
    {
        private readonly GuanaHospiContext _context;

        public DoctorTratamientoController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: DoctorTratamiento
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.DoctorTratamientos.Include(d => d.IdDoctorNavigation).Include(d => d.IdTratamientoNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }

        // GET: DoctorTratamiento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorTratamiento = await _context.DoctorTratamientos
                .Include(d => d.IdDoctorNavigation)
                .Include(d => d.IdTratamientoNavigation)
                .FirstOrDefaultAsync(m => m.IdDoctorTratamiento == id);
            if (doctorTratamiento == null)
            {
                return NotFound();
            }

            return View(doctorTratamiento);
        }

        // GET: DoctorTratamiento/Create
        public IActionResult Create()
        {
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula");
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre");
            return View();
        }

        // POST: DoctorTratamiento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDoctorTratamiento,IdDoctor,IdTratamiento")] DoctorTratamiento doctorTratamiento)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_InsertarDoctorTratamiento";
                cmd.Parameters.Add("@Id_Doctor", System.Data.SqlDbType.Int).Value = doctorTratamiento.IdDoctor;
                cmd.Parameters.Add("@Id_Tratamiento", System.Data.SqlDbType.Int).Value = doctorTratamiento.IdTratamiento;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula", doctorTratamiento.IdDoctor);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre", doctorTratamiento.IdTratamiento);
            return View(doctorTratamiento);
        }

        // GET: DoctorTratamiento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorTratamiento = await _context.DoctorTratamientos.FindAsync(id);
            if (doctorTratamiento == null)
            {
                return NotFound();
            }
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula", doctorTratamiento.IdDoctor);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre", doctorTratamiento.IdTratamiento);
            return View(doctorTratamiento);
        }

        // POST: DoctorTratamiento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDoctorTratamiento,IdDoctor,IdTratamiento")] DoctorTratamiento doctorTratamiento)
        {
            if (id != doctorTratamiento.IdDoctorTratamiento)
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
                    cmd.CommandText = "SP_Actualizar_DoctorTratamiento";
                    cmd.Parameters.Add("@Id_DoctorTratamiento", System.Data.SqlDbType.Int).Value = doctorTratamiento.IdDoctorTratamiento;
                    cmd.Parameters.Add("@Id_Doctor", System.Data.SqlDbType.Int).Value = doctorTratamiento.IdDoctor;
                    cmd.Parameters.Add("@Id_Tratamiento", System.Data.SqlDbType.Int).Value = doctorTratamiento.IdTratamiento;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorTratamientoExists(doctorTratamiento.IdDoctorTratamiento))
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
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula", doctorTratamiento.IdDoctor);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "Nombre", doctorTratamiento.IdTratamiento);
            return View(doctorTratamiento);
        }

        // GET: DoctorTratamiento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorTratamiento = await _context.DoctorTratamientos
                .Include(d => d.IdDoctorNavigation)
                .Include(d => d.IdTratamientoNavigation)
                .FirstOrDefaultAsync(m => m.IdDoctorTratamiento == id);
            if (doctorTratamiento == null)
            {
                return NotFound();
            }

            return View(doctorTratamiento);
        }

        // POST: DoctorTratamiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_EliminarDoctorTratamiento";
            cmd.Parameters.Add("@Id_DoctorTratamiento", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorTratamientoExists(int id)
        {
            return _context.DoctorTratamientos.Any(e => e.IdDoctorTratamiento == id);
        }
    }
}
