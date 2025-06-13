using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PlayerManagement.DAL;
using PlayerManagement.Models;

namespace PlayerManagement.ViewComponents
{
    public class HeadCountViewComponent:ViewComponent
    {
        private readonly AppDbContext _db;
        public HeadCountViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync(int CitizenshipId)
        {
            var citizenshipCounts = await _db.Players.Include(c => c.Citizenships).GroupBy(s => new { s.Citizenships.CitizenshipId, s.Citizenships.CountryName }).Select(g => new CountryHeadCount
            {
                CitizenshipId = g.Key.CitizenshipId,
                CountryName = g.Key.CountryName,
                Count = g.Count()
            })
             .ToListAsync();
            return View(citizenshipCounts);
        }
    }
}
