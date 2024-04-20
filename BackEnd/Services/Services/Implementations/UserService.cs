using Database.Entities;
using Database.Entities.Enums;
using Database.Repositories.Interfaces;
using Services.DTOs;
using Services.DTOs.EntityMappings;
using Services.Exceptions;
using Services.Interfaces;

using AutoMapper;
using System.Security.Cryptography;
using System.Text;

namespace Services.Implementations
{
    public class UserService(IUserRepository m_userRepository, 
                             IFriendRequestRepository m_friendRequestRepository, 
                             IPostRepository m_postRepository,
                             IReportRepository m_reportRepository,
                             IMapper m_mapper) : IUserService
    {
        public UserDTO Add(NewUserDTO newUser)
        {
            const int iterationCount = 350000;
            const int saltSize = 64;

            var tag = RandomNumberGenerator.GetString(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'], 5);
            var saltBytes = RandomNumberGenerator.GetBytes(saltSize);
            var passwordBytes = Encoding.UTF8.GetBytes(newUser.Password);

            var hashedPassword = Rfc2898DeriveBytes.Pbkdf2(passwordBytes, saltBytes, iterationCount, HashAlgorithmName.SHA512, saltSize);

            var userRole = (UserRole)Enum.Parse(typeof(UserRole), newUser.Role);

            User user = new()
            {
                Username = newUser.Username,
                Password = Convert.ToHexString(hashedPassword),
                Salt = saltBytes,
                Role = userRole,
                Tag = tag,
                DisplayName = newUser.Username + "#" + tag,
                Approved = userRole == UserRole.User
            };

            m_userRepository.Add(user);
            return m_mapper.Map<UserDTO>(user);
        }

        public void Delete(String displayName) 
        {
            try
            {
                m_userRepository.Delete(displayName);
            }
            catch(ArgumentNullException)
            {
                throw new BadQuery("Id cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("User not found");
            }
        }

        public void ApproveAdmin(UserDTO user)
        {
            try
            {
                User existentUser = m_userRepository.GetUserById(user.Id);
                existentUser.Approved = true;
                m_userRepository.Update(existentUser);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("User cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("User not found");
            }
        }

        public UserDTO Get(string displayName)
        {
            try
            {
                User user = m_userRepository.GetUserByDisplayName(displayName);
                return m_mapper.Map<UserDTO>(user);
            }
            catch(ArgumentNullException)
            {
                throw new BadQuery("Dispaly name cannot be null");
            }
            catch(NullReferenceException) 
            {
                throw new EntityNotFound("User not found");
            }
        }

        public User GetRaw(String displayName)
        {
            try
            {
                User user = m_userRepository.GetUserByDisplayName(displayName);
                return user;
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Dispaly name cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("User not found");
            }
        }

        public UserDTO Get(Guid id)
        {
            try
            {
                User user = m_userRepository.GetUserById(id);
                return m_mapper.Map<UserDTO>(user);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Dispaly name cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("User not found");
            }
        }

        public IEnumerable<UserDTO> GetUnapprovedAdmins()
        {
            IEnumerable<User> unapprovedAdmins = m_userRepository.GetUnapprovedAdmins();
            return m_mapper.Map<IEnumerable<UserDTO>>(unapprovedAdmins);
        }

        public FriendRequestDTO SendFriendRequest(UserDTO sender, UserDTO receiver)
        {
            FriendRequest fr = new()
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Status = FriendRequestStatus.PENDING
            };
            m_friendRequestRepository.Add(fr);
            return m_mapper.Map<FriendRequestDTO>(fr);
        }

        public void UpdateFriendRequest(Guid id, FriendRequestStatus status)
        {
            try
            {
                FriendRequest fr = m_friendRequestRepository.Get(id);
                fr.Status = status;
                m_friendRequestRepository.Update(fr);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Dispaly name cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("Entity not found");
            }
        }

        public IEnumerable<FriendRequestDTO> GetFriendRequests(Guid id)
        {
            try
            {
                IEnumerable<FriendRequest> friendRequests = m_friendRequestRepository.GetSentFriendsRequests(id)
                                                                                     .Union(m_friendRequestRepository.GetReceivedFriendsRequests(id));
                return m_mapper.Map<IEnumerable<FriendRequestDTO>>(friendRequests);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Dispaly name cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("Entity not found");
            }
        }

        public void HandleUserReaction(string displayName, PostDTO postDTO, ReactionType reactionType)
        {
            try
            {
                User user = m_userRepository.GetUserByDisplayName(displayName);
                Post post = m_postRepository.GetById(postDTO.Id);
                m_userRepository.HandleUserReaction(user, post, reactionType);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Dispaly name cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("Entity not found");
            }
        }

        public void CloseReport(Guid reportId)
        {
            try
            {
                Report report = m_reportRepository.Get(reportId);
                report.Closed = true;
                m_reportRepository.Update(report);
            }
            catch (ArgumentNullException)
            {
                throw new BadQuery("Dispaly name cannot be null");
            }
            catch (NullReferenceException)
            {
                throw new EntityNotFound("Entity not found");
            }
        }
    }
}
