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
    public class PaymentModeMastersController : Controller
    {
        private readonly PostgresContext _context;

        public PaymentModeMastersController(PostgresContext context)
        {
            _context = context;
        }

        // GET: PaymentModeMasters
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblInnoPaymentModeMasters.ToListAsync());
        }

        // GET: PaymentModeMasters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoPaymentModeMaster = await _context.TblInnoPaymentModeMasters
                .FirstOrDefaultAsync(m => m.PaymentModeId == id);
            if (tblInnoPaymentModeMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoPaymentModeMaster);
        }

        // GET: PaymentModeMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentModeMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentModeId,ModeName,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoPaymentModeMaster tblInnoPaymentModeMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInnoPaymentModeMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblInnoPaymentModeMaster);
        }

        // GET: PaymentModeMasters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoPaymentModeMaster = await _context.TblInnoPaymentModeMasters.FindAsync(id);
            if (tblInnoPaymentModeMaster == null)
            {
                return NotFound();
            }
            return View(tblInnoPaymentModeMaster);
        }

        // POST: PaymentModeMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PaymentModeId,ModeName,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoPaymentModeMaster tblInnoPaymentModeMaster)
        {
            if (id != tblInnoPaymentModeMaster.PaymentModeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInnoPaymentModeMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblInnoPaymentModeMasterExists(tblInnoPaymentModeMaster.PaymentModeId))
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
            return View(tblInnoPaymentModeMaster);
        }

        // GET: PaymentModeMasters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoPaymentModeMaster = await _context.TblInnoPaymentModeMasters
                .FirstOrDefaultAsync(m => m.PaymentModeId == id);
            if (tblInnoPaymentModeMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoPaymentModeMaster);
        }

        // POST: PaymentModeMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblInnoPaymentModeMaster = await _context.TblInnoPaymentModeMasters.FindAsync(id);
            if (tblInnoPaymentModeMaster != null)
            {
                _context.TblInnoPaymentModeMasters.Remove(tblInnoPaymentModeMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblInnoPaymentModeMasterExists(long id)
        {
            return _context.TblInnoPaymentModeMasters.Any(e => e.PaymentModeId == id);
        }
    }
}
