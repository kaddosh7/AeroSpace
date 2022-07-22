using AeroSpace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace AeroSpace.Controllers
{
    [Authorize]
    public class PilotoController : Controller
    {
        private readonly AeroSpaceContext _context;

        public PilotoController(AeroSpaceContext context)
        {
            _context = context;
        }

        // GET: PilotoController
        public async Task<ActionResult> Index()
        {
            var pilotos = await _context.Pilotos.Where(x => x.EstadoPiloto == 1).Include(x => x.Persona).ToListAsync();
            return View(pilotos);
        }

        // GET: PilotoController/Details/5
        public ActionResult Details(int? id, Persona persona)
        {
            var m_piloto = _context.Pilotos.Find(id);
            if (m_piloto == null)
            {
                return NotFound();
            }

            ViewData["IdPiloto"] = new SelectList(_context.Personas.Where(x => x.CargoPersona == "Piloto"), "IdPersona", "NombrePersona");
            return View(m_piloto);
        }

        // GET: PilotoController/Create
        public ActionResult Create()
        {
            ViewData["IdPiloto"] = new SelectList(_context.Personas.Where(x => x.CargoPersona == "Piloto"), "IdPersona", "NombrePersona");
            return View();
        }

        // POST: PilotoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Piloto m_piloto)
        {
            if (ModelState.IsValid)
            {
                var piloto = new Piloto()
                {
                    LicenciaPiloto = m_piloto.PersonaId + "0000",
                    HorasVuelo = m_piloto.HorasVuelo,
                    PersonaId = m_piloto.PersonaId,
                    FechaRev = DateTime.Now
                };
                _context.Pilotos.Add(piloto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdPiloto"] = new SelectList(_context.Personas.Where(x => x.CargoPersona == "Piloto"), "IdPersona", "NombrePersona", m_piloto.PersonaId);
            return View();
        }

        // GET: PilotoController/Edit/5
        public ActionResult Edit(int? id)
        {
            var piloto = _context.Pilotos.Find(id);
            if (piloto == null)
            {
                return NotFound();
            }
            ViewData["IdPiloto"] = new SelectList(_context.Personas.Where(x => x.CargoPersona == "Piloto"), "IdPersona", "NombrePersona");
            return View(piloto);
        }

        // POST: PilotoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, Piloto m_piloto)
        {
            if (ModelState.IsValid)
            {
                m_piloto.EstadoPiloto = 1;
                _context.Pilotos.Update(m_piloto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdPiloto"] = new SelectList(_context.Personas.Where(x => x.CargoPersona == "Piloto"), "IdPersona", "NombrePersona");
            return View(m_piloto);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            var piloto = _context.Pilotos.Find(id);
            if (piloto == null)
            {
                return NotFound();
            }

            piloto.EstadoPiloto = 0;
            _context.Pilotos.Update(piloto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
