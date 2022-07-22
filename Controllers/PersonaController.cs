using AeroSpace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace AeroSpace.Controllers
{
    [Authorize]
    public class PersonaController : Controller
    {
        private readonly AeroSpaceContext _context;

        public PersonaController(AeroSpaceContext context)
        {
            _context = context;
        }

        // GET: Persona
        public async Task<ActionResult> Index()
        {
            return View(await _context.Personas.Where(x => x.EstadoPersona == 1).ToListAsync());
        }

        // GET: Persona/Details/5
        public ActionResult Details(int id)
        {
            var persona = _context.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // GET: Persona/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Persona/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Persona modelPersona)
        {
            if (ModelState.IsValid)
            {
                var persona = new Persona()
                {
                    NombrePersona = modelPersona.NombrePersona,
                    CedulaPersona = modelPersona.CedulaPersona
                };
                _context.Add(persona);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(modelPersona);
        }

        // GET: Persona/Edit/5
        public ActionResult Edit(int? id)
        {
            var persona = _context.Personas.Find(id);

            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Persona/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Persona m_persona)
        {
            if (ModelState.IsValid)
            {
                m_persona.EstadoPersona = 1;
                _context.Personas.Update(m_persona);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(m_persona);
        }

        public async Task<ActionResult> Delete(int? id, Persona m_persona)
        {
            var persona = _context.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            persona.EstadoPersona = 0;
            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
