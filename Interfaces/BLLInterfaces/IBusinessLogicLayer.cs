using System;
using System.Collections.Generic;
using System.Text;

using Entities;

namespace Interfaces
{
    public interface IBusinessLogicLayer : IUserControllerBL, IRewardControllerBL
    {
        IEnumerable<UserNote> GetUsersWithRewards();

        IEnumerable<RewardNote> GetRewardsWithUsers();
    }
}
