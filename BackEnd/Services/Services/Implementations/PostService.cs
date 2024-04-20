using Database.Entities;
using Database.Repositories.Interfaces;
using Services.DTOs;
using Services.DTOs.EntityMappings;
using Services.Exceptions;
using Services.Interfaces;

using AutoMapper;

namespace Services.Implementations
{
    public class PostService(IPostRepository m_postRepository, IUserRepository m_userRepository, IFriendRequestRepository m_friendRequestRepository, IMapper m_mapper) : IPostService
    {
        private const int BatchSize = 20;


        public void Add(NewPostDTO newPost, String senderDisplayname)
        {
            try
            {
                User user = m_userRepository.GetUserByDisplayName(senderDisplayname);

                Post post = new()
                {
                    UserId = user.Id,
                    Text = newPost.Text
                };

                m_postRepository.Add(post);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Post cannot be null");
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                Post post = m_postRepository.GetById(id);
                m_postRepository.Delete(post);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Id cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("Post not found");
            }
        }

        public PostDTO Get(Guid id)
        {
            try
            {
                Post post = m_postRepository.GetById(id);
                return m_mapper.Map<PostDTO>(post);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Id cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("Post not found");
            }
        }

        public void Update(PostDTO newPost)
        {
            try
            {
                Post oldPost = m_postRepository.GetById(newPost.Id);
                oldPost.Text = newPost.Text;
                oldPost.UpdateTime();
                m_postRepository.Update(oldPost);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Id cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("Post not found");
            }
        }

        public IEnumerable<PostDTO> GetPersonalPosts(Guid userId, int batchIndex)
        {
            try
            {

                List<Post> userPosts = [..m_postRepository.GetUserPosts(userId).OrderByDescending(post => post.UpdatedAt)];

                int startIndex = batchIndex * BatchSize;
                if (startIndex >= userPosts.Count)
                    return [];

                int count = Math.Min(BatchSize, userPosts.Count - startIndex);
                List<Post> batchPosts = userPosts.Slice(startIndex, count);
                return m_mapper.Map<List<PostDTO>>(batchPosts);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Id cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("Post not found");
            }
        }

        public IEnumerable<PostDTO> GetFeedPosts(Guid userId, int batchIndex)
        {
            IEnumerable<User> friends = m_friendRequestRepository.GetAcceptedFriendRequests(userId)
                                                                 .Select(fr => fr.SenderId == userId ? fr.Receiver : fr.Sender);
            IEnumerable<Guid> friendIds = friends.Select(user => user.Id);
            List<Post> friendPosts = [..m_postRepository.GetUsersPosts(friendIds).OrderByDescending(post => post.UpdatedAt)];

            int startIndex = batchIndex * BatchSize;
            if (startIndex >= friendPosts.Count)
                return [];

            int count = Math.Min(BatchSize, friendPosts.Count - startIndex);
            List<Post> batchPosts =  friendPosts.Slice(startIndex, count);
            return m_mapper.Map<List<PostDTO>>(batchPosts);
        }
    }
}
