using Entities;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace UsersAndRewardsWeb.Models.ViewEntities
{
    public class RewardAndUsersEditViewModel
    {
        public RewardAndUsersEditViewModel()
        {

        }

        public RewardAndUsersEditViewModel(Reward reward, List<SelectListItem> allUsers)
        {
            RewardView = new SingleRewardViewModel(reward);

            AllUsers = new List<SelectListItem>(allUsers);
        }

        public SingleRewardViewModel RewardView { get; set; }

        public IList<SelectListItem> AllUsers { get; set; }
    }
}
