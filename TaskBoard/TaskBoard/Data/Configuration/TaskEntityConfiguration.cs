namespace TaskBoardApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TaskBoard.Data.Models;

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
                OwnerId = "1d5509ba-8a6f-43a1-9079-278d45a3f8a4",
                BoardId = 1


            };
            tasks.Add(task);

            task = new Task()
            {
                Id = 2,
                Title = "Android Client App",
                Description = "Create Android App client for RESTful TaskBoard service",
                CreatedOn = DateTime.UtcNow.AddMonths(-5),
                OwnerId = "1d5509ba-8a6f-43a1-9079-278d45a3f8a4",
                BoardId = 1


            };
            tasks.Add(task);

            task = new Task()
            {
                Id = 3,
                Title = "Desktop Client App",
                Description = "Create Windows Forms desktops app client for the taskBoard RESTful API",
                CreatedOn = DateTime.UtcNow.AddMonths(-1),
                OwnerId = "1d5509ba-8a6f-43a1-9079-278d45a3f8a4",
                BoardId = 2


            };
            tasks.Add(task);

            task = new Task()
            {
                Id = 4,
                Title = "Create Tasks",
                Description = "Implement tasks [Create Task] page for adding new Tasks",
                CreatedOn = DateTime.UtcNow.AddMonths(-5),
                OwnerId = "1d5509ba-8a6f-43a1-9079-278d45a3f8a4",
                BoardId = 3
            };
            tasks.Add(task);

            return tasks;
        }
    }
}
