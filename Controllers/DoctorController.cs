﻿using System;
using System.Collections.Generic;
using System.Data;
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
    public class DoctorController : Controller
    {
        List<Doctor> listadoctores = new List<Doctor>();
        SqlDataAdapter adapter;
        private readonly GuanaHospiContext _context;
        public DoctorController(GuanaHospiContext context)
        {
            _context = context;
        }


        public List<Doctor> ListarDoctores()
        {
            DataTable datatable = new DataTable();
            string error;
            try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                adapter = new SqlDataAdapter("SP_ListarDoctor", conn);
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
                            Doctor doctor = new Doctor();
                            doctor.IdDoctor = Int32.Parse(datatable.Rows[i][0].ToString());
                            doctor.Cedula = datatable.Rows[i][1].ToString();
                            doctor.Nombre = datatable.Rows[i][2].ToString();
                            doctor.Apellido1 = datatable.Rows[i][3].ToString();
                            doctor.Apellido2 = datatable.Rows[i][4].ToString();
                            doctor.FechaNacimiento = DateTime.Parse(datatable.Rows[i][5].ToString());
                            doctor.Email = datatable.Rows[i][6].ToString();
                            doctor.Salario = datatable.Rows[i][7].ToString();
                            doctor.FechaContratacion = DateTime.Parse(datatable.Rows[i][8].ToString());
                            listadoctores.Add(doctor);
                        }
                    }
                    conn.Close();

                }

            }catch(Exception e)
            {
                error = e.InnerException.Message;
            }

            return listadoctores;
        }

        // GET: Doctor
        public IActionResult Index()
        {
            //return View(await _context.Doctors.ToListAsync());
            return View(ListarDoctores());
        }

        public IActionResult PDF()
        {
            return new ViewAsPdf(ListarDoctores())
            {
                FileName = "Doctor.pdf"
            };
        }

        // GET: Doctor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.IdDoctor == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDoctor,Cedula,Nombre,Apellido1,Apellido2,FechaNacimiento,Email,Salario,FechaContratacion")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(doctor);
                //await _context.SaveChangesAsync();
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_InsertarDoctor";
                cmd.Parameters.Add("@Cedula", System.Data.SqlDbType.VarChar, 10).Value = doctor.Cedula;
                cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 30).Value = doctor.Nombre;
                cmd.Parameters.Add("@Apellido1", System.Data.SqlDbType.VarChar, 30).Value = doctor.Apellido1;
                cmd.Parameters.Add("@Apellido2", System.Data.SqlDbType.VarChar, 30).Value = doctor.Apellido2;
                cmd.Parameters.Add("@FechaNacimiento", System.Data.SqlDbType.Date).Value = doctor.FechaNacimiento;
                cmd.Parameters.Add("@Email", System.Data.SqlDbType.VarChar,30).Value = doctor.Email;
                cmd.Parameters.Add("@Salario", System.Data.SqlDbType.VarChar, 10).Value = doctor.Salario;
                cmd.Parameters.Add("@Fecha_Contratacion", System.Data.SqlDbType.Date).Value = doctor.FechaContratacion;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDoctor,Cedula,Nombre,Apellido1,Apellido2,FechaNacimiento,Email,Salario,FechaContratacion")] Doctor doctor)
        {
            if (id != doctor.IdDoctor)
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
                    cmd.CommandText = "SP_Actualizar_Doctor";
                    cmd.Parameters.Add("@Id_Doctor", System.Data.SqlDbType.Int).Value = doctor.IdDoctor;
                    cmd.Parameters.Add("@Cedula", System.Data.SqlDbType.VarChar, 10).Value = doctor.Cedula;
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 30).Value = doctor.Nombre;
                    cmd.Parameters.Add("@Apellido1", System.Data.SqlDbType.VarChar, 30).Value = doctor.Apellido1;
                    cmd.Parameters.Add("@Apellido2", System.Data.SqlDbType.VarChar, 30).Value = doctor.Apellido2;
                    cmd.Parameters.Add("@FechaNacimiento", System.Data.SqlDbType.Date).Value = doctor.FechaNacimiento;
                    cmd.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 30).Value = doctor.Email;
                    cmd.Parameters.Add("@Salario", System.Data.SqlDbType.VarChar, 10).Value = doctor.Salario;
                    cmd.Parameters.Add("@Fecha_Contratacion", System.Data.SqlDbType.Date).Value = doctor.FechaContratacion;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(doctor);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.IdDoctor))
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
            return View(doctor);
        }

        // GET: Doctor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.IdDoctor == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_EliminarDoctor";
            cmd.Parameters.Add("@Id_Doctor", System.Data.SqlDbType.Int).Value = id;
            
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var doctor = await _context.Doctors.FindAsync(id);
            //_context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.IdDoctor == id);
        }
    }
}
