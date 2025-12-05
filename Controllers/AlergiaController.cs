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
    public class AlergiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlergiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alergia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alergia.ToListAsync());
        }

        // GET: Alergia/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alergia = await _context.Alergia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alergia == null)
            {
                return NotFound();
            }

            return View(alergia);
        }

        // GET: Alergia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alergia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Reacao")] Alergia alergia)
        {
            if (ModelState.IsValid)
            {
                alergia.Id = Guid.NewGuid();
                _context.Add(alergia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alergia);
        }

        // GET: Alergia/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alergia = await _context.Alergia.FindAsync(id);
            if (alergia == null)
            {
                return NotFound();
            }
            return View(alergia);
        }

        // POST: Alergia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Reacao")] Alergia alergia)
        {
            if (id != alergia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alergia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlergiaExists(alergia.Id))
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
            return View(alergia);
        }

        // GET: Alergia/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alergia = await _context.Alergia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alergia == null)
            {
                return NotFound();
            }

            return View(alergia);
        }

        // POST: Alergia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var alergia = await _context.Alergia.FindAsync(id);
            if (alergia != null)
            {
                _context.Alergia.Remove(alergia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlergiaExists(Guid id)
        {
            return _context.Alergia.Any(e => e.Id == id);
        }
    }
}
