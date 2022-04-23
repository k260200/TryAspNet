using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Mvc.Rendering;

using Interfaces;
using Entities;
using DL;
using BLL;

using UsersAndRewardsWeb.Models.ViewEntities;

namespace UsersAndRewardsWeb.Controllers
{
    public class RewardsController : Controller
    {
        private readonly IConfiguration _configuration;

        IDataLayer _data;

        IBusinessLogicLayer _logic;

        public RewardsController(IConfiguration config)
        {
            _configuration = config;
            string DBconnectionString = _configuration.GetConnectionString("DefaultConnection");
            _data = new DataBaseDL(DBconnectionString);
            _logic = new MainLogic(_data);
        }

        public IActionResult Index()
        {
            var rewards = _logic.GetRewards();

            return View(rewards);
        }

        [HttpPost]
        public IActionResult DeleteReward(int id)
        {
            _logic.DeleteReward(id);

            return RedirectToAction("Index");
        }

        public IActionResult EditRewardAndUsers(int id)
        {
            var reward = _logic.GetRewards().FirstOrDefault(i => i.ID == id);
            if (reward is null)
                return new NotFoundResult();
            var awardedUsersId = _logic.GetAwardedUsersId(id).ToList();
            var allUsers = _logic.GetUsers().ToList();
            List<SelectListItem> allUsersItemList = new List<SelectListItem>();
            foreach (var user in allUsers)
            {
                SelectListItem item = new SelectListItem { Text = user.FirstName + " " + user.LastName, Value = user.ID.ToString() };
                if (awardedUsersId.Contains(int.Parse(item.Value)))
                    item.Selected = true;

                allUsersItemList.Add(item);
            }
            RewardAndUsersEditViewModel rewardAndUsers = new RewardAndUsersEditViewModel(reward, allUsersItemList);

            return View(rewardAndUsers);
        }

        [HttpPost]
        public IActionResult EditRewardAndUsers(RewardAndUsersEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var rewardView = new SingleRewardViewModel(model.RewardView.ID, model.RewardView.Title,
                model.RewardView.Description);
            Reward reward = new Reward(rewardView.ID,
                rewardView.Title, rewardView.Description??="");

            _logic.EditReward(reward.ID, reward);

            for (int i = 0; i < model.AllUsers.Count; i++)
            {
                if (model.AllUsers[i].Selected == true)
                    _logic.AddRewardToUser(int.Parse(model.AllUsers[i].Value), reward.ID);
                else
                    _logic.TakeAwayRewardFromUser(int.Parse(model.AllUsers[i].Value), reward.ID);
            }

            return RedirectToAction("Index");
        }

        public IActionResult AddReward()
        {
            var allUsers = _logic.GetUsers().ToList();
            List<SelectListItem> allUsersItemList = new List<SelectListItem>(
                allUsers.Select(x => new SelectListItem { Text = x.FirstName + " " + x.LastName, Value = x.ID.ToString() }).ToList()
            );

            return View(new AddRewardViewModel(allUsersItemList));
        }

        [HttpPost]
        public IActionResult AddReward(AddRewardViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            int currentId = _data.AddReward(new Reward(model.RewardView.Title,
                model.RewardView.Description??=""));

            foreach (var item in model.AllUsers)
                if (item.Selected == true)
                    _data.AddUserAndReward(int.Parse(item.Value),
                        currentId);

            return RedirectToAction("Index");
        }
    }
}
