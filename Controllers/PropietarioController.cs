using AeroSpace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace AeroSpace.Controllers
{
    [Authorize]
    public class PropietarioController : Controller
    {
        private readonly AeroSpaceContext _context;

        public PropietarioController(AeroSpaceContext context)
        {
            _context = context;
        }
        // GET: PropietarioController
        public ActionResult Index()
        {
            var propietario = _context.Propietarios.Where(x => x.EstadoPropietario == 1).Include(p => p.Persona).ToList();
            return View(propietario);
        }

        // GET: PropietarioController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var propietarios = _context.Propietarios.Find(id);
            ViewData["IdPersona"] = new SelectList(_context.Personas, "IdPersona", "NombrePersona");
            return View(propietarios);
        }

        // GET: PropietarioController/Create
        public ActionResult Create()
        {
            var personas = _context.Personas.Select(x => new
            {
                Id = x.IdPersona,
                CedulaNombre = string.Concat(x.CedulaPersona, " | ", x.NombrePersona),
                EstadoPers = x.EstadoPersona
            }).ToList();

            ViewData["IdPersona"] = new SelectList(personas.Where(e => e.EstadoPers == 1), "Id", "CedulaNombre");
            return View();
        }

        // POST: PropietarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Propietario propietario)
        {
            var partialCode = _context.Personas;
            
            if (ModelState.IsValid)
            {
                var newPropietario = new Propietario()
                {

                    Rif = "RIF-" + propietario.PersonaId.GetHashCode(),
                    PersonaId = propietario.PersonaId
                };
                _context.Add(newPropietario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(_context.Personas, "IdPersona", "NombrePersona", propietario.PersonaId);
            return View();
        }

        // GET: PropietarioController/Edit/5
        public ActionResult Edit(int? id)
        {
            var propietarios = _context.Propietarios.Find(id);

            var personas = _context.Personas.Select(x => new
            {
                Id = x.IdPersona,
                CedulaNombre = string.Concat(x.CedulaPersona, " | ", x.NombrePersona)
            }).ToList();

            ViewData["IdPersona"] = new SelectList(personas, "Id", "CedulaNombre");
            return View(propietarios);
        }

        // POST: PropietarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                propietario.Rif = "RIF-" + propietario.PersonaId;
                propietario.EstadoPropietario = 1;
                _context.Update(propietario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(_context.Personas, "IdPersona", "NombrePersona", propietario.PersonaId);
            return View();
        }

        public async Task<ActionResult> Delete(int? id)
        {
            var propietario = _context.Propietarios.Find(id);
            if (propietario == null)
            {
                return NotFound();
            }

            propietario.EstadoPropietario = 0;
            _context.Propietarios.Update(propietario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
