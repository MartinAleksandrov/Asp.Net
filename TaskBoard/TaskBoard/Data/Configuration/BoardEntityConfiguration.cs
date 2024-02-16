namespace TaskBoardApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TaskBoard.Data.Models;

    public class BoardEntityConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            ICollection<Board> boards = GenerateBoards();

            builder.
                HasData(boards);
        }

        private ICollection<Board> GenerateBoards()
        {
            ICollection<Board> boards = new List<Board>();

            Board board;

            board = new Board()
            {
                Id = 1,
                Name = "Open"
            };
            boards.Add(board);

            board = new Board()
            {
                Id = 2,
                Name = "In Progress"
            };

            boards.Add(board); board = new Board()
            {
                Id = 3,
                Name = "Done"
            };
            boards.Add(board);

            return boards;
        }
    }
}
