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
    public class ShopMastersController : Controller
    {
        private readonly PostgresContext _context;

        public ShopMastersController(PostgresContext context)
        {
            _context = context;
        }

        // GET: TblInnoShopMasters
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.TblInnoShopMasters.Include(t => t.OwnerUser);
            return View(await postgresContext.ToListAsync());
        }

        // GET: TblInnoShopMasters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoShopMaster = await _context.TblInnoShopMasters
                .Include(t => t.OwnerUser)
                .FirstOrDefaultAsync(m => m.ShopId == id);
            if (tblInnoShopMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoShopMaster);
        }

        // GET: TblInnoShopMasters/Create
        public IActionResult Create()
        {
            ViewData["OwnerUserId"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId");
            return View();
        }

        // POST: TblInnoShopMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShopId,OwnerUserId,ShopName,ShopCategory,WhatsappNumber,AdvertisementMsg,CompanyLogo,IsActive,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoShopMaster tblInnoShopMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInnoShopMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerUserId"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId", tblInnoShopMaster.OwnerUserId);
            return View(tblInnoShopMaster);
        }

        // GET: TblInnoShopMasters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoShopMaster = await _context.TblInnoShopMasters.FindAsync(id);
            if (tblInnoShopMaster == null)
            {
                return NotFound();
            }
            ViewData["OwnerUserId"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId", tblInnoShopMaster.OwnerUserId);
            return View(tblInnoShopMaster);
        }

        // POST: TblInnoShopMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ShopId,OwnerUserId,ShopName,ShopCategory,WhatsappNumber,AdvertisementMsg,CompanyLogo,IsActive,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoShopMaster tblInnoShopMaster)
        {
            if (id != tblInnoShopMaster.ShopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInnoShopMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblInnoShopMasterExists(tblInnoShopMaster.ShopId))
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
            ViewData["OwnerUserId"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId", tblInnoShopMaster.OwnerUserId);
            return View(tblInnoShopMaster);
        }

        // GET: TblInnoShopMasters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoShopMaster = await _context.TblInnoShopMasters
                .Include(t => t.OwnerUser)
                .FirstOrDefaultAsync(m => m.ShopId == id);
            if (tblInnoShopMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoShopMaster);
        }

        // POST: TblInnoShopMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblInnoShopMaster = await _context.TblInnoShopMasters.FindAsync(id);
            if (tblInnoShopMaster != null)
            {
                _context.TblInnoShopMasters.Remove(tblInnoShopMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblInnoShopMasterExists(long id)
        {
            return _context.TblInnoShopMasters.Any(e => e.ShopId == id);
        }
    }
}
