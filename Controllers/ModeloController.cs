using AeroSpace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace AeroSpace.Controllers
{
    [Authorize]
    public class ModeloController : Controller
    {
        private readonly AeroSpaceContext _context;

        public ModeloController(AeroSpaceContext context)
        {
            _context = context;
        }

        // GET: ModeloController
        public async Task<ActionResult> Index()
        {
            var modelos = await _context.Modelos.Where(x => x.EstadoModelo == 1).Include(a => a.Avion).ToListAsync();
            return View(modelos);
        }

        // GET: ModeloController/Details/5
        public ActionResult Details(int? id)
        {
            var modelo = _context.Modelos.Find(id);
            ViewData["Siglas"] = new SelectList(_context.Avions, "IdAvion", "Siglas");
            return View(modelo);
        }

        // GET: ModeloController/Create
        public ActionResult Create()
        {
            ViewData["IdAvion"] = new SelectList(_context.Avions, "IdAvion", "Siglas");
            return View();
        }

        // POST: ModeloController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Modelo m_Modelo)
        {
            if (ModelState.IsValid)
            {
                var modelo = new Modelo()
                {
                    Descripcion = m_Modelo.Descripcion,
                    Propulsion = m_Modelo.Propulsion,
                    Motores = m_Modelo.Motores,
                    Peso = m_Modelo.Peso,
                    AvionId = m_Modelo.AvionId
                };
                _context.Add(modelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdAvion"] = new SelectList(_context.Avions, "IdAvion", "Siglas");
            return View();
        }

        // GET: ModeloController/Edit/5
        public ActionResult Edit(int? id)
        {
            var modelo = _context.Modelos.Find(id);
            ViewData["IdAvion"] = new SelectList(_context.Avions, "IdAvion", "Siglas");
            return View(modelo);
        }

        // POST: ModeloController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Modelo m_Modelo)
        {
            if (ModelState.IsValid)
            {
                m_Modelo.EstadoModelo = 1;
                _context.Update(m_Modelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdAvion"] = new SelectList(_context.Avions, "IdAvion", "Siglas");
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            var modelo = _context.Modelos.Find(id);
            if (modelo == null)
            {
                return NotFound();
            }

            modelo.EstadoModelo = 0;
            _context.Update(modelo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
