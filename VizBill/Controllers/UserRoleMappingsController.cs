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
    public class UserRoleMappingsController : Controller
    {
        private readonly PostgresContext _context;

        public UserRoleMappingsController(PostgresContext context)
        {
            _context = context;
        }

        // GET: TblInnoUserRoleMappings
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.TblInnoUserRoleMappings.Include(t => t.Role).Include(t => t.User);
            return View(await postgresContext.ToListAsync());
        }

        // GET: TblInnoUserRoleMappings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoUserRoleMapping = await _context.TblInnoUserRoleMappings
                .Include(t => t.Role)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.UserRoleId == id);
            if (tblInnoUserRoleMapping == null)
            {
                return NotFound();
            }

            return View(tblInnoUserRoleMapping);
        }

        // GET: TblInnoUserRoleMappings/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.TblInnoRoleMasters, "RoleId", "RoleId");
            ViewData["UserId"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId");
            return View();
        }

        // POST: TblInnoUserRoleMappings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserRoleId,UserId,RoleId,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoUserRoleMapping tblInnoUserRoleMapping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblInnoUserRoleMapping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.TblInnoRoleMasters, "RoleId", "RoleId", tblInnoUserRoleMapping.RoleId);
            ViewData["UserId"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId", tblInnoUserRoleMapping.UserId);
            return View(tblInnoUserRoleMapping);
        }

        // GET: TblInnoUserRoleMappings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoUserRoleMapping = await _context.TblInnoUserRoleMappings.FindAsync(id);
            if (tblInnoUserRoleMapping == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.TblInnoRoleMasters, "RoleId", "RoleId", tblInnoUserRoleMapping.RoleId);
            ViewData["UserId"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId", tblInnoUserRoleMapping.UserId);
            return View(tblInnoUserRoleMapping);
        }

        // POST: TblInnoUserRoleMappings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("UserRoleId,UserId,RoleId,IsDeleted,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] TblInnoUserRoleMapping tblInnoUserRoleMapping)
        {
            if (id != tblInnoUserRoleMapping.UserRoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblInnoUserRoleMapping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblInnoUserRoleMappingExists(tblInnoUserRoleMapping.UserRoleId))
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
            ViewData["RoleId"] = new SelectList(_context.TblInnoRoleMasters, "RoleId", "RoleId", tblInnoUserRoleMapping.RoleId);
            ViewData["UserId"] = new SelectList(_context.TblInnoUserMasters, "UserId", "UserId", tblInnoUserRoleMapping.UserId);
            return View(tblInnoUserRoleMapping);
        }

        // GET: TblInnoUserRoleMappings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblInnoUserRoleMapping = await _context.TblInnoUserRoleMappings
                .Include(t => t.Role)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.UserRoleId == id);
            if (tblInnoUserRoleMapping == null)
            {
                return NotFound();
            }

            return View(tblInnoUserRoleMapping);
        }

        // POST: TblInnoUserRoleMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tblInnoUserRoleMapping = await _context.TblInnoUserRoleMappings.FindAsync(id);
            if (tblInnoUserRoleMapping != null)
            {
                _context.TblInnoUserRoleMappings.Remove(tblInnoUserRoleMapping);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblInnoUserRoleMappingExists(long id)
        {
            return _context.TblInnoUserRoleMappings.Any(e => e.UserRoleId == id);
        }
    }
}
