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
    public class IntervencionController : Controller
    {
        List<MayorIntervencionDoctor> listaintervencion = new List<MayorIntervencionDoctor>();
        List<DetalleIntervencion> listadetalle = new List<DetalleIntervencion>();
        SqlDataAdapter adapter;
        private readonly GuanaHospiContext _context;

        public IntervencionController(GuanaHospiContext context)
        {
            _context = context;
        }

        // GET: Intervencion
        public async Task<IActionResult> Index()
        {
            var guanaHospiContext = _context.Intervenciones.Include(i => i.IdDoctorNavigation);
            return View(await guanaHospiContext.ToListAsync());
        }


        public List<MayorIntervencionDoctor> ListarMayorIntervenciones()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_MayorIntervencionesDoctor", conn);
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
                            MayorIntervencionDoctor intervencion = new MayorIntervencionDoctor();
                            intervencion.Id = Int32.Parse(datatable.Rows[i][0].ToString());
                            intervencion.NombreDoctor = datatable.Rows[i][1].ToString();
                            intervencion.CantidadIntervencion = Int32.Parse(datatable.Rows[i][2].ToString());
                            listaintervencion.Add(intervencion);
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listaintervencion;
        }

        public List<DetalleIntervencion> ListarDetalleIntervenciones()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("sp_DetallesIntervencion", conn);
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
                            DetalleIntervencion detalleintervencion = new DetalleIntervencion();
                            detalleintervencion.NombreIntervencion = datatable.Rows[i][0].ToString();
                            detalleintervencion.Descripcion = datatable.Rows[i][0].ToString();
                            detalleintervencion.MasFrecuente = Int32.Parse(datatable.Rows[i][2].ToString());
                            listadetalle.Add(detalleintervencion);
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            return listadetalle;
        }

        public IActionResult DetalleIntervencion()
        {
            return View(ListarDetalleIntervenciones());
        }
        public IActionResult MayorIntervencion()
        {
            return View(ListarMayorIntervenciones());
        }

        // GET: Intervencion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervencion = await _context.Intervenciones
                .Include(i => i.IdDoctorNavigation)
                .FirstOrDefaultAsync(m => m.IdIntervencion == id);
            if (intervencion == null)
            {
                return NotFound();
            }

            return View(intervencion);
        }

        // GET: Intervencion/Create
        public IActionResult Create()
        {
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Apellido1");
            return View();
        }

        // POST: Intervencion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIntervencion,Nombre,Descripcion,FechaIntervencion,Hora,IdDoctor")] Intervencion intervencion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intervencion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Apellido1", intervencion.IdDoctor);
            return View(intervencion);
        }

        // GET: Intervencion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervencion = await _context.Intervenciones.FindAsync(id);
            if (intervencion == null)
            {
                return NotFound();
            }
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Apellido1", intervencion.IdDoctor);
            return View(intervencion);
        }

        // POST: Intervencion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdIntervencion,Nombre,Descripcion,FechaIntervencion,Hora,IdDoctor")] Intervencion intervencion)
        {
            if (id != intervencion.IdIntervencion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intervencion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntervencionExists(intervencion.IdIntervencion))
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
            ViewData["IdDoctor"] = new SelectList(_context.Doctors, "IdDoctor", "Apellido1", intervencion.IdDoctor);
            return View(intervencion);
        }

        // GET: Intervencion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervencion = await _context.Intervenciones
                .Include(i => i.IdDoctorNavigation)
                .FirstOrDefaultAsync(m => m.IdIntervencion == id);
            if (intervencion == null)
            {
                return NotFound();
            }

            return View(intervencion);
        }

        // POST: Intervencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intervencion = await _context.Intervenciones.FindAsync(id);
            _context.Intervenciones.Remove(intervencion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntervencionExists(int id)
        {
            return _context.Intervenciones.Any(e => e.IdIntervencion == id);
        }
    }
}
