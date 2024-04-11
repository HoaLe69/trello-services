using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using trello_services.Entities;

namespace trello_services.Configuration.Entities
{
    public class UserBoardEntityConfiguration : IEntityTypeConfiguration<UserBoard>
    {
        public void Configure(EntityTypeBuilder<UserBoard> builder)
        {
                builder.ToTable("user_board");
                builder.HasKey(ub => ub.userBoardId);
                // one to many board
                builder.HasOne(ub => ub.Board)
                    .WithMany(b => b.UserBoards)
                    .HasForeignKey(ub => ub.boardId)
                    // remove user when delete board
                    .OnDelete(DeleteBehavior.Cascade);
                // one to many user
                builder.HasOne(ub => ub.User)
                    .WithMany(u => u.UserBoards)
                    .HasForeignKey(ub => ub.userId);
        }
    }
}
