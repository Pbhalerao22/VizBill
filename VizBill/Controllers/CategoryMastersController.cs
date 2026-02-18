using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VizBill.MasterDbContext;

namespace VizBill.Controllers
{
    public class CategoryMastersController : Controller
    {
        private readonly PostgresContext _context;

        public CategoryMastersController(PostgresContext context)
        {
            _context = context;
        }

        // GET: CategoryMasters
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblInnoCategoryMasters.ToListAsync());
        }

        // GET: CategoryMasters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoCategoryMaster = await _context.TblInnoCategoryMasters
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (tblInnoCategoryMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoCategoryMaster);
        }

        // GET: CategoryMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoCategoryMaster tblInnoCategoryMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInnoCategoryMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblInnoCategoryMaster);
        }

        // GET: CategoryMasters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoCategoryMaster = await _context.TblInnoCategoryMasters.FindAsync(id);
            if (tblInnoCategoryMaster == null)
            {
                return NotFound();
            }
            return View(tblInnoCategoryMaster);
        }

        // POST: CategoryMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CategoryId,CategoryName,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoCategoryMaster tblInnoCategoryMaster)
        {
            if (id != tblInnoCategoryMaster.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInnoCategoryMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblInnoCategoryMasterExists(tblInnoCategoryMaster.CategoryId))
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
            return View(tblInnoCategoryMaster);
        }

        // GET: CategoryMasters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoCategoryMaster = await _context.TblInnoCategoryMasters
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (tblInnoCategoryMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoCategoryMaster);
        }

        // POST: CategoryMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblInnoCategoryMaster = await _context.TblInnoCategoryMasters.FindAsync(id);
            if (tblInnoCategoryMaster != null)
            {
                _context.TblInnoCategoryMasters.Remove(tblInnoCategoryMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblInnoCategoryMasterExists(long id)
        {
            return _context.TblInnoCategoryMasters.Any(e => e.CategoryId == id);
        }
    }
}
