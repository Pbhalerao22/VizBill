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
    public class UserShopMappingsController : Controller
    {
        private readonly PostgresContext _context;

        public UserShopMappingsController(PostgresContext context)
        {
            _context = context;
        }

        // GET: UserShopMappings
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.TblInnoUserShopMappings.Include(t => t.Shop).Include(t => t.User);
            return View(await postgresContext.ToListAsync());
        }

        // GET: UserShopMappings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoUserShopMapping = await _context.TblInnoUserShopMappings
                .Include(t => t.Shop)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.UserShopId == id);
            if (tblInnoUserShopMapping == null)
            {
                return NotFound();
            }

            return View(tblInnoUserShopMapping);
        }

        // GET: UserShopMappings/Create
        public IActionResult Create()
        {
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId");
            ViewData["UserId"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId");
            return View();
        }

        // POST: UserShopMappings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserShopId,UserId,ShopId,IsActive,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoUserShopMapping tblInnoUserShopMapping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInnoUserShopMapping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId", tblInnoUserShopMapping.ShopId);
            ViewData["UserId"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId", tblInnoUserShopMapping.UserId);
            return View(tblInnoUserShopMapping);
        }

        // GET: UserShopMappings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoUserShopMapping = await _context.TblInnoUserShopMappings.FindAsync(id);
            if (tblInnoUserShopMapping == null)
            {
                return NotFound();
            }
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId", tblInnoUserShopMapping.ShopId);
            ViewData["UserId"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId", tblInnoUserShopMapping.UserId);
            return View(tblInnoUserShopMapping);
        }

        // POST: UserShopMappings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("UserShopId,UserId,ShopId,IsActive,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoUserShopMapping tblInnoUserShopMapping)
        {
            if (id != tblInnoUserShopMapping.UserShopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInnoUserShopMapping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblInnoUserShopMappingExists(tblInnoUserShopMapping.UserShopId))
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
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId", tblInnoUserShopMapping.ShopId);
            ViewData["UserId"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId", tblInnoUserShopMapping.UserId);
            return View(tblInnoUserShopMapping);
        }

        // GET: UserShopMappings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoUserShopMapping = await _context.TblInnoUserShopMappings
                .Include(t => t.Shop)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.UserShopId == id);
            if (tblInnoUserShopMapping == null)
            {
                return NotFound();
            }

            return View(tblInnoUserShopMapping);
        }

        // POST: UserShopMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblInnoUserShopMapping = await _context.TblInnoUserShopMappings.FindAsync(id);
            if (tblInnoUserShopMapping != null)
            {
                _context.TblInnoUserShopMappings.Remove(tblInnoUserShopMapping);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblInnoUserShopMappingExists(long id)
        {
            return _context.TblInnoUserShopMappings.Any(e => e.UserShopId == id);
        }
    }
}
