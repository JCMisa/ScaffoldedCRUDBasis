using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MainPortfolio.Data;
using MainPortfolio.Models;
using Microsoft.AspNetCore.Authorization;

namespace MainPortfolio.Controllers
{
    [Authorize]
    public class JoinController : Controller
    {
        private readonly JoinCommunityDbContext _context;

        public JoinController(JoinCommunityDbContext context)
        {
            _context = context;
        }

        // GET: Join
        public async Task<IActionResult> Index()
        {
              return _context.Member != null ? 
                          View(await _context.Member.ToListAsync()) :
                          Problem("Entity set 'JoinCommunityDbContext.Member'  is null.");
        }

        // GET: Join/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var joinCommunityModel = await _context.Member
                .FirstOrDefaultAsync(m => m.ID == id);
            if (joinCommunityModel == null)
            {
                return NotFound();
            }

            return View(joinCommunityModel);
        }

        // GET: Join/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Join/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Mobile,Email")] JoinCommunityModel joinCommunityModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(joinCommunityModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(joinCommunityModel);
        }

        // GET: Join/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var joinCommunityModel = await _context.Member.FindAsync(id);
            if (joinCommunityModel == null)
            {
                return NotFound();
            }
            return View(joinCommunityModel);
        }

        // POST: Join/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Mobile,Email")] JoinCommunityModel joinCommunityModel)
        {
            if (id != joinCommunityModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(joinCommunityModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JoinCommunityModelExists(joinCommunityModel.ID))
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
            return View(joinCommunityModel);
        }

        // GET: Join/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var joinCommunityModel = await _context.Member
                .FirstOrDefaultAsync(m => m.ID == id);
            if (joinCommunityModel == null)
            {
                return NotFound();
            }

            return View(joinCommunityModel);
        }

        // POST: Join/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Member == null)
            {
                return Problem("Entity set 'JoinCommunityDbContext.Member'  is null.");
            }
            var joinCommunityModel = await _context.Member.FindAsync(id);
            if (joinCommunityModel != null)
            {
                _context.Member.Remove(joinCommunityModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JoinCommunityModelExists(int id)
        {
          return (_context.Member?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
