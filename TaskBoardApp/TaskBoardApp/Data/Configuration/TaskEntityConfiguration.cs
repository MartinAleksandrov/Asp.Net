namespace TaskBoardApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TaskBoardApp.Data.ViewModels;

    public class TaskEntityConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasData(GenerateTasks());
        }
        private ICollection<Task> GenerateTasks()
        {
            ICollection<Task> tasks = new List<Task>();

            Task task;

            task = new Task()
            {
                Id = 1,
                Title = "Imporove CSS Styles",
                Description = "Implement better styling for all public pages",
                CreatedOn = DateTime.UtcNow.AddDays(-200),
                OwnerId = "9d6376b8-5ff1-42c3-8052-df28f0b4194b",
                BoardId = 1


            };
            tasks.Add(task);

            task = new Task()
            {
                Id = 2,
                Title = "Android Client App",
                Description = "Create Android App client for RESTful TaskBoard service",
                CreatedOn = DateTime.UtcNow.AddMonths(-5),
                OwnerId = "0eb084c3-24e8-4509-85df-413cf237d5f3",
                BoardId = 1


            };
            tasks.Add(task);

            task = new Task()
            {
                Id = 3,
                Title = "Desktop Client App",
                Description = "Create Windows Forms desktops app client for the taskBoard RESTful API",
                CreatedOn = DateTime.UtcNow.AddMonths(-1),
                OwnerId = "48f6bead-bdac-45d6-82ad-1ce829ab4be4",
                BoardId = 2


            };
            tasks.Add(task);

            task = new Task()
            {
                Id = 4,
                Title = "Create Tasks",
                Description = "Implement tasks [Create Task] page for adding new Tasks",
                CreatedOn = DateTime.UtcNow.AddMonths(-5),
                OwnerId = "3a7a60e8-04f8-42a5-96fe-75182feccce1",
                BoardId = 3


            };
            tasks.Add(task);

            return tasks;
        }
    }
}
