using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlayerManagement.Models.ViewModels
{
    public class PlayerViewModel
    {
        public int PlayerId { get; set; }
        [Display(Name = "Name")]
        public string PlayerName { get; set; } = null!;
        [Display(Name = "Mobile")]
        public string MobileNo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsOverseas { get; set; }
        [Display(Name = "Country")]
        public int CitizenshipId { get; set; }
        [Display(Name = "Fee")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal SigningFee { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}"), DataType(DataType.Date)]
        public DateTime SigningDate { get; set; }
        [Display(Name = "Image")]
        public string? ImageUrl { get; set; } = null!;
        [Display(Name = "Upload Picture")]
        public IFormFile? ProfileFile { get; set; }
        public int PlayerSkillId { get; set; }
        public string? SkillName { get; set; } = null!;
        public string? SkillLevel { get; set; } = null!;
        public string? CountryName { get; set; } = null!;
        public virtual IList<Player>? Players { get; set; }
        public virtual IList<PlayerSkill> PlayerSkills { get; set; } = new List<PlayerSkill>();
        public virtual IList<Citizenship>? Citizenships { get; set; }
    }
}
