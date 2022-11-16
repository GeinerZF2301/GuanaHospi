using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuanaHospi.Data;
using GuanaHospi.Models;
using GuanaHospi.Models.ViewModels;
using Microsoft.Data.SqlClient;
using System.Data;
using Rotativa.AspNetCore;

namespace GuanaHospi.Controllers
{
    public class UnidadController : Controller
    {
        List<UnidadMasPacientes> listaunidadmaspacientes = new List<UnidadMasPacientes>();
        SqlDataAdapter adapter;
        private readonly GuanaHospiContext _context;

        public UnidadController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: Unidad
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.Unidades.Include(u => u.IdDoctorNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }
        public async Task<IActionResult> PDF()
        {
            return new ViewAsPdf(await _context.Unidades.Include(u => u.IdDoctorNavigation).ToListAsync())
            {
                FileName = "Unidad.pdf"
            };
        }

        public List<UnidadMasPacientes> ListarUnidadMasPacientes()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_UnidadesMasPacientes", conn);
                using (adapter)
                {
                    conn.Open();
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.Fill(datatable);
                    int tamanno = datatable.Rows.Count;
                    if (tamanno > 0)
                    {
                        for (int i = 0; i < tamanno; i++)
                        {
                            UnidadMasPacientes unidad = new UnidadMasPacientes();
                            unidad.IdUnidad = Int32.Parse(datatable.Rows[i][0].ToString());
                            unidad.NombreUnidad = datatable.Rows[i][1].ToString();
                            unidad.CantidadPacientes = Int32.Parse(datatable.Rows[i][2].ToString());
                            listaunidadmaspacientes.Add(unidad);
                        }
                    }
                conn.Close();
                }
            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaunidadmaspacientes;
        }
        public IActionResult UnidadMasPacientes()
        {
            return View(ListarUnidadMasPacientes());
        }
        // GET: Unidad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidad = await _context.Unidades
                .Include(u => u.IdDoctorNavigation)
                .FirstOrDefaultAsync(m => m.IdUnidad == id);
            if (unidad == null)
            {
                return NotFound();
            }

            return View(unidad);
        }

        // GET: Unidad/Create
        public IActionResult Create()
        {
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula");
            return View();
        }

        // POST: Unidad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUnidad,Nombre,Planta,IdDoctor")] Unidad unidad)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_InsertarUnidad";
                cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 15).Value = unidad.Nombre;
                cmd.Parameters.Add("@Planta", System.Data.SqlDbType.VarChar, 15).Value = unidad.Planta;
                cmd.Parameters.Add("@Id_Doctor", System.Data.SqlDbType.Int).Value = unidad.IdDoctor;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula", unidad.IdDoctor);
            return View(unidad);
        }

        // GET: Unidad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidad = await _context.Unidades.FindAsync(id);
            if (unidad == null)
            {
                return NotFound();
            }
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula", unidad.IdDoctor);
            return View(unidad);
        }

        // POST: Unidad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUnidad,Nombre,Planta,IdDoctor")] Unidad unidad)
        {
            if (id != unidad.IdUnidad)
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
                    cmd.CommandText = "SP_Actualizar_Unidad";
                    cmd.Parameters.Add("@Id_Unidad", System.Data.SqlDbType.Int).Value = unidad.IdUnidad;
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 15).Value = unidad.Nombre;
                    cmd.Parameters.Add("@Planta", System.Data.SqlDbType.VarChar, 15).Value = unidad.Planta;
                    cmd.Parameters.Add("@Id_Doctor", System.Data.SqlDbType.Int).Value = unidad.IdDoctor;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadExists(unidad.IdUnidad))
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
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Cedula", unidad.IdDoctor);
            return View(unidad);
        }

        // GET: Unidad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidad = await _context.Unidades
                .Include(u => u.IdDoctorNavigation)
                .FirstOrDefaultAsync(m => m.IdUnidad == id);
            if (unidad == null)
            {
                return NotFound();
            }

            return View(unidad);
        }

        // POST: Unidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_EliminarUnidad";
            cmd.Parameters.Add("@Id_Unidad", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadExists(int id)
        {
            return _context.Unidades.Any(e => e.IdUnidad == id);
        }
    }
}
