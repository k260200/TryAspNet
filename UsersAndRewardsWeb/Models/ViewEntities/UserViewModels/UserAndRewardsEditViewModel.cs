using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

using Entities;

namespace UsersAndRewardsWeb.Models.ViewEntities
{
    public class UserAndRewardsEditViewModel
    {
        public UserAndRewardsEditViewModel()
        {
                
        }

        public UserAndRewardsEditViewModel(User user, List<SelectListItem> allRewards)
        {
            UserView = new SingleUserViewModel(user);

            AllRewards = new List<SelectListItem>(allRewards);
        }

        public SingleUserViewModel UserView { get; set; }

        public IList<SelectListItem> AllRewards { get; set; }
    }
}
