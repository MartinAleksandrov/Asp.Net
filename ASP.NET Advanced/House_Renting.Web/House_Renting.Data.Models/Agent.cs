namespace House_Renting.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static HouseRenting.Common.EntityValidationsConstants.Agent;

    public class Agent
    {
        public Agent()
        {
            Id = Guid.NewGuid();
            ManagedHouses = new List<House>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public ICollection<House> ManagedHouses { get; set; }

        public enum MyEnum { get; set; }
    }
}
