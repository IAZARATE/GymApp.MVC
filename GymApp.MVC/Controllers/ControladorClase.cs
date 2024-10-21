using GymApp.MVC.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.MVC.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GymApp.MVC.Controllers
{
    public class ControladorClase : Controller
    {
        private readonly GymContext _context;

        public ControladorClase(GymContext context)
        {
            _context = context;
        }

        // GET: ControladorClase
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clases.ToListAsync());
        }

        // GET: ControladorClase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clase == null)
            {
                return NotFound();
            }

            return View(clase);
        }

        // GET: ControladorClase/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControladorClase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Horario")] Clase clase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clase);
        }

        // GET: ControladorClase/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clases.FindAsync(id);
            if (clase == null)
            {
                return NotFound();
            }
            return View(clase);
        }

        // POST: ControladorClase/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Horario")] Clase clase)
        {
            if (id != clase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaseExists(clase.Id))
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
            return View(clase);
        }

        // GET: ControladorClase/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clase == null)
            {
                return NotFound();
            }

            return View(clase);
        }

        // POST: ControladorClase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clase = await _context.Clases.FindAsync(id);
            if (clase != null)
            {
                _context.Clases.Remove(clase);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ClaseExists(int id)
        {
            return _context.Clases.Any(e => e.Id == id);
        }

        // GET: ControladorClase/GestionarInscripcion
        public IActionResult GestionarInscripcion(int? claseId, int? miembroId)
        {
            if (claseId == null || miembroId == null)
            {
                return NotFound();
            }
            ViewBag.ClaseId = claseId;
            ViewBag.MiembroId = miembroId;
            return View();
        }

        // POST: ControladorClase/GestionarInscripcion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GestionarInscripcion(int claseId, int miembroId)
        {
            var clase = await _context.Clases.FindAsync(claseId);
            var miembro = await _context.Miembros.FindAsync(miembroId);

            if (clase != null && miembro != null)
            {
                clase.InscribirMiembro(miembro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = claseId });
            }
            return NotFound();
        }

        // GET: ControladorClase/GestionarCancelacion
        public IActionResult GestionarCancelacion(int? claseId, int? miembroId)
        {
            if (claseId == null || miembroId == null)
            {
                return NotFound();
            }
            ViewBag.ClaseId = claseId;
            ViewBag.MiembroId = miembroId;
            return View();
        }

        // POST: ControladorClase/GestionarCancelacion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GestionarCancelacion(int claseId, int miembroId)
        {
            var clase = await _context.Clases.FindAsync(claseId);
            var miembro = await _context.Miembros.FindAsync(miembroId);

            if (clase != null && miembro != null)
            {
                clase.MiembrosInscritos.Remove(miembro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = claseId });
            }
            return NotFound();
        }
    }
}