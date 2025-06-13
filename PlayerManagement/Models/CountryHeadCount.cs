namespace PlayerManagement.Models
{
    public class CountryHeadCount
    {
        public int CitizenshipId { get; set; }
        public int Count { get; set; }
        public string CountryName { get; set; }
        public virtual Citizenship Citizenship { get; set; }
    }
}
