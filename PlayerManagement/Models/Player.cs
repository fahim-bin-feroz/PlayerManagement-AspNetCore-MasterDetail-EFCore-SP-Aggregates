using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerManagement.Models
{
    public class Player
    {
        public Player()
        {
            this.PlayerSkills = new HashSet<PlayerSkill>();
        }
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:yyyy-MM-dd}"),DataType(DataType.Date)]
        public DateTime SigningDate { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; } = null!;
        public virtual Citizenship Citizenships { get; set; } = null!;
        [Display(Name = "Skill")]
        public virtual ICollection<PlayerSkill> PlayerSkills { get; set; }
    }

    public class PlayerSkill
    {
        public int PlayerSkillId { get; set; }
        public string SkillName { get; set; } = null!;
        public string SkillLevel { get; set; } = null!;
        public int PlayerId { get; set; }
        public virtual Player? Players { get; set; } = null!;
    }

    public class Citizenship
    {
        public Citizenship()
        {
            this.Players = new HashSet<Player>();
        }
        public int CitizenshipId { get; set; }
        public string CountryName { get; set; } = null!;
        public virtual ICollection<Player> Players { get; set; }
    }
}
