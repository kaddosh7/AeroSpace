using AeroSpace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace AeroSpace.Controllers
{
    [Authorize]
    public class VueloController : Controller
    {
        private readonly AeroSpaceContext _context;

        public VueloController(AeroSpaceContext context)
        {
            _context = context;
        }
        // GET: VueloController
        public async Task<ActionResult> Index()
        {
            var vuelos = await _context.Vuelos.Where(x => x.EstadoVuelo == 1).Include(a => a.Avion).ToListAsync();
            return View(vuelos);
        }

        // GET: VueloController/Details/5
        public ActionResult Details(int? id)
        {
            var vuelo = _context.Vuelos.Find(id);
            if (vuelo == null)
            {
                return NotFound();
            }
            ViewData["IdAvion"] = new SelectList(_context.Avions, "IdAvion", "Siglas");
            return View(vuelo);
        }

        // GET: VueloController/Create
        public ActionResult Create()
        {
            ViewData["IdAvion"] = new SelectList(_context.Avions, "IdAvion", "Siglas");
            ViewData["IdPiloto"] = new SelectList(_context.Pilotos, "IdPiloto", "PersonaId");
            return View();
        }

        // POST: VueloController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Vuelo m_vuelo)
        {
            if (ModelState.IsValid)
            {
                var vuelo = new Vuelo()
                {
                    Fecha = m_vuelo.Fecha,
                    Hora = m_vuelo.Hora,
                    TipoVuelo = m_vuelo.TipoVuelo,
                    AvionId = m_vuelo.AvionId,
                    PilotoId = m_vuelo.PilotoId,
                    Copiloto = m_vuelo.Copiloto,
                    TipoAvion = m_vuelo.TipoAvion
                };
                _context.Vuelos.Add(vuelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAvion"] = new SelectList(_context.Avions, "IdAvion", "Siglas");
            ViewData["IdPiloto"] = new SelectList(_context.Pilotos, "IdPiloto", "PersonaId");
            return View();
        }

        // GET: VueloController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vuelo = _context.Vuelos.Find(id);
            ViewData["IdAvion"] = new SelectList(_context.Avions, "IdAvion", "Siglas");
            ViewData["IdPiloto"] = new SelectList(_context.Pilotos, "IdPiloto", "PersonaId");
            return View(vuelo);
        }

        // POST: VueloController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Vuelo m_vuelo)
        {
            if (ModelState.IsValid)
            {
                m_vuelo.EstadoVuelo = 1;
                _context.Vuelos.Update(m_vuelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAvion"] = new SelectList(_context.Avions, "IdAvion", "Siglas");
            ViewData["IdPiloto"] = new SelectList(_context.Pilotos, "IdPiloto", "PersonaId");
            return View(m_vuelo);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            var vuelo = _context.Vuelos.Find(id);
            if (vuelo == null)
            {
                return NotFound();
            }
            vuelo.EstadoVuelo = 0;
            _context.Vuelos.Update(vuelo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
