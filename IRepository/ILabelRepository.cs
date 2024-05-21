using trello_services.Entities;
using trello_services.Models.Request;

namespace trello_services.IRepository
{
    public interface ILabelRepository
    {
        Task<Label> CreateNewLabelAsync(LabelRequestModel request);
        Task DeleteLabelAsync(Guid labelId);
        Task<Label> UpdateLabelAsync(Guid labelId , LabelRequestModel update);
        Task<IList<Label>> GetLabelByBoarId (Guid boarId);
        Task<Label> GetById(Guid labelId);
    }
}
