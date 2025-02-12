using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FireTruck.Data;
using FireTruck.Models;

namespace FireTruck.Controllers
{
    public class FireStaitonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FireStaitonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FireStaitons
        public async Task<IActionResult> Index()
        {
            return View(await _context.FireStaiton.ToListAsync());
        }

        // GET: FireStaitons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fireStaiton = await _context.FireStaiton
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fireStaiton == null)
            {
                return NotFound();
            }

            return View(fireStaiton);
        }

        // GET: FireStaitons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FireStaitons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Model")] FireStaiton fireStaiton)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fireStaiton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fireStaiton);
        }

        // GET: FireStaitons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fireStaiton = await _context.FireStaiton.FindAsync(id);
            if (fireStaiton == null)
            {
                return NotFound();
            }
            return View(fireStaiton);
        }

        // POST: FireStaitons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model")] FireStaiton fireStaiton)
        {
            if (id != fireStaiton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fireStaiton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FireStaitonExists(fireStaiton.Id))
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
            return View(fireStaiton);
        }

        // GET: FireStaitons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fireStaiton = await _context.FireStaiton
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fireStaiton == null)
            {
                return NotFound();
            }

            return View(fireStaiton);
        }

        // POST: FireStaitons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fireStaiton = await _context.FireStaiton.FindAsync(id);
            if (fireStaiton != null)
            {
                _context.FireStaiton.Remove(fireStaiton);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FireStaitonExists(int id)
        {
            return _context.FireStaiton.Any(e => e.Id == id);
        }
    }
}
