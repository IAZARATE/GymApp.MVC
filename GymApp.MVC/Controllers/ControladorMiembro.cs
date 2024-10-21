
using GymApp.MVC.Entidades;
using GymApp.MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GymApp.MVC.Controllers
{
    public class ControladorMiembro : Controller
    {
        private readonly GymContext _context;

        public ControladorMiembro(GymContext context)
        {
            _context = context;
        }

        // GET: ControladorMiembro
        public async Task<IActionResult> Index()
        {
            return View(await _context.Miembros.ToListAsync());
        }

        // GET: ControladorMiembro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miembro = await _context.Miembros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (miembro == null)
            {
                return NotFound();
            }

            return View(miembro);
        }

        // GET: ControladorMiembro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControladorMiembro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Email")] Miembro miembro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(miembro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(miembro);
        }

        // GET: ControladorMiembro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miembro = await _context.Miembros.FindAsync(id);
            if (miembro == null)
            {
                return NotFound();
            }
            return View(miembro);
        }

        // POST: ControladorMiembro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Email")] Miembro miembro)
        {
            if (id != miembro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(miembro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiembroExists(miembro.Id))
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
            return View(miembro);
        }

        // GET: ControladorMiembro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miembro = await _context.Miembros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (miembro == null)
            {
                return NotFound();
            }

            return View(miembro);
        }

        // POST: ControladorMiembro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miembro = await _context.Miembros.FindAsync(id);
            if (miembro != null)
            {
                _context.Miembros.Remove(miembro);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MiembroExists(int id)
        {
            return _context.Miembros.Any(e => e.Id == id);
        }

        // GET: ControladorMiembro/GestionarRegistro
        public IActionResult GestionarRegistro()
        {
            return View();
        }

        // POST: ControladorMiembro/GestionarRegistro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GestionarRegistro([Bind("Nombre,Email")] Miembro miembro)
        {
            if (ModelState.IsValid)
            {
                miembro.Registrar();
                _context.Add(miembro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(miembro);
        }

        // GET: ControladorMiembro/GestionarRenovacion/5
        public async Task<IActionResult> GestionarRenovacion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miembro = await _context.Miembros.FindAsync(id);
            if (miembro == null)
            {
                return NotFound();
            }
            return View(miembro);
        }

        // POST: ControladorMiembro/GestionarRenovacion/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GestionarRenovacion(int id, string tipoMembresia)
        {
            var miembro = await _context.Miembros.FindAsync(id);
            if (miembro == null || string.IsNullOrEmpty(tipoMembresia))
            {
                return NotFound();
            }

            miembro.RenovarMembresia(tipoMembresia);
            _context.Update(miembro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = miembro.Id });
        }
    }
}