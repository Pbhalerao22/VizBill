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
    public class RoleMastersController : Controller
    {
        private readonly PostgresContext _context;

        public RoleMastersController(PostgresContext context)
        {
            _context = context;
        }

        // GET: RoleMasters
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblInnoRoleMasters.ToListAsync());
        }

        // GET: RoleMasters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoRoleMaster = await _context.TblInnoRoleMasters
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (tblInnoRoleMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoRoleMaster);
        }

        // GET: RoleMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName")] TblInnoRoleMaster tblInnoRoleMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInnoRoleMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblInnoRoleMaster);
        }

        // GET: RoleMasters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoRoleMaster = await _context.TblInnoRoleMasters.FindAsync(id);
            if (tblInnoRoleMaster == null)
            {
                return NotFound();
            }
            return View(tblInnoRoleMaster);
        }

        // POST: RoleMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("RoleId,RoleName,IsDeleted")] TblInnoRoleMaster tblInnoRoleMaster)
        {
            if (id != tblInnoRoleMaster.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInnoRoleMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblInnoRoleMasterExists(tblInnoRoleMaster.RoleId))
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
            return View(tblInnoRoleMaster);
        }

        // GET: RoleMasters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoRoleMaster = await _context.TblInnoRoleMasters
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (tblInnoRoleMaster == null)
            {
                return NotFound();
            }

            return View(tblInnoRoleMaster);
        }

        // POST: RoleMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblInnoRoleMaster = await _context.TblInnoRoleMasters.FindAsync(id);
            if (tblInnoRoleMaster != null)
            {
                _context.TblInnoRoleMasters.Remove(tblInnoRoleMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblInnoRoleMasterExists(long id)
        {
            return _context.TblInnoRoleMasters.Any(e => e.RoleId == id);
        }
    }
}
