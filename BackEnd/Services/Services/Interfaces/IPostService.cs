using Database.Entities;
using Services.DTOs;
using Services.DTOs.EntityMappings;

namespace Services.Interfaces
{
    public interface IPostService
    {
        public void Add(NewPostDTO newPost, String posterDisplayName);
        public void Delete(Guid id);
        public PostDTO Get(Guid id);
        public void Update(PostDTO newPost);
        public IEnumerable<PostDTO> GetPersonalPosts(Guid userId, int batchIndex);
        public IEnumerable<PostDTO> GetFeedPosts(Guid userId, int batchIndex);
    }
}
