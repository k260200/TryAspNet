using System;
using System.Collections.Generic;
using System.Text;

using Entities;

namespace Interfaces
{
    public interface IDataLayer
    {
        // Возвращает ID добавленного пользователя
        int AddUser(User user);

        // Возвращает ID добавленной награды
        int AddReward(Reward reward);

        void AddUserAndReward(int userId, int rewardId);

        // Возвращает True при успешном удалении и false в противном случае
        bool DeleteUser(int userID);

        // Возвращает True при успешном удалении и false в противном случае
        bool DeleteReward(int rewardID);

        // Возвращает True при успешном удалении и false в противном случае
        bool DeleteUserAndReward(int userId, int RewardId);

        void EditUser(int userID, User newUser);

        void EditReward(int rewardID, Reward newReward);

        IEnumerable<User> GetUsers();

        IEnumerable<Reward> GetRewards();

        IEnumerable<Reward> GetUserRewards(int userID);

        IEnumerable<User> GetAwardedUsers(int rewardID);

        IEnumerable<KeyValuePair<int, int>> GetUsersAndRewards();
    }
}
