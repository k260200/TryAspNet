using System;
using System.Collections.Generic;
using System.Text;

using Entities;

namespace Interfaces
{
    public interface IRewardDataGetterBL
    {
        IEnumerable<int> GetAwardedUsersId(int rewardId);

        IEnumerable<User> GetAwardedUsers(int rewardId);

        IEnumerable<Reward> GetRewards();
    }
}
