using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;

namespace Store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public StoreController(StoreDbContext context)
        {
            _context = context;
        }

        // Список іменинників
        [HttpGet("birthday")]
        public async Task<IActionResult> GetBirthdayPeople([FromQuery] DateTime date)
        {
            var list = await _context.Customers
                .Where(c => c.BirthDate.Day == date.Day && c.BirthDate.Month == date.Month)
                .Select(c => new { c.Id, c.FullName })
                .ToListAsync();

            return Ok(list);
        }

        // Останні покупці
        [HttpGet("recent-buyers")]
        public async Task<IActionResult> GetRecentBuyers([FromQuery] int days)
        {
            DateTime fromDate = DateTime.Today.AddDays(-days);

            var list = await _context.Purchases
                .Where(p => p.Date >= fromDate)
                .GroupBy(p => p.Customer)
                .Select(g => new {
                    g.Key.Id,
                    g.Key.FullName,
                    LastPurchase = g.Max(p => p.Date)
                })
                .ToListAsync();

            return Ok(list);
        }

        // Затребувані категорії
        [HttpGet("popular-categories/{customerId}")]
        public async Task<IActionResult> GetPopularCategories(int customerId)
        {
            var list = await _context.PurchaseItems
                .Where(pi => pi.Purchase.CustomerId == customerId)
                .GroupBy(pi => pi.Product.Category)
                .Select(g => new {
                    Category = g.Key,
                    TotalUnits = g.Sum(pi => pi.Quantity)
                })
                .ToListAsync();

            return Ok(list);
        }
    }
}
