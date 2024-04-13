namespace trello_services.Models.Response
{
    public class UserResponseVM
    {
        public Guid userId { get; set; }
        public string displayName { get; set; }
        public string email { get; set; }
        public string avatar_path { get; set; }
    }
}
