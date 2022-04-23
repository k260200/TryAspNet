using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace UsersAndRewardsWeb.Models.ViewEntities
{
    public class AddRewardViewModel
    {
        public AddRewardViewModel()
        {

        }

        public AddRewardViewModel(List<SelectListItem> allUsers)
        {
            RewardView = new SingleRewardViewModel();

            AllUsers = new List<SelectListItem>(allUsers);
        }

        public SingleRewardViewModel RewardView { get; set; }

        public IList<SelectListItem> AllUsers { get; set; }
    }
}
