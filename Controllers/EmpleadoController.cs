using AeroSpace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace AeroSpace.Controllers
{
    [Authorize]
    public class EmpleadoController : Controller
    {
        private readonly AeroSpaceContext _context;

        public EmpleadoController(AeroSpaceContext context)
        {
            _context = context;
        }
        // GET: EmpleadoController
        public async Task<ActionResult> Index()
        {
            var empleado = await _context.Empleados.Where(x => x.EstadoEmpleado == 1).Include(e => e.Persona).ToListAsync();
            return View(empleado);
        }

        // GET: EmpleadoController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = _context.Empleados.Find(id);
            ViewData["IdPersona"] = new SelectList(_context.Personas, "IdPersona", "NombrePersona");
            return View(empleado);
        }

        // GET: EmpleadoController/Create
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

        // POST: EmpleadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Empleado modelEmpleado)
        {
            var personas = _context.Personas.Select(x => new
            {
                Id = x.IdPersona,
                CedulaNombre = string.Concat(x.CedulaPersona, " | ", x.NombrePersona)
            }).ToList();

            if (ModelState.IsValid)
            {
                var empleado = new Empleado()
                {
                    RolEmpleado = modelEmpleado.RolEmpleado,
                    Salario = modelEmpleado.Salario,
                    PersonaId = modelEmpleado.PersonaId
                };

                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdPersona"] = new SelectList(personas, "Id", "CedulaNombre", modelEmpleado.PersonaId);
            return View(modelEmpleado);
        }

        // GET: EmpleadoController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = _context.Empleados.Find(id);
            ViewData["IdPersona"] = new SelectList(_context.Personas, "IdPersona", "NombrePersona");
            return View(empleado);
        }

        // POST: EmpleadoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Empleado m_empleado)
        {
            if (ModelState.IsValid)
            {
                m_empleado.EstadoEmpleado = 1;
                _context.Empleados.Update(m_empleado);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(m_empleado);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            var empleado = _context.Empleados.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }
            
            empleado.EstadoEmpleado = 0;
            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
