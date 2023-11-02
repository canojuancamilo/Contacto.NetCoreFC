using CrudNet7MVC.Datos;
using CrudNet7MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CrudNet7MVC.Controllers
{
    public class InicioController : Controller
    {
        private readonly ApplicationDBContext _contexto;

        public InicioController(ApplicationDBContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Contactos.ToArrayAsync());
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Contactos.Add(contacto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(contacto);
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var contacto = _contexto.Contactos.Find(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Contactos.Update(contacto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(contacto);
        }

        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var contacto = _contexto.Contactos.Find(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpGet]
        public IActionResult Eliminar(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var contacto = _contexto.Contactos.Find(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarContacto(int id)
        {
            var contacto = await _contexto.Contactos.FindAsync(id);

            if (contacto == null)
                return NotFound();


            _contexto.Contactos.Remove(contacto);
            await _contexto.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}