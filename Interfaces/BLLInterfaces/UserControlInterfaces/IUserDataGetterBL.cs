using System;
using System.Collections.Generic;
using System.Text;

using Entities;

namespace Interfaces
{
    public interface IUserDataGetterBL
    {
        IEnumerable<User> GetUsers();

        IEnumerable<int> GetUserRewardsId(int userId);

        IEnumerable<Reward> GetUserRewards(int userId);
    }
}
