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
    public class BillMastersController : Controller
    {
        private readonly PostgresContext _context;

        public BillMastersController(PostgresContext context)
        {
            _context = context;
        }

        // GET: TblInnoBillMasters
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.TblInnoBillMasters.Include(t => t.PaymentMode).Include(t => t.Shop);
            return View(await postgresContext.ToListAsync());
        }

        // GET: TblInnoBillMasters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoBillMaster = await _context.TblInnoBillMasters
                .Include(t => t.PaymentMode)
                .Include(t => t.Shop)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (tblInnoBillMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoBillMaster);
        }

        // GET: TblInnoBillMasters/Create
        public IActionResult Create()
        {
            ViewData["PaymentModeId"] = new SelectList(_context.TblInnoPaymentModeMasters, "PaymentModeId", "PaymentModeId");
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId");
            return View();
        }

        // POST: TblInnoBillMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillId,ShopId,BillNumber,CustomerMobile,TotalAmount,PaymentModeId,Notes,BillDate,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoBillMaster tblInnoBillMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInnoBillMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentModeId"] = new SelectList(_context.TblInnoPaymentModeMasters, "PaymentModeId", "PaymentModeId", tblInnoBillMaster.PaymentModeId);
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId", tblInnoBillMaster.ShopId);
            return View(tblInnoBillMaster);
        }

        // GET: TblInnoBillMasters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoBillMaster = await _context.TblInnoBillMasters.FindAsync(id);
            if (tblInnoBillMaster == null)
            {
                return NotFound();
            }
            ViewData["PaymentModeId"] = new SelectList(_context.TblInnoPaymentModeMasters, "PaymentModeId", "PaymentModeId", tblInnoBillMaster.PaymentModeId);
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId", tblInnoBillMaster.ShopId);
            return View(tblInnoBillMaster);
        }

        // POST: TblInnoBillMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("BillId,ShopId,BillNumber,CustomerMobile,TotalAmount,PaymentModeId,Notes,BillDate,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoBillMaster tblInnoBillMaster)
        {
            if (id != tblInnoBillMaster.BillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInnoBillMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblInnoBillMasterExists(tblInnoBillMaster.BillId))
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
            ViewData["PaymentModeId"] = new SelectList(_context.TblInnoPaymentModeMasters, "PaymentModeId", "PaymentModeId", tblInnoBillMaster.PaymentModeId);
            ViewData["ShopId"] = new SelectList(_context.TblInnoShopMasters, "ShopId", "ShopId", tblInnoBillMaster.ShopId);
            return View(tblInnoBillMaster);
        }

        // GET: TblInnoBillMasters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoBillMaster = await _context.TblInnoBillMasters
                .Include(t => t.PaymentMode)
                .Include(t => t.Shop)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (tblInnoBillMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoBillMaster);
        }

        // POST: TblInnoBillMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblInnoBillMaster = await _context.TblInnoBillMasters.FindAsync(id);
            if (tblInnoBillMaster != null)
            {
                _context.TblInnoBillMasters.Remove(tblInnoBillMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblInnoBillMasterExists(long id)
        {
            return _context.TblInnoBillMasters.Any(e => e.BillId == id);
        }
    }
}
