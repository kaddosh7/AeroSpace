using AeroSpace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace AeroSpace.Controllers
{
    [Authorize]
    public class EstadiumController : Controller
    {
        private readonly AeroSpaceContext _context;

        public EstadiumController(AeroSpaceContext context)
        {
            _context = context;
        }
        // GET: EstadiumController
        public async Task<ActionResult> Index()
        {
            var estadias = await _context.Estadia.Where(x => x.EstadoEstadia == 1).Include(a => a.Avion).Include(h => h.Hangar).ToListAsync();
            ViewData["MontoEstadia"] = estadias.Sum(x => x.MontoEtadia);
            return View(estadias);
        }

        // GET: EstadiumController/Details/5
        public ActionResult Details(int? id)
        {
            return View();
        }

        // GET: EstadiumController/Create
        public ActionResult Create()
        {
            var costoHangar = _context.Hangars.Select(x => new
            {
                Id = x.IdHangar,
                HangarCapacity = string.Concat(x.CapacidadHangar + " | " + x.CostoHora)
            });

            ViewData["IdAvion"] = new SelectList(_context.Avions, "IdAvion", "Siglas");
            ViewData["IdHangar"] = new SelectList(costoHangar, "Id", "HangarCapacity");
            ViewData["CostoHangar"] = new SelectList(_context.Hangars, "IdHangar", "CostoHora");
            return View();
        }

        // POST: EstadiumController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Estadium m_estadium)
        {
            if (ModelState.IsValid)
            {
                var estadia = new Estadium()
                {
                    FechaEntrada = m_estadium.FechaEntrada,
                    FechaSalida = m_estadium.FechaSalida,
                    AvionId = m_estadium.AvionId,
                    HangarId = m_estadium.HangarId,
                    MontoEtadia = m_estadium.MontoEtadia
                };
                _context.Estadia.Add(estadia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var costoHangar = _context.Hangars.Select(x => new
            {
                Id = x.IdHangar,
                HangarCapacity = string.Concat(x.CapacidadHangar + " | " + x.CostoHora)
            });

            ViewData["IdAvion"] = new SelectList(_context.Avions, "IdAvion", "Siglas");
            ViewData["IdHangar"] = new SelectList(costoHangar, "Id", "HangarCapacity");
            ViewData["CostoHangar"] = new SelectList(_context.Hangars, "IdHangar", "CostoHora");
            return View();
        }

        // GET: EstadiumController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EstadiumController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
