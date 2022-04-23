using System;
using System.Collections.Generic;
using System.Text;

using Entities;

namespace Interfaces
{
    public interface IUserRewardsControllerBL
    {
        void AddRewardToUser(int userId, int rewadrId);

        void AddRewardToUser(User user, Reward reward);

        void AddRewardsToUser(int userId, IEnumerable<int> rewardsId);

        void AddRewardsToUser(User user, IEnumerable<int> rewardsId);

        void TakeAwayRewardFromUser(int userId, int rewardId);

        void TakeAwayRewardFromUser(User user, Reward reward);
    }
}
