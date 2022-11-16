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

namespace GuanaHospi.Controllers
{
    public class EnfermedadController : Controller
    {
        List<TopEnfermedades> listatop = new List<TopEnfermedades>();
        SqlDataAdapter adapter;
        private readonly GuanaHospiContext _context;

        public EnfermedadController(GuanaHospiContext context)
        {
            _context = context;
        }


        public List<TopEnfermedades> ListarTopEnfermedades()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_TopEnfermedadesAtendidas", conn);
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
                            TopEnfermedades top = new TopEnfermedades();
                            
                            top.NombreEnfermedad = datatable.Rows[i][0].ToString();
                            top.Sintoma = Int32.Parse(datatable.Rows[i][1].ToString());
                            listatop.Add(top);
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listatop;
        }

        public IActionResult TopEnfermedades()
        {
            return View(ListarTopEnfermedades());
        }

        // GET: Enfermedad
        public async Task<IActionResult> Index()
        {
            return View(await _context.Enfermedades.ToListAsync());
        }

        // GET: Enfermedad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedad = await _context.Enfermedades
                .FirstOrDefaultAsync(m => m.IdEnfermedad == id);
            if (enfermedad == null)
            {
                return NotFound();
            }

            return View(enfermedad);
        }

        // GET: Enfermedad/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enfermedad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEnfermedad,Nombre,Descripcion")] Enfermedad enfermedad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enfermedad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enfermedad);
        }

        // GET: Enfermedad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedad = await _context.Enfermedades.FindAsync(id);
            if (enfermedad == null)
            {
                return NotFound();
            }
            return View(enfermedad);
        }

        // POST: Enfermedad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEnfermedad,Nombre,Descripcion")] Enfermedad enfermedad)
        {
            if (id != enfermedad.IdEnfermedad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enfermedad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermedadExists(enfermedad.IdEnfermedad))
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
            return View(enfermedad);
        }

        // GET: Enfermedad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedad = await _context.Enfermedades
                .FirstOrDefaultAsync(m => m.IdEnfermedad == id);
            if (enfermedad == null)
            {
                return NotFound();
            }

            return View(enfermedad);
        }

        // POST: Enfermedad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enfermedad = await _context.Enfermedades.FindAsync(id);
            _context.Enfermedades.Remove(enfermedad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermedadExists(int id)
        {
            return _context.Enfermedades.Any(e => e.IdEnfermedad == id);
        }
    }
}
