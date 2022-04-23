using System;
using System.Collections.Generic;
using Entities;
using Interfaces;
using System.Linq;

namespace BLL
{
    public class MainLogic : IBusinessLogicLayer
    {
        IDataLayer _data;
        
        public MainLogic(IDataLayer data)
        {
            _data = data;
        }

        public int AddReward(Reward reward)
        {
            return _data.AddReward(reward);
        }

        public void AddRewardsToUser(int userId, IEnumerable<int> rewardsId)
        {
            foreach (var rewardId in rewardsId)
                if (!_data.GetUsersAndRewards().Contains(new KeyValuePair<int, int>(userId, rewardId)))
                    AddRewardToUser(userId, rewardId);
        }

        public void AddRewardsToUser(User user, IEnumerable<int> rewardsId)
        {
            this.AddRewardsToUser(user.ID, rewardsId);
        }

        public void AddRewardToUser(int userId, int rewardId)
        {
            if (!_data.GetUsersAndRewards().Contains(new KeyValuePair<int, int>(userId, rewardId)))
                _data.AddUserAndReward(userId, rewardId);
        }

        public void AddRewardToUser(User user, Reward reward)
        {
            if (!_data.GetUsers().Contains(user) | !_data.GetRewards().Contains(reward))
                throw new ArgumentException("Переданы некорректные пользователь или награда!");

            this.AddRewardToUser(user.ID, reward.ID);
        }

        public void TakeAwayRewardFromUser(int userId, int rewardId)
        {
            _data.DeleteUserAndReward(userId, rewardId);
        }

        public void TakeAwayRewardFromUser(User user, Reward reward)
        {
            this.TakeAwayRewardFromUser(user.ID, reward.ID);
        }

        public int AddUser(User user)
        {
            return _data.AddUser(user);
        }

        public void DeleteReward(int rewardId)
        {
            _data.DeleteReward(rewardId);
        }

        public void DeleteReward(Reward reward)
        {
            _data.DeleteReward(reward.ID);
        }

        public void DeleteUser(int userId)
        {
            _data.DeleteUser(userId);
        }

        public void DeleteUser(User user)
        {
            _data.DeleteUser(user.ID);
        }

        public void EditReward(int rewardId, Reward newReward)
        {
            _data.EditReward(rewardId, newReward);
        }

        public void EditReward(Reward existingReward, Reward newReward)
        {
            _data.EditReward(existingReward.ID, newReward);
        }

        public void EditUser(int userId, User newUser)
        {
            _data.EditUser(userId, newUser);
        }

        public void EditUser(User existingUser, User newUser)
        {
            _data.EditUser(existingUser.ID, newUser);
        }

        public IEnumerable<Reward> GetRewards()
        {
            return _data.GetRewards();
        }

        public IEnumerable<RewardNote> GetRewardsWithUsers()
        {
            List <RewardNote> rewardNotes = new List<RewardNote>();
            RewardNote current;

            foreach (var reward in _data.GetRewards())
            {
                current = new RewardNote();
                current.Reward = reward;
                foreach (var pair in _data.GetUsersAndRewards())
                    if (pair.Value == reward.ID)
                    {
                        User rewardedUser = _data.GetUsers().Where(i => i.ID == pair.Key).First();
                        current.Users += rewardedUser.ToString() + ";" + Environment.NewLine;
                    }
                rewardNotes.Add(current);
            }

            return rewardNotes;
        }

        public IEnumerable<User> GetUsers()
        {
            return _data.GetUsers();
        }

        public IEnumerable<UserNote> GetUsersWithRewards()
        {
            List<UserNote> userNotes = new List<UserNote>();
            UserNote current;

            foreach (var user in _data.GetUsers())
            {
                current = new UserNote();
                current.User = user;
                foreach (var pair in _data.GetUsersAndRewards())
                    if (pair.Key == user.ID)
                    {
                        Reward reward = _data.GetRewards().Where(i => i.ID == pair.Value).First();
                        current.Rewards += reward.ToString() + ";" + Environment.NewLine;
                    }
                userNotes.Add(current);
            }

            return userNotes;
        }

        public IEnumerable<int> GetUserRewardsId(int userId)
        {
            return _data.GetUsersAndRewards().Where(i => i.Key == userId).Select(i => i.Value).ToList();
        }

        public IEnumerable<int> GetAwardedUsersId(int rewardId)
        {
            return _data.GetUsersAndRewards().Where(i => i.Value == rewardId).Select(i => i.Key).ToList();
        }

        public IEnumerable<Reward> GetUserRewards(int userId)
        {
            return _data.GetUserRewards(userId);
        }

        public IEnumerable<User> GetAwardedUsers(int rewardId)
        {
            return _data.GetAwardedUsers(rewardId);
        }
    }
}
