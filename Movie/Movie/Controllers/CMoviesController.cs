#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie.Data;
using Movie.Models;

namespace Movie.Controllers
{
    public class CMoviesController : Controller
    {
        private readonly MovieContext _context;

        public CMoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: CMovies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        }

        // GET: CMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMovies = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMovies == null)
            {
                return NotFound();
            }

            return View(cMovies);
        }

        // GET: CMovies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] CMovies cMovies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cMovies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cMovies);
        }

        // GET: CMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMovies = await _context.Movie.FindAsync(id);
            if (cMovies == null)
            {
                return NotFound();
            }
            return View(cMovies);
        }

        // POST: CMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] CMovies cMovies)
        {
            if (id != cMovies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cMovies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CMoviesExists(cMovies.Id))
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
            return View(cMovies);
        }

        // GET: CMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMovies = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMovies == null)
            {
                return NotFound();
            }

            return View(cMovies);
        }

        // POST: CMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cMovies = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(cMovies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CMoviesExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
