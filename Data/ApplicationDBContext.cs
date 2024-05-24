using Microsoft.EntityFrameworkCore;
using trello_services.Configuration.Entities;
using trello_services.Entities;

namespace trello_services.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext (DbContextOptions<ApplicationDBContext> options) : base(options) { }
        #region Dbset
        public DbSet<User> Users { get; set; }
        public DbSet<UserWorkspace> UserWorkspaces { get; set; }
        public DbSet<WorkSpace> Workspaces { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<UserBoard> UserBoards { get; set; }
        public DbSet<ListCard> Columns { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<CardLabel> CardLabels { get; set; }
        public DbSet<CheckList> CheckLists { get; set; }
        public DbSet<CheckListDetail> CheckListDetails { get; set; }
        public DbSet<UserCard> UserCards { get; set; }
        public DbSet<Activity> Activitys { get; set; }
        public DbSet<Comment> Comments { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // user
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            // user_Workspace
            modelBuilder.Entity<UserWorkspace>(e =>
            {
                e.ToTable("user_worksapce");
                e.HasKey(w => new { w.userId , w.workSpaceId});
                // one to many user
                e.HasOne(w => w.User)
                    .WithMany(u => u.UserOfWorkspace)
                    .HasForeignKey(w => w.userId);
                // one to many workspace
                e.HasOne(w => w.WorkSpace)
                    .WithMany(w => w.UserWorkspaces)
                    .HasForeignKey(w => w.workSpaceId)
                    // remove user when delete workspace
                    .OnDelete(DeleteBehavior.Cascade);
            });
            // workspace
            modelBuilder.Entity<WorkSpace>(e => {
                e.ToTable("workspace");
                e.HasKey(w => w.workSpaceId);
                e.Property(w => w.description).IsRequired(false);
            });
            //board
            modelBuilder.Entity<Board>(e => {
                e.ToTable("board");
                e.HasKey(b => b.boardId);
                e.Property(b => b.orderColumnIds).IsRequired(false);
                //one to many with work space
                e.HasOne(b => b.WorkSpace)
                    .WithMany(w => w.Boards)
                    .HasForeignKey(b =>  b.workSpaceId)
                    // remove Board when delete worksapce
                    .OnDelete(DeleteBehavior.Cascade);
            });
            // user_board
            modelBuilder.ApplyConfiguration(new UserBoardEntityConfiguration());
            // column
            modelBuilder.Entity<ListCard>(e => {
                e.ToTable("column");
                e.HasKey(c => c.columnId);
                e.Property(c => c.orderCardIds).IsRequired(false);
                // one to many board
                e.HasOne(c => c.Board)
                    .WithMany(b => b.Columns)
                    .HasForeignKey(c => c.boardId)
                    // delete column when remove board
                    .OnDelete(DeleteBehavior.Cascade);
            });
            //cards
            modelBuilder.Entity<Card>(e => {
                e.ToTable("card");
                e.HasKey(c => c.cardId);
                e.Property(c => c.description).IsRequired(false);
                e.Property(c => c.startDate).IsRequired(false);
                e.Property(c => c.endDate).IsRequired(false);
                e.Property(c => c.cover).IsRequired(false);
                // one to many column
                e.HasOne(c => c.Column)
                    .WithMany(c => c.Cards)
                    .HasForeignKey(c => c.columnId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            //label
            modelBuilder.Entity<Label>(e => {
                e.ToTable("label");
                e.Property(l => l.labelName).IsRequired(false);
                // one to many boards
                e.HasOne(l => l.Board)
                    .WithMany(b => b.Labels)
                    .HasForeignKey(l => l.boardId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            //card labels
            modelBuilder.Entity<CardLabel>(e => {
                e.ToTable("card_label");
                e.HasKey(cl =>new  { cl.cardId, cl.labelId });
                // one to many label
                e.HasOne(cl => cl.Label)
                    .WithMany(l => l.CardLabels)
                    .HasForeignKey(cl => cl.labelId)
                    .OnDelete(DeleteBehavior.Cascade);
                // one to many card
                e.HasOne(cl => cl.Card)
                    .WithMany(c => c.CardLabels)
                    .HasForeignKey(cl => cl.cardId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            //Checklists
            modelBuilder.Entity<CheckList>(e => {
                e.ToTable("checklist");
                e.HasKey(cl => cl.checkListId);
                // one to many card
                e.HasOne(cl => cl.Card)
                    .WithMany(c => c.CheckLists)
                    .HasForeignKey(c => c.cardId)
                    // remove checklist when remove card
                    .OnDelete(DeleteBehavior.Cascade);
            });
            //CheckListDetails
            modelBuilder.Entity<CheckListDetail>(e => {
                e.ToTable("check_list_detail");
                e.HasKey(cld => cld.clDetailId);
                //one to many checklist
                e.HasOne(cld => cld.CheckList)
                    .WithMany(cl => cl.CheckListDetails)
                    .HasForeignKey(cld => cld.checkListId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            // user card
            modelBuilder.Entity<UserCard>(e => {
                e.ToTable("user_card");
                e.HasKey(uc => new { uc.cardId , uc.userId});
                // one to many user
                e.HasOne(uc => uc.User)
                    .WithMany(u => u.UserCards)
                    .HasForeignKey(uc => uc.userId);
                // one to many card
                e.HasOne(uc => uc.Card)
                    .WithMany(c => c.UserCards)
                     .HasForeignKey(uc => uc.cardId)
                     .OnDelete(DeleteBehavior.Cascade);
            });
            // Activitys
            modelBuilder.Entity<Activity>(e => {
                e.ToTable("activity");
                e.HasKey(a => a.activityId);
                // one to many user
                e.HasOne(a => a.User)
                    .WithMany(u => u.Activities)
                    .HasForeignKey(a => a.userId);
                // one to many card
                e.HasOne(a => a.Card)
                    .WithMany(c => c.Activities)
                    .HasForeignKey(a => a.cardId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            //Comments
            modelBuilder.Entity<Comment>(e =>
            {
                e.ToTable("comment");
                e.HasKey(c => c.commentId);

                // one to many user
                e.HasOne(c => c.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(c => c.userId);
                // one to many card
                e.HasOne(c => c.Card)
                    .WithMany(c => c.Comments)
                    .HasForeignKey(c => c.cardId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
