using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetoTP3_A2.Data;
using projetoTP3_A2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetoTP3_A2.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class FarmaciaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ViaCepService _viaCepService; // <-- adiciona o serviço


        public FarmaciaController(ApplicationDbContext context, ViaCepService viaCepService)
        {
            _context = context;
            _viaCepService = viaCepService; // <-- injeta aqui
        }

        // GET: Farmacia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Farmacia.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> BuscarEndereco(string cep)
        {
            try
            {
                var endereco = await _viaCepService.ConsultarCepAsync(cep);
                if (endereco.Erro)
                    return NotFound("CEP não encontrado.");

                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Farmacia/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacia = await _context.Farmacia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmacia == null)
            {
                return NotFound();
            }

            return View(farmacia);
        }

        // GET: Farmacia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Farmacia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Endereco")] Farmacia farmacia)
        {
            if (ModelState.IsValid)
            {
                farmacia.Id = Guid.NewGuid();
                _context.Add(farmacia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(farmacia);
        }

        // GET: Farmacia/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacia = await _context.Farmacia.FindAsync(id);
            if (farmacia == null)
            {
                return NotFound();
            }
            return View(farmacia);
        }

        // POST: Farmacia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Endereco")] Farmacia farmacia)
        {
            if (id != farmacia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmacia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmaciaExists(farmacia.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(farmacia);
        }

        // GET: Farmacia/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacia = await _context.Farmacia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmacia == null)
            {
                return NotFound();
            }

            return View(farmacia);
        }

        // POST: Farmacia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var farmacia = await _context.Farmacia.FindAsync(id);
            if (farmacia != null)
            {
                _context.Farmacia.Remove(farmacia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmaciaExists(Guid id)
        {
            return _context.Farmacia.Any(e => e.Id == id);
        }
    }
}
