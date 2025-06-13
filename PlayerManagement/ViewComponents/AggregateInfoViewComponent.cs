using Microsoft.AspNetCore.Mvc;
using PlayerManagement.DAL;
using PlayerManagement.Models.ViewModels;

namespace PlayerManagement.ViewComponents
{
    public class AggregateInfoViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;
        public AggregateInfoViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = _db.Players
                .Join(_db.Citizenships, p => p.CitizenshipId, c => c.CitizenshipId,
                      (p, c) => new { Player = p, Country = c })
                .ToList();

            if (data.Count > 0)
            {
                var min = data.Min(p => p.Player.SigningFee);
                var max = data.Max(p => p.Player.SigningFee);
                var sum = data.Sum(p => p.Player.SigningFee);
                var avg = data.Average(p => p.Player.SigningFee);

                var groupByResult = data
                    .GroupBy(p => new { p.Player.CitizenshipId, p.Country.CountryName })
                    .Select(c => new GroupByViewModel
                    {
                        CitizenshipId = c.Key.CitizenshipId,
                        CountryName = c.Key.CountryName,
                        MaxValue = c.Max(p => p.Player.SigningFee),
                        MinValue = c.Min(p => p.Player.SigningFee),
                        SumValue = c.Sum(p => p.Player.SigningFee),
                        AvgValue = c.Average(p => p.Player.SigningFee),
                        Count = c.Count()
                    }).ToList();

                var aggregateViewModel = new AggregateViewModel
                {
                    MinValue = min,
                    MaxValue = max,
                    SumValue = sum,
                    AvgValue = avg,
                    GroupByResult = groupByResult
                };

                return View(aggregateViewModel);
            }


            return View(new AggregateViewModel
            {
                MinValue = 0,
                MaxValue = 0,
                SumValue = 0,
                AvgValue = 0,
                GroupByResult = new List<GroupByViewModel>()
            });
        }
    }
}
