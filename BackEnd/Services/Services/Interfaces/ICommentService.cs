using Services.DTOs;
using Services.DTOs.EntityMappings;

namespace Services.Services.Interfaces
{
    public interface ICommentService
    {
        public CommentDTO GetOne(Guid commentId);
        public IEnumerable<CommentDTO> Get(Guid postId);
        public CommentDTO Add(NewCommentDTO comment, String posterDisplayName);
        public void Update(CommentDTO newComment);
        public void Delete(Guid commentId);
    }
}
