using trello_services.Entities;

namespace trello_services.Models.Request
{
    public class MiddleTableModel
    {
       public Guid workspaceId { get; set; }
        public Guid userId { get; set; }
        public Role? role { get; set; }
        public MiddleTableModel() { 
            role = Role.Member;
        }
    }
}
