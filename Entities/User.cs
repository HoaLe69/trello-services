namespace trello_services.Entities
{
    public class User
    {
        public Guid userId { get; set; }
        public string? displayName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? avatar_path { get; set; }
        public IList<UserWorkspace> UserOfWorkspace { get; set; }
        public IList<UserCard> UserCards { get; set; }
        public IList<Activity> Activities { get; set; }
        public IList <Comment> Comments { get; set; }
        public IList<UserBoard> UserBoards { get; set; }
        public User()
        {
            Activities = new List<Activity>();
            UserOfWorkspace = new List<UserWorkspace>();
            UserCards = new List<UserCard>();
            Comments = new List<Comment>();
            UserBoards = new List<UserBoard>();
        }
    }
}
