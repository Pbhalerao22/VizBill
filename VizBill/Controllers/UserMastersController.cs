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
    public class UserMastersController : Controller
    {
        private readonly PostgresContext _context;

        public UserMastersController(PostgresContext context)
        {
            _context = context;
        }

        // GET: TblInnoUserMasters
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblInnoUserMasters.ToListAsync());
        }

        // GET: TblInnoUserMasters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoUserMaster = await _context.TblInnoUserMasters
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (tblInnoUserMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoUserMaster);
        }

        // GET: TblInnoUserMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblInnoUserMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Email,GoogleId,ProfileImage,IsActive,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoUserMaster tblInnoUserMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInnoUserMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblInnoUserMaster);
        }

        // GET: TblInnoUserMasters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoUserMaster = await _context.TblInnoUserMasters.FindAsync(id);
            if (tblInnoUserMaster == null)
            {
                return NotFound();
            }
            return View(tblInnoUserMaster);
        }

        // POST: TblInnoUserMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("UserId,Name,Email,GoogleId,ProfileImage,IsActive,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoUserMaster tblInnoUserMaster)
        {
            if (id != tblInnoUserMaster.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInnoUserMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblInnoUserMasterExists(tblInnoUserMaster.UserId))
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
            return View(tblInnoUserMaster);
        }

        // GET: TblInnoUserMasters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoUserMaster = await _context.TblInnoUserMasters
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (tblInnoUserMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoUserMaster);
        }

        // POST: TblInnoUserMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblInnoUserMaster = await _context.TblInnoUserMasters.FindAsync(id);
            if (tblInnoUserMaster != null)
            {
                _context.TblInnoUserMasters.Remove(tblInnoUserMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblInnoUserMasterExists(long id)
        {
            return _context.TblInnoUserMasters.Any(e => e.UserId == id);
        }
    }
}
