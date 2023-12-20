namespace MyWebSite.Seeding
{
    using MyWebSite.ViewModels;
    public static class Seed
    {
        public static readonly IEnumerable<ProductViewModel> Products =
            new HashSet<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    Id= 1,
                    Name = "Chicken",
                    Price = 9.99m
                },
                new ProductViewModel
                {
                    Id= 2,
                    Name = "Burger",
                    Price = 6.00m
                },
                new ProductViewModel
                {
                    Id = 3,
                    Name = "Pizza",
                    Price = 2.50m
                },
                new ProductViewModel
                {
                    Id= 4,
                    Name = "Bread",
                    Price = 1.00m
                }
            };
    }
}
