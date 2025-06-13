using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PlayerManagement.DAL;
using PlayerManagement.Models;
using PlayerManagement.Models.ViewModels;

namespace PlayerManagement.Controllers
{
    public class PlayersController : Controller
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment web;

        public PlayersController(AppDbContext db, IWebHostEnvironment web)
        {
            this.db = db;
            this.web = web;
        }

        public IActionResult Index()
        {
            IEnumerable<Player> player = db.Players.Include(c => c.Citizenships).Include(p => p.PlayerSkills).ToList();
            return View(player);
        }

        public IActionResult DeletePlayer(int id)
        {
            var player = db.Players.Find(id);
            if (player != null)
            {
                db.Players.Remove(player);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult CreatePlayer()
        {
            PlayerViewModel player = new PlayerViewModel();
            player.Citizenships = db.Citizenships.ToList();
            player.PlayerSkills.Add(new PlayerSkill { PlayerSkillId = 1 });
            return PartialView("_CreatePlayer", player);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlayer(PlayerViewModel vobj)
        {
            if (!ModelState.IsValid)
            {
                vobj.Citizenships = db.Citizenships.ToList();
                return View();
            }
            Player obj = new Player
            {
                PlayerId = vobj.PlayerId,
                PlayerName = vobj.PlayerName,
                MobileNo = vobj.MobileNo,
                Email = vobj.Email,
                IsOverseas = vobj.IsOverseas,
                CitizenshipId = vobj.CitizenshipId,
                SigningFee = vobj.SigningFee,
                SigningDate = vobj.SigningDate,
                PlayerSkills = vobj.PlayerSkills
            };
            if (vobj.ProfileFile != null)
            {
                string fileName = await GetFileName(vobj.ProfileFile);
                obj.ImageUrl = fileName;
            }
            else
            {
                obj.ImageUrl = "noimage.jpg";
            }
            DataTable skillTable = new DataTable();
            skillTable.Columns.Add("SkillName", typeof(string));
            skillTable.Columns.Add("SkillLevel", typeof(string));
            if (obj.PlayerSkills != null && obj.PlayerSkills.Any())
            {
                foreach (var m in obj.PlayerSkills)
                {
                    skillTable.Rows.Add(m.SkillName, m.SkillLevel);
                }
            }
            var parameters = new[]
            {
                new SqlParameter("@PlayerName",obj.PlayerName),
                new SqlParameter("@MobileNo",obj.MobileNo),
                new SqlParameter("@Email",obj.Email),
                new SqlParameter("@IsOverseas",obj.IsOverseas),
                new SqlParameter("@CitizenshipId",obj.CitizenshipId),
                new SqlParameter("@SigningFee",obj.SigningFee),
                new SqlParameter("@SigningDate",obj.SigningDate),
                new SqlParameter("@ImageUrl",obj.ImageUrl?? (object) DBNull.Value),

                new SqlParameter
                {
                    ParameterName = "@PlayerSkills",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.FParamSkillType",
                    Value = skillTable
                }


            };

            await db.Database.ExecuteSqlRawAsync("EXEC dbo.spInsertPlayer @PlayerName,@MobileNo,@Email,@IsOverseas,@CitizenshipId,@SigningFee,@SigningDate,@ImageUrl,@PlayerSkills", parameters);

            return RedirectToAction("Index");
        }

        private async Task<string> GetFileName(IFormFile profileFile)
        {
            string uFileName = null;
            if (profileFile != null)
            {
                uFileName = Guid.NewGuid().ToString() + Path.GetExtension(profileFile.FileName);
                string uploadFolder = Path.Combine(web.WebRootPath, "images");
                string filePath = Path.Combine(uploadFolder, uFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await profileFile.CopyToAsync(fileStream);
                }
            }
            return uFileName;
        }
        public IActionResult EditPlayer(int id)
        {
            var player = db.Players.Include(p => p.PlayerSkills).FirstOrDefault(x => x.PlayerId == id);
            var vObj = new PlayerViewModel
            {
                PlayerId = player.PlayerId,
                PlayerName = player.PlayerName,
                MobileNo = player.MobileNo,
                Email = player.Email,
                IsOverseas = player.IsOverseas,
                CitizenshipId = player.CitizenshipId,
                SigningDate = player.SigningDate,
                SigningFee = player.SigningFee,
                ImageUrl = player.ImageUrl,
                PlayerSkills = player.PlayerSkills.ToList(),
                Citizenships = db.Citizenships.ToList()
            };
            return PartialView("_EditPlayer", vObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPlayer(PlayerViewModel vobj, string OldImageUrl)
        {
            if (!ModelState.IsValid)
            {
                vobj.Citizenships = db.Citizenships.ToList();
                return View();
            }
            Player obj = db.Players.FirstOrDefault(x => x.PlayerId == vobj.PlayerId);
            if (obj != null)
            {
                obj.PlayerId = vobj.PlayerId;
                obj.PlayerName = vobj.PlayerName;
                obj.Email = vobj.Email;
                obj.MobileNo = vobj.MobileNo;
                obj.IsOverseas = vobj.IsOverseas;
                obj.CitizenshipId = vobj.CitizenshipId;
                obj.SigningFee = vobj.SigningFee;
                obj.SigningDate = vobj.SigningDate;
                if (vobj.ProfileFile != null)
                {
                    string fileName = await GetFileName(vobj.ProfileFile);
                    obj.ImageUrl = fileName;
                }
                else
                {
                    obj.ImageUrl = OldImageUrl;
                }
                var skills = db.PlayerSkills.Where(x => x.PlayerId == vobj.PlayerId).ToList();
                DataTable skillTable = new DataTable();
                skillTable.Columns.Add("SkillName", typeof(string));
                skillTable.Columns.Add("SkillLevel", typeof(string));
                if (vobj.PlayerSkills != null && vobj.PlayerSkills.Any())
                {
                    foreach (var m in vobj.PlayerSkills)
                    {
                        skillTable.Rows.Add(m.SkillName, m.SkillLevel);
                    }
                }
                var parameters = new[]
                {
                new SqlParameter("@PlayerName",obj.PlayerName),
                new SqlParameter("@MobileNo",obj.MobileNo),
                new SqlParameter("@Email",obj.Email),
                new SqlParameter("@IsOverseas",obj.IsOverseas),
                new SqlParameter("@CitizenshipId",obj.CitizenshipId),
                new SqlParameter("@SigningFee",obj.SigningFee),
                new SqlParameter("@SigningDate",obj.SigningDate),
                new SqlParameter("@ImageUrl",obj.ImageUrl?? (object) DBNull.Value),

                new SqlParameter
                {
                    ParameterName = "@PlayerSkills",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.EditParamSkillType",
                    Value = skillTable
                },

                new SqlParameter("@PlayerId",vobj.PlayerId)

            };

                await db.Database.ExecuteSqlRawAsync("EXEC dbo.spUpdatePlayer @PlayerName,@MobileNo,@Email,@IsOverseas,@CitizenshipId,@SigningFee,@SigningDate,@ImageUrl,@PlayerSkills,@PlayerId", parameters);
                return RedirectToAction("Index");
            }
            vobj.Citizenships = db.Citizenships.ToList();
            return View(vobj);
        }
    }
}
