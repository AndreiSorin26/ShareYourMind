using Database.Entities;

namespace Database.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        public Comment GetOne(Guid commentId);
        public IEnumerable<Comment> Get(Guid postId);
        public void Add(Comment comment);
        public void Update(Comment newComment);
        public void Delete(Guid commentId);
    }
}
