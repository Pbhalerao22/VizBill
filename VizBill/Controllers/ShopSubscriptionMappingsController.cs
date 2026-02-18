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
    public class ShopSubscriptionMappingsController : Controller
    {
        private readonly PostgresContext _context;

        public ShopSubscriptionMappingsController(PostgresContext context)
        {
            _context = context;
        }

        // GET: ShopSubscriptionMappings
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.TblInnoShopSubscriptionMappings.Include(t => t.ApprovedByNavigation).Include(t => t.Plan).Include(t => t.Shop);
            return View(await postgresContext.ToListAsync());
        }

        // GET: ShopSubscriptionMappings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoShopSubscriptionMapping = await _context.TblInnoShopSubscriptionMappings
                .Include(t => t.ApprovedByNavigation)
                .Include(t => t.Plan)
                .Include(t => t.Shop)
                .FirstOrDefaultAsync(m => m.SubscriptionId == id);
            if (tblInnoShopSubscriptionMapping == null)
            {
                return NotFound();
            }

            return View(tblInnoShopSubscriptionMapping);
        }

        // GET: ShopSubscriptionMappings/Create
        public IActionResult Create()
        {
            ViewData["ApprovedBy"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId");
            ViewData["PlanId"] = new SelectList(_context.TblInnoSubscriptionPlanMasters, "PlanId", "PlanId");
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId");
            return View();
        }

        // POST: ShopSubscriptionMappings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubscriptionId,ShopId,PlanId,Status,StartDate,EndDate,IsTrial,ApprovedBy,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoShopSubscriptionMapping tblInnoShopSubscriptionMapping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInnoShopSubscriptionMapping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApprovedBy"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId", tblInnoShopSubscriptionMapping.ApprovedBy);
            ViewData["PlanId"] = new SelectList(_context.TblInnoSubscriptionPlanMasters, "PlanId", "PlanId", tblInnoShopSubscriptionMapping.PlanId);
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId", tblInnoShopSubscriptionMapping.ShopId);
            return View(tblInnoShopSubscriptionMapping);
        }

        // GET: ShopSubscriptionMappings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoShopSubscriptionMapping = await _context.TblInnoShopSubscriptionMappings.FindAsync(id);
            if (tblInnoShopSubscriptionMapping == null)
            {
                return NotFound();
            }
            ViewData["ApprovedBy"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId", tblInnoShopSubscriptionMapping.ApprovedBy);
            ViewData["PlanId"] = new SelectList(_context.TblInnoSubscriptionPlanMasters, "PlanId", "PlanId", tblInnoShopSubscriptionMapping.PlanId);
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId", tblInnoShopSubscriptionMapping.ShopId);
            return View(tblInnoShopSubscriptionMapping);
        }

        // POST: ShopSubscriptionMappings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SubscriptionId,ShopId,PlanId,Status,StartDate,EndDate,IsTrial,ApprovedBy,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoShopSubscriptionMapping tblInnoShopSubscriptionMapping)
        {
            if (id != tblInnoShopSubscriptionMapping.SubscriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInnoShopSubscriptionMapping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblInnoShopSubscriptionMappingExists(tblInnoShopSubscriptionMapping.SubscriptionId))
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
            ViewData["ApprovedBy"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId", tblInnoShopSubscriptionMapping.ApprovedBy);
            ViewData["PlanId"] = new SelectList(_context.TblInnoSubscriptionPlanMasters, "PlanId", "PlanId", tblInnoShopSubscriptionMapping.PlanId);
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId", tblInnoShopSubscriptionMapping.ShopId);
            return View(tblInnoShopSubscriptionMapping);
        }

        // GET: ShopSubscriptionMappings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoShopSubscriptionMapping = await _context.TblInnoShopSubscriptionMappings
                .Include(t => t.ApprovedByNavigation)
                .Include(t => t.Plan)
                .Include(t => t.Shop)
                .FirstOrDefaultAsync(m => m.SubscriptionId == id);
            if (tblInnoShopSubscriptionMapping == null)
            {
                return NotFound();
            }

            return View(tblInnoShopSubscriptionMapping);
        }

        // POST: ShopSubscriptionMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblInnoShopSubscriptionMapping = await _context.TblInnoShopSubscriptionMappings.FindAsync(id);
            if (tblInnoShopSubscriptionMapping != null)
            {
                _context.TblInnoShopSubscriptionMappings.Remove(tblInnoShopSubscriptionMapping);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblInnoShopSubscriptionMappingExists(long id)
        {
            return _context.TblInnoShopSubscriptionMappings.Any(e => e.SubscriptionId == id);
        }
    }
}
