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
using Rotativa.AspNetCore;

namespace GuanaHospi.Controllers
{
    public class SecretariaController : Controller
    {
        private readonly GuanaHospiContext _context;

        public SecretariaController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: Secretaria
        public async Task<IActionResult> Index()
        {
            return View(await _context.Secretaria.ToListAsync());
        }


        public async Task<IActionResult> PDF()
        {
            return new ViewAsPdf(await _context.Secretaria.ToListAsync())
            {
                FileName = "Secretaria.pdf"
            };
        }

        // GET: Secretaria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretaria = await _context.Secretaria
                .FirstOrDefaultAsync(m => m.IdSecretaria == id);
            if (secretaria == null)
            {
                return NotFound();
            }

            return View(secretaria);
        }

        // GET: Secretaria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Secretaria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSecretaria,Cedula,Nombre,Apellido1,Apellido2,FechaNacimiento,Edad,Email,FechaContratacion")] Secretaria secretaria)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_InsertarSecretaria";
                cmd.Parameters.Add("@Cedula", System.Data.SqlDbType.VarChar, 15).Value = secretaria.Cedula;
                cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 15).Value = secretaria.Nombre;
                cmd.Parameters.Add("@Apellido1", System.Data.SqlDbType.VarChar, 15).Value = secretaria.Apellido1;
                cmd.Parameters.Add("@Apellido2", System.Data.SqlDbType.VarChar, 15).Value = secretaria.Apellido2;
                cmd.Parameters.Add("@FechaNacimiento", System.Data.SqlDbType.Date).Value = secretaria.FechaNacimiento;
                cmd.Parameters.Add("@Edad", System.Data.SqlDbType.VarChar, 50).Value = secretaria.Edad;
                cmd.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 15).Value = secretaria.Email;
                cmd.Parameters.Add("@Fecha_Contratacion", System.Data.SqlDbType.Date).Value = secretaria.FechaContratacion;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            return View(secretaria);
        }

        // GET: Secretaria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretaria = await _context.Secretaria.FindAsync(id);
            if (secretaria == null)
            {
                return NotFound();
            }
            return View(secretaria);
        }

        // POST: Secretaria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSecretaria,Cedula,Nombre,Apellido1,Apellido2,FechaNacimiento,Edad,Email,FechaContratacion")] Secretaria secretaria)
        {
            if (id != secretaria.IdSecretaria)
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
                    cmd.CommandText = "SP_Actualizar_Secretaria";
                    cmd.Parameters.Add("@Id_Secretaria", System.Data.SqlDbType.Int).Value = secretaria.IdSecretaria;
                    cmd.Parameters.Add("@Cedula", System.Data.SqlDbType.VarChar, 15).Value = secretaria.Cedula;
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 15).Value = secretaria.Nombre;
                    cmd.Parameters.Add("@Apellido1", System.Data.SqlDbType.VarChar, 15).Value = secretaria.Apellido1;
                    cmd.Parameters.Add("@Apellido2", System.Data.SqlDbType.VarChar, 15).Value = secretaria.Apellido2;
                    cmd.Parameters.Add("@FechaNacimiento", System.Data.SqlDbType.Date).Value = secretaria.FechaNacimiento;
                    cmd.Parameters.Add("@Edad", System.Data.SqlDbType.VarChar, 50).Value = secretaria.Edad;
                    cmd.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 15).Value = secretaria.Email;
                    cmd.Parameters.Add("@Fecha_Contratacion", System.Data.SqlDbType.Date).Value = secretaria.FechaContratacion;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecretariaExists(secretaria.IdSecretaria))
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
            return View(secretaria);
        }

        // GET: Secretaria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretaria = await _context.Secretaria
                .FirstOrDefaultAsync(m => m.IdSecretaria == id);
            if (secretaria == null)
            {
                return NotFound();
            }

            return View(secretaria);
        }

        // POST: Secretaria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_EliminarSecretaria";
            cmd.Parameters.Add("@Id_Secretaria", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            return RedirectToAction(nameof(Index));
        }

        private bool SecretariaExists(int id)
        {
            return _context.Secretaria.Any(e => e.IdSecretaria == id);
        }
    }
}
