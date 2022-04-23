using System;
using System.Collections.Generic;
using System.Text;

using Entities;

namespace Interfaces
{
    public interface IRewardDeleterBL
    {
        void DeleteReward(int rewardId);

        void DeleteReward(Reward reward);
    }
}
