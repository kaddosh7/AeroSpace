using AeroSpace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace AeroSpace.Controllers
{
    [Authorize]
    public class HangarController : Controller
    {
        private readonly AeroSpaceContext _context;

        public HangarController(AeroSpaceContext context)
        {
            _context = context;
        }
        // GET: HangarController
        public async Task<ActionResult> Index()
        {
            // var hangares = await _context.Hangars.Where(h => h.EstadoHangar == 1).Include(h => h.Avion).Include(p => p.Persona).ToListAsync();
            var hangares = await _context.Hangars.Where(h => h.EstadoHangar == 1).Include(p => p.Persona).ToListAsync();
            return View(hangares);
        }

        // GET: HangarController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var hangar = _context.Hangars.Find(id);
            ViewData["IdPersona"] = new SelectList(_context.Personas, "IdPersona", "NombrePersona");
            return View(hangar);
        }

        // GET: HangarController/Create
        public ActionResult Create()
        {
            ViewData["IdPersona"] = new SelectList(_context.Personas, "IdPersona", "NombrePersona");
            return View();
        }

        // POST: HangarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Hangar hangar)
        {
            if (ModelState.IsValid)
            {
                var newHangar = new Hangar()
                {
                     Ubicacion = hangar.Ubicacion,
                     CapacidadHangar = hangar.CapacidadHangar,
                     PersonaId = hangar.PersonaId,
                };
                _context.Add(newHangar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(_context.Personas, "IdPersona", "NombrePersona", hangar.PersonaId);
            return View(hangar);
        }

        // GET: HangarController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangar = _context.Hangars.Find(id);
            ViewData["IdPersona"] = new SelectList(_context.Personas, "IdPersona", "NombrePersona");
            return View(hangar);
        }

        // POST: HangarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Hangar hangar)
        {
            if (ModelState.IsValid)
            {
                hangar.EstadoHangar = 1;
                _context.Hangars.Update(hangar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hangar);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            var hangar = _context.Hangars.Find(id);
            if (hangar == null)
            {
                return NotFound();
            }

            hangar.EstadoHangar = 0;
            _context.Hangars.Update(hangar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
