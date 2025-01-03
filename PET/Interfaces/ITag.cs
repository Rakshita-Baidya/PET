using PET.Models;

namespace PET.Interfaces
{
    public interface ITag
    {
        Task SaveTagAsync(Tags tag);
        Task<List<Tags>> LoadAllTagsAsync();
    }
}
