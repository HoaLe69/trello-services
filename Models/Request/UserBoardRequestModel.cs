using trello_services.Entities;

namespace trello_services.Models.Request
{
    public class UserBoardRequestModel
    {
        public Guid userId { get; set; }
        public Guid boardId { get; set; }
        public bool? star { get; set; }
        public Role? role { get; set; }
        public UserBoardRequestModel() {
            role = Role.Member;
        }
    }
}
