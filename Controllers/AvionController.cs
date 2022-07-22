using AeroSpace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AeroSpace.Controllers
{
    [Authorize]
    public class AvionController : Controller
    {
        private readonly AeroSpaceContext _context;

        public AvionController(AeroSpaceContext context)
        {
            _context = context;
        }
        // GET: AvionController
        public async Task<ActionResult> Index()
        {
            //var aviones = await _context.Avions.Where(h => h.EstadoAvion == 1).Include(pr => pr.Persona).Include(p => p.Propietario).ToListAsync();
            var aviones = await _context.Avions.Where(h => h.EstadoAvion == 1).Include(p => p.Propietario).ToListAsync();
            return View(aviones);
        }

        // GET: AvionController/Details/5
        public ActionResult Details(int? id)
        {
            var avion = _context.Avions.Find(id);
            if (avion == null)
            {
                return NotFound();
            }
            ViewData["IdPropietario"] = new SelectList(_context.Propietarios, "IdPropietario", "PersonaId");
            ViewData["IdPersona"] = new SelectList(_context.Personas, "IdPersona", "NombrePersona");
            return View(avion);
        }

        // GET: AvionController/Create
        public ActionResult Create()
        {
            var propietarioPersona = _context.Personas.Select(x => new
            {
                Id = x.IdPersona,
                NombreCompleto = _context.Personas.Join(_context.Propietarios, per => per.IdPersona, pro => pro.PersonaId, (per, pro) => per.NombrePersona).ToList()
            }).ToList();

            //ViewData["IdPropietario"] = new SelectList(propietarioPersona, _context.Propietarios.Join(_context.Personas, pro => pro.PersonaId, per => per.IdPersona, (per, pro) => pro.NombrePersona )).ToList();
            ViewData["IdPropietario"] = new SelectList(_context.Propietarios, "IdPropietario", "PersonaId");
            return View();
        }

        // POST: AvionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Avion avion)
        {
            if (ModelState.IsValid)
            {
                var mAvion = new Avion()
                {
                    Siglas = avion.Siglas,
                    Capacidad = avion.Capacidad,
                    TipoAvion = avion.TipoAvion,
                    PropietarioId = avion.PropietarioId
                };
                _context.Add(mAvion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPropietario"] = new SelectList(_context.Propietarios, "IdPropietario", "PersonaId");
            return View(avion);
        }

        // GET: AvionController/Edit/5
        public ActionResult Edit(int? id)
        {
            var avion = _context.Avions.Find(id);
            ViewData["IdPropietario"] = new SelectList(_context.Propietarios, "IdPropietario", "PersonaId");
            return View(avion);
        }

        // POST: AvionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Avion mAvion)
        {
            if (ModelState.IsValid)
            {
                mAvion.EstadoAvion = 1;
                _context.Avions.Update(mAvion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPropietario"] = new SelectList(_context.Propietarios, "IdPropietario", "PersonaId");
            return View(mAvion);
        }

        // GET: AvionController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var avion = _context.Avions.Find(id);
            if (avion == null)
            {
                return NotFound();
            }

            avion.EstadoAvion = 0;
            _context.Avions.Update(avion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
