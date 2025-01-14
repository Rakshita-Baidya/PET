using PET.Models;

namespace PET.Interfaces
{
    public interface ITag
    {
        Task AddTagAsync(Tags tag);
        Task<List<Tags>> LoadAllTagsAsync();
        Task SaveAllTagAsync(List<Tags> tags);
    }
}
