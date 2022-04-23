using System;
using System.Collections.Generic;
using System.Text;

using Entities;

namespace Interfaces
{
    public interface IRewardEditorBL
    {
        void EditReward(int rewardId, Reward newReward);

        void EditReward(Reward existingReward, Reward newReward);
    }
}
