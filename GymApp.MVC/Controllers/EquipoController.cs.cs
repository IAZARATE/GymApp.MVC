using GymApp.MVC.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.MVC.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GymApp.MVC.Controllers
{
    public class EquipoController : Controller
    {
        private readonly GymContext _context;

        public EquipoController (GymContext context)
        {
            _context = context;
        }

        // GET: ControladorEquipo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Equipos.ToListAsync());
        }

        // GET: ControladorEquipo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // GET: ControladorEquipo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControladorEquipo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Estado")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipo);
        }

        // GET: ControladorEquipo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }
            return View(equipo);
        }

        // POST: ControladorEquipo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Estado")] Equipo equipo)
        {
            if (id != equipo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipoExists(equipo.Id))
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
            return View(equipo);
        }

        // GET: ControladorEquipo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // POST: ControladorEquipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo != null)
            {
                _context.Equipos.Remove(equipo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.Id == id);
        }

        // GET: ControladorEquipo/GestionarReserva
        public IActionResult GestionarReserva(int? equipoId, int? miembroId)
        {
            if (equipoId == null || miembroId == null)
            {
                return NotFound();
            }
            ViewBag.EquipoId = equipoId;
            ViewBag.MiembroId = miembroId;
            return View();
        }

        // POST: ControladorEquipo/GestionarReserva
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GestionarReserva(int equipoId, int miembroId, DateTime fechaInicio, DateTime fechaFin)
        {
            var equipo = await _context.Equipos.FindAsync(equipoId);
            var miembro = await _context.Miembros.FindAsync(miembroId);

            if (equipo != null && miembro != null)
            {
                equipo.Reservar(miembro, fechaInicio, fechaFin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = equipoId });
            }
            return NotFound();
        }
    }
}