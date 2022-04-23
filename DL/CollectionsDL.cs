using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Interfaces;
using System.Linq;

namespace DL
{
    public class CollectionsDL : IDataLayer
    {
        static int _rewardIdCalculator = -1;
        static int _userIdCalculator = -1;

        IList<User> Users { get; }
        IList<Reward> Rewards { get; }
        IList<KeyValuePair<int, int>> UsersAndRewards { get; }

        public CollectionsDL(IList<User> users, IList<Reward> rewards, IList<KeyValuePair<int, int>> usersAndRewards)
        {
            Users = users;
            Rewards = rewards;
            UsersAndRewards = usersAndRewards;
        }

        public CollectionsDL()
        {
            Users = new List<User>();
            Rewards = new List<Reward>();
            UsersAndRewards = new List<KeyValuePair<int, int>>();
        }

        public int AddReward(Reward reward)
        {
            Rewards.Add(new Reward(++_rewardIdCalculator, reward.Title, reward.Description));
            return _rewardIdCalculator;
        }

        public int AddUser(User user)
        {
            Users.Add(new User(++_userIdCalculator, user.FirstName, user.LastName, user.BirthDate)); // +
            return _userIdCalculator;
        }

        public void AddUserAndReward(int userId, int rewardId)
        {
            UsersAndRewards.Add(new KeyValuePair<int, int>(userId, rewardId));
        }

        public bool DeleteReward(int rewardID)
        {
            foreach (var awardedUser in GetAwardedUsers(rewardID))
                DeleteUserAndReward(awardedUser.ID, rewardID);

            return Rewards.Remove(Rewards.Where(i => i.ID == rewardID).First());
        }

        public bool DeleteUser(int userID)
        {
            foreach (var reward in GetUserRewards(userID))
                DeleteUserAndReward(userID, reward.ID);

            return Users.Remove(Users.Where(i => i.ID == userID).First());
        }

        public bool DeleteUserAndReward(int userId, int rewardId)
        {
            return UsersAndRewards.Remove(new KeyValuePair<int, int>(userId, rewardId));
        }

        public void EditReward(int rewardID, Reward newReward)
        {
            Rewards[rewardID] = newReward;
        }

        public void EditUser(int UserID, User newUser)
        {
            Users[UserID] = newUser;
        }

        public IEnumerable<Reward> GetRewards()
        {
            return Rewards;
        }

        public IEnumerable<User> GetUsers()
        {
            return Users;
        }

        public IEnumerable<KeyValuePair<int, int>> GetUsersAndRewards()
        {
            return UsersAndRewards;
        }

        public IEnumerable<Reward> GetUserRewards(int userID)
        {
            return Rewards.Where( i => UsersAndRewards.Contains( new KeyValuePair<int, int>( userID, i.ID ) ) );
        }

        public IEnumerable<User> GetAwardedUsers(int rewardID)
        {
            return Users.Where(i => UsersAndRewards.Contains(new KeyValuePair<int, int>(i.ID, rewardID)));
        }
    }
}
