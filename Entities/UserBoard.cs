namespace trello_services.Entities
{
    public class UserBoard
    {
        public int userBoardId { get; set; }
        public Guid userId { get; set; }
        public Guid boardId { get; set; }
        public Role role { get; set; }
        public User User { get; set; }
        public Board Board { get; set; }
        public UserBoard()
        {
            role = Role.Member;
        }
        
    }
}
