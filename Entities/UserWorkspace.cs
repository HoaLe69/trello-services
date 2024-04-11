namespace trello_services.Entities
{
    public enum Role
    {
        Owner, Member
    }
    public class UserWorkspace
    {
        public int userWorkSpaceId { get; set; }
        public Guid workSpaceId { get; set; }
        public Guid userId { get; set; }
        public Role role { get; set; }
        public User User { get; set; }
        public WorkSpace WorkSpace { get; set; }
        public UserWorkspace()
        {
            role = Role.Member;
        }
    }
}
