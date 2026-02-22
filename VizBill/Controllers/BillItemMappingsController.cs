using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VizBill.MasterDbContext;

namespace VizBill.Controllers
{
    [Authorize]
    public class BillItemMappingsController : Controller
    {
        private readonly PostgresContext _context;

        public BillItemMappingsController(PostgresContext context)
        {
            _context = context;
        }

        // GET: BillItemMappings
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.TblInnoBillItemMappings.Include(t => t.Bill).Include(t => t.Item);
            return View(await postgresContext.ToListAsync());
        }

        // GET: BillItemMappings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoBillItemMapping = await _context.TblInnoBillItemMappings
                .Include(t => t.Bill)
                .Include(t => t.Item)
                .FirstOrDefaultAsync(m => m.BillItemId == id);
            if (tblInnoBillItemMapping == null)
            {
                return NotFound();
            }

            return View(tblInnoBillItemMapping);
        }

        // GET: BillItemMappings/Create
        public IActionResult Create()
        {
            ViewData["BillId"] = new SelectList(_context.TblInnoBillMasters, "BillId", "BillId");
            ViewData["ItemId"] = new SelectList(_context.TblInnoItemMasters, "ItemId", "ItemId");
            return View();
        }

        // POST: BillItemMappings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillItemId,BillId,ItemId,ItemName,Quantity,Price,Total,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoBillItemMapping tblInnoBillItemMapping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInnoBillItemMapping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillId"] = new SelectList(_context.TblInnoBillMasters, "BillId", "BillId", tblInnoBillItemMapping.BillId);
            ViewData["ItemId"] = new SelectList(_context.TblInnoItemMasters, "ItemId", "ItemId", tblInnoBillItemMapping.ItemId);
            return View(tblInnoBillItemMapping);
        }

        // GET: BillItemMappings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoBillItemMapping = await _context.TblInnoBillItemMappings.FindAsync(id);
            if (tblInnoBillItemMapping == null)
            {
                return NotFound();
            }
            ViewData["BillId"] = new SelectList(_context.TblInnoBillMasters, "BillId", "BillId", tblInnoBillItemMapping.BillId);
            ViewData["ItemId"] = new SelectList(_context.TblInnoItemMasters, "ItemId", "ItemId", tblInnoBillItemMapping.ItemId);
            return View(tblInnoBillItemMapping);
        }

        // POST: BillItemMappings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("BillItemId,BillId,ItemId,ItemName,Quantity,Price,Total,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoBillItemMapping tblInnoBillItemMapping)
        {
            if (id != tblInnoBillItemMapping.BillItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInnoBillItemMapping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblInnoBillItemMappingExists(tblInnoBillItemMapping.BillItemId))
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
            ViewData["BillId"] = new SelectList(_context.TblInnoBillMasters, "BillId", "BillId", tblInnoBillItemMapping.BillId);
            ViewData["ItemId"] = new SelectList(_context.TblInnoItemMasters, "ItemId", "ItemId", tblInnoBillItemMapping.ItemId);
            return View(tblInnoBillItemMapping);
        }

        // GET: BillItemMappings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoBillItemMapping = await _context.TblInnoBillItemMappings
                .Include(t => t.Bill)
                .Include(t => t.Item)
                .FirstOrDefaultAsync(m => m.BillItemId == id);
            if (tblInnoBillItemMapping == null)
            {
                return NotFound();
            }

            return View(tblInnoBillItemMapping);
        }

        // POST: BillItemMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblInnoBillItemMapping = await _context.TblInnoBillItemMappings.FindAsync(id);
            if (tblInnoBillItemMapping != null)
            {
                _context.TblInnoBillItemMappings.Remove(tblInnoBillItemMapping);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblInnoBillItemMappingExists(long id)
        {
            return _context.TblInnoBillItemMappings.Any(e => e.BillItemId == id);
        }
    }
}
