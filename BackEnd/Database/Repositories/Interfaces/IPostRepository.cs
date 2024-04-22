using Database.Entities;

namespace Database.Repositories.Interfaces
{
    public interface IPostRepository
    {
        public void Add(Post post);
        public void Delete(Post post);
        public void Update(Post newPost);
        public Post GetById(Guid id);
        public IEnumerable<Post> GetUserPosts(Guid userId);
        public IEnumerable<Post> GetUsersPosts(IEnumerable<Guid> userIds);
    }
}
