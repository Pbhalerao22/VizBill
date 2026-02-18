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
        public async Task<IActionResult> Index(string search, int page = 1, int pageSize = 1)
        {
            var query = _context.TblInnoUserMasters
                                .Where(x => !x.IsDeleted);

            // 🔍 search
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search.Trim().ToLower()) ||
                    x.Email.ToLower().Contains(search.Trim().ToLower()) ||
                    x.GoogleId.ToLower().Contains(search.Trim().ToLower()));
            }

            var totalRecords = await query.CountAsync();

            var users = await query
                .OrderByDescending(x => x.UserId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new TblInnoUserMaster
                {
                    UserId = x.UserId,
                    Name = x.Name,
                    Email = x.Email,
                    GoogleId = x.GoogleId,
                    ProfileImage = x.ProfileImage,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            ViewBag.search= search;

            return View(users);
        }


        // GET: UserMasters/Details/5
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

        // GET: UserMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserMasters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,Email,GoogleId,IsActive")] TblInnoUserMaster model,
            IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                // Handle Image Upload
                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(ms);
                        model.ProfileImage = ms.ToArray();
                    }
                }

                // System Managed Fields
                model.CreatedOn = DateTime.Now;
                model.CreatedBy = 1; // Replace with logged-in user id
                model.IsDeleted = false;

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        // GET: UserMasters/Edit/5
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

        // POST: UserMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id,
      [Bind("UserId,Name,Email,GoogleId,IsActive")] TblInnoUserMaster model,
      IFormFile? imageFile)
        {
            if (id != model.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingUser = await _context.TblInnoUserMasters.FindAsync(id);
                if (existingUser == null)
                    return NotFound();

                // Update editable fields
                existingUser.Name = model.Name;
                existingUser.Email = model.Email;
                existingUser.GoogleId = model.GoogleId;
                existingUser.IsActive = model.IsActive;

                // Update Image only if new uploaded
                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(ms);
                        existingUser.ProfileImage = ms.ToArray();
                    }
                }

                // System managed fields
                existingUser.ModifiedOn = DateTime.Now;
                existingUser.ModifiedBy = 1; // replace with logged-in user id

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: UserMasters/Delete/5
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

        // POST: UserMasters/Delete/5
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
