using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_TIPI_LEARNING.Models;
using Microsoft.AspNetCore.Hosting;
using E_TIPI_LEARNING.Utilities;

namespace E_TIPI_LEARNING.Controllers
{
    public class LearningRessourcesController : Controller
    {
        private readonly LearningContext _context;
        private readonly IHostingEnvironment _env;

        public LearningRessourcesController(LearningContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: LearningRessources
        public async Task<IActionResult> Index()
        {
            return View(await _context.LearningRessources.ToListAsync());
        }

        public async Task<IActionResult> Export()
        {
            var learningRessources = await _context.LearningRessources.ToListAsync();
            var memoryStream = await Excel.Export(learningRessources, _env);
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", @"E-TIPI.xlsx");
        }

        // GET: LearningRessources/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningRessource = await _context.LearningRessources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (learningRessource == null)
            {
                return NotFound();
            }

            return View(learningRessource);
        }

        // GET: LearningRessources/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LearningRessources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,StartDate,EndDate")] LearningRessource learningRessource)
        {
            if (ModelState.IsValid)
            {
                _context.Add(learningRessource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(learningRessource);
        }

        // GET: LearningRessources/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningRessource = await _context.LearningRessources.FindAsync(id);
            if (learningRessource == null)
            {
                return NotFound();
            }
            return View(learningRessource);
        }

        // POST: LearningRessources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Description,StartDate,EndDate")] LearningRessource learningRessource)
        {
            if (id != learningRessource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(learningRessource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearningRessourceExists(learningRessource.Id))
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
            return View(learningRessource);
        }

        // GET: LearningRessources/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningRessource = await _context.LearningRessources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (learningRessource == null)
            {
                return NotFound();
            }

            return View(learningRessource);
        }

        // POST: LearningRessources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var learningRessource = await _context.LearningRessources.FindAsync(id);
            _context.LearningRessources.Remove(learningRessource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearningRessourceExists(string id)
        {
            return _context.LearningRessources.Any(e => e.Id == id);
        }
    }
}
