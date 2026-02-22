using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VizBill.MasterDbContext;
using VizBill.Models;

namespace VizBill.Controllers
{
    [Authorize]
    public class MainController : Controller
    {
        private readonly PostgresContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MainController(PostgresContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Home()
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

                int? userId = userIdClaim != null? int.Parse(userIdClaim.Value): (int?)null;

                var ShopiId = await _context.TblInnoShopMasters.Where(w => w.OwnerUserId == userId).Select(s => s.ShopId).FirstOrDefaultAsync();



                // If no shop found
                if (ShopiId == 0)
                {
                    ViewBag.TotalSales = 0;
                    ViewBag.TotalOrders = 0;
                    ViewBag.TotalAmountinUPI = 0;
                    ViewBag.TotalAmtCash = 0;
                    return View();
                }

                var TotalSales = _context.TblInnoBillMasters.Where(w => w.ShopId == ShopiId && w.CreatedOn == DateTime.Today).Sum(w => (decimal?)w.TotalAmount) ?? 0;
                ViewBag.TotalSales = TotalSales;

                var TotalOrders = _context.TblInnoBillMasters.Where(w => w.ShopId == ShopiId && w.CreatedOn == DateTime.Today).Sum(w => (decimal?)w.BillId) ?? 0;
                ViewBag.TotalOrders = TotalOrders;


                var TotalAmountinCash = await(from b in _context.TblInnoBillMasters
                                         join pm in _context.TblInnoPaymentModeMasters on b.PaymentModeId equals pm.PaymentModeId
                                         where pm.ModeName == "Cash" && b.CreatedOn == DateTime.Today && b.ShopId == ShopiId
                                         select (decimal?)b.TotalAmount).SumAsync() ?? 0;
                ViewBag.TotalAmtCash = TotalAmountinCash;


                var TotalAmountinUPI = await (from b in _context.TblInnoBillMasters
                                         join pm in _context.TblInnoPaymentModeMasters on b.PaymentModeId equals pm.PaymentModeId
                                         where pm.ModeName == "UPI" && b.CreatedOn == DateTime.Today && b.ShopId == ShopiId
                                         select (decimal?)b.TotalAmount).SumAsync() ?? 0;
                ViewBag.TotalAmtUPI = TotalAmountinUPI;


                var RecentTransaction = await (from bm in _context.TblInnoBillMasters
                                         join pm in _context.TblInnoPaymentModeMasters on bm.PaymentModeId equals pm.PaymentModeId
                                         where bm.CreatedOn == DateTime.Today && bm.ShopId == ShopiId orderby bm.CreatedOn descending
                                         select new { bm.BillNumber, bm.TotalAmount, bm.CreatedOn, pm.ModeName }).Take(5).ToListAsync();
                                      
                ViewBag.RecentTransaction = RecentTransaction;

            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }




        public IActionResult Bill()
        {
             var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            int? userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;

            var ShopiId = _context.TblInnoShopMasters.Where(w => w.OwnerUserId == userId).Select(s => s.ShopId).FirstOrDefault();

            var ListItem = (from s in _context.TblInnoItemMasters
                            join ca in _context.TblInnoCategoryMasters on s.CategoryId equals ca.CategoryId
                            where s.ShopId == ShopiId
                            select new { s.ItemId, s.ItemName, s.Price, ca.CategoryName }).ToList();
            ViewBag.ItemList = ListItem;
            return View();
        }

        public IActionResult GeneratedBillScreen(string amt, string mode, string billtoken)
        {
            ViewBag.Amount = amt;


            ViewBag.Token = billtoken;

            ViewBag.paymentMode = mode;
            return View();
        }

        [HttpPost]


        [HttpPost]
        public async Task<IActionResult> GeneratedBill([FromBody] BillViewModel model)
        {
            if (model == null)
                return Json(new { success = false });

            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

                int? userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;

                var shopId = await _context.TblInnoShopMasters
                    .Where(w => w.OwnerUserId == userId)
                    .Select(s => s.ShopId)
                    .FirstOrDefaultAsync();

                Random tok = new Random();
                var token = tok.Next();

                TblInnoBillMaster n = new TblInnoBillMaster
                {
                    ShopId = shopId,
                    BillNumber = token.ToString(),
                    TotalAmount = model.Amt,
                    PaymentModeId = 1,
                    BillDate = DateTime.Today,
                    CustomerMobile = null,
                    CreatedOn = DateTime.Today,
                    CreatedBy = 1
                };

                await _context.TblInnoBillMasters.AddAsync(n);
                await _context.SaveChangesAsync();  

                long generatedBillId = n.BillId;

                var billItems = model.Cart.Select(item => new TblInnoBillItemMapping
                {
                    BillId = generatedBillId,
                    ItemId = Convert.ToInt32(item.itemid),
                    ItemName = item.name,
                    Price = item.price,
                    Quantity = item.qty,
                    Total = (decimal)item.price * item.qty,
                    CreatedOn = DateTime.Today,
                    CreatedBy = userId
                }).ToList();

                await _context.TblInnoBillItemMappings.AddRangeAsync(billItems);

                await _context.SaveChangesAsync();   

                return Json(new
                {
                    success = true,
                    token = token.ToString(),
                    TotalAmt = model.Amt,
                    PaymentMode = model.Mode
                });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        public IActionResult Item()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            int? userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
            var ShopiId = _context.TblInnoShopMasters.Where(w => w.OwnerUserId == userId).Select(s => s.ShopId).FirstOrDefault();

            var ListItem = (from s in _context.TblInnoItemMasters
                            join ca in _context.TblInnoCategoryMasters on s.CategoryId equals ca.CategoryId
                            where s.ShopId == ShopiId
                            select new { s.ItemId, s.ItemName, s.Price, ca.CategoryName }).ToList();
            ViewBag.ItemList = ListItem;
            return View();
            
        }

        public IActionResult CreateItem()
        {
            return View();
        }

        public IActionResult Report()
        {
            return View();
        }

        public IActionResult Setting()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            int? userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
            var ShopiId = _context.TblInnoShopMasters.Where(w => w.OwnerUserId == userId).Select(s => s.ShopId).FirstOrDefault();

            ViewBag.ShopDetails=_context.TblInnoShopMasters.Where(w=>w.OwnerUserId== userId && w.ShopId== ShopiId).Select(s=>new {s.ShopId,s.ShopName,s.CompanyLogo,s.AdvertisementMsg,s.WhatsappNumber}).ToList();

           
            return View();
        }

        public IActionResult ShopSelection()
        {

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            int? userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
            var ShopiId = _context.TblInnoShopMasters.Where(w => w.OwnerUserId == userId).Select(s => new { s.ShopId,s.ShopName,s.ShopCategory,s.IsActive}).ToList();

            ViewBag.OwnerShops = ShopiId;
            return View();
        }

    }
}
