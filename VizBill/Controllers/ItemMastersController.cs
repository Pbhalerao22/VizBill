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
    public class ItemMastersController : Controller
    {
        private readonly PostgresContext _context;

        public ItemMastersController(PostgresContext context)
        {
            _context = context;
        }

        // GET: ItemMasters
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.TblInnoItemMasters.Include(t => t.Category).Include(t => t.Shop);
            return View(await postgresContext.ToListAsync());
        }

        // GET: ItemMasters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoItemMaster = await _context.TblInnoItemMasters
                .Include(t => t.Category)
                .Include(t => t.Shop)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (tblInnoItemMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoItemMaster);
        }

        // GET: ItemMasters/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.TblInnoCategoryMasters, "CategoryId", "CategoryId");
            //ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId");

            ViewBag.CategoryDrop = _context.TblInnoCategoryMasters.Where(w=>w.CreatedBy==1).Select(s => new { s.CategoryId, s.CategoryName }).ToList();

            return View();
        }

        // POST: ItemMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,CategoryId,ItemName,Price")] TblInnoItemMaster tblInnoItemMaster)
        {
            ModelState.Remove("Key");
            if (ModelState.IsValid)
            {
                tblInnoItemMaster.IsActive = true;
                tblInnoItemMaster.CreatedOn = DateTime.Now;
                tblInnoItemMaster.CreatedBy = 1;
                tblInnoItemMaster.ShopId = 2;
                _context.Add(tblInnoItemMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction("Item","Main");
                //return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.TblInnoCategoryMasters, "CategoryId", "CategoryId", tblInnoItemMaster.CategoryId);
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId", tblInnoItemMaster.ShopId);
            return View(tblInnoItemMaster);
        }

        // GET: ItemMasters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoItemMaster = await _context.TblInnoItemMasters.FindAsync(id);
            if (tblInnoItemMaster == null)
            {
                return NotFound();
            }
            //ViewData["CategoryId"] = new SelectList(_context.TblInnoCategoryMasters, "CategoryId", "CategoryId", tblInnoItemMaster.CategoryId);
            //ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId", tblInnoItemMaster.ShopId);

            ViewBag.CategoryDrop = _context.TblInnoCategoryMasters.Where(w => w.CreatedBy == 1).Select(s => new { s.CategoryId, s.CategoryName }).ToList();
            return View(tblInnoItemMaster);
        }

        // POST: ItemMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ItemId,CategoryId,ItemName,Price,IsActive,IsDeleted")] TblInnoItemMaster tblInnoItemMaster)
        {
            //if (id != tblInnoItemMaster.ItemId)
            //{
            //    return NotFound();
            //}
            ModelState.Remove("Key");
            if (ModelState.IsValid)
            {
                try
                {
                    tblInnoItemMaster.ModifiedOn = DateTime.Now;
                    tblInnoItemMaster.ModifiedBy = 1;
                    tblInnoItemMaster.ShopId=2;
                    _context.Update(tblInnoItemMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblInnoItemMasterExists(tblInnoItemMaster.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Item","Main");
            }
            ViewData["CategoryId"] = new SelectList(_context.TblInnoCategoryMasters, "CategoryId", "CategoryId", tblInnoItemMaster.CategoryId);
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId", tblInnoItemMaster.ShopId);
            return View(tblInnoItemMaster);
        }

        // GET: ItemMasters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoItemMaster = await _context.TblInnoItemMasters
                .Include(t => t.Category)
                .Include(t => t.Shop)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (tblInnoItemMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoItemMaster);
        }

        // POST: ItemMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblInnoItemMaster = await _context.TblInnoItemMasters.FindAsync(id);
            if (tblInnoItemMaster != null)
            {
                _context.TblInnoItemMasters.Remove(tblInnoItemMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblInnoItemMasterExists(long id)
        {
            return _context.TblInnoItemMasters.Any(e => e.ItemId == id);
        }
    }
}
