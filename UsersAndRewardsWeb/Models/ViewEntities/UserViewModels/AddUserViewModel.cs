using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace UsersAndRewardsWeb.Models.ViewEntities
{
    public class AddUserViewModel
    {
        public AddUserViewModel()
        {

        }

        public AddUserViewModel(List<SelectListItem> allRewards)
        {
            UserView = new SingleUserViewModel() { BirthDate = DateTime.Now };

            AllRewards = new List<SelectListItem>(allRewards);
        }

        public SingleUserViewModel UserView { get; set; }

        public IList<SelectListItem> AllRewards { get; set; }
    }
}
