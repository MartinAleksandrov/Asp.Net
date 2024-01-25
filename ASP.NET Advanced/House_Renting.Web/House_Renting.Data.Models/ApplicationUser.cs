namespace House_Renting.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            RentedHouses = new List<House>();
        }

        public virtual ICollection<House> RentedHouses { get; set; }
    }
}
