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
    public class SubscriptionPlanMastersController : Controller
    {
        private readonly PostgresContext _context;

        public SubscriptionPlanMastersController(PostgresContext context)
        {
            _context = context;
        }

        // GET: SubscriptionPlanMasters
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblInnoSubscriptionPlanMasters.ToListAsync());
        }

        // GET: SubscriptionPlanMasters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoSubscriptionPlanMaster = await _context.TblInnoSubscriptionPlanMasters
                .FirstOrDefaultAsync(m => m.PlanId == id);
            if (tblInnoSubscriptionPlanMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoSubscriptionPlanMaster);
        }

        // GET: SubscriptionPlanMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubscriptionPlanMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanId,PlanName,Price,BillingCycle,MaxBillsPerDay,MaxItems,WhatsappEnabled,MultiShopEnabled,IsActive,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoSubscriptionPlanMaster tblInnoSubscriptionPlanMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInnoSubscriptionPlanMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblInnoSubscriptionPlanMaster);
        }

        // GET: SubscriptionPlanMasters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoSubscriptionPlanMaster = await _context.TblInnoSubscriptionPlanMasters.FindAsync(id);
            if (tblInnoSubscriptionPlanMaster == null)
            {
                return NotFound();
            }
            return View(tblInnoSubscriptionPlanMaster);
        }

        // POST: SubscriptionPlanMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PlanId,PlanName,Price,BillingCycle,MaxBillsPerDay,MaxItems,WhatsappEnabled,MultiShopEnabled,IsActive,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoSubscriptionPlanMaster tblInnoSubscriptionPlanMaster)
        {
            if (id != tblInnoSubscriptionPlanMaster.PlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInnoSubscriptionPlanMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblInnoSubscriptionPlanMasterExists(tblInnoSubscriptionPlanMaster.PlanId))
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
            return View(tblInnoSubscriptionPlanMaster);
        }

        // GET: SubscriptionPlanMasters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoSubscriptionPlanMaster = await _context.TblInnoSubscriptionPlanMasters
                .FirstOrDefaultAsync(m => m.PlanId == id);
            if (tblInnoSubscriptionPlanMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoSubscriptionPlanMaster);
        }

        // POST: SubscriptionPlanMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblInnoSubscriptionPlanMaster = await _context.TblInnoSubscriptionPlanMasters.FindAsync(id);
            if (tblInnoSubscriptionPlanMaster != null)
            {
                _context.TblInnoSubscriptionPlanMasters.Remove(tblInnoSubscriptionPlanMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblInnoSubscriptionPlanMasterExists(long id)
        {
            return _context.TblInnoSubscriptionPlanMasters.Any(e => e.PlanId == id);
        }
    }
}
