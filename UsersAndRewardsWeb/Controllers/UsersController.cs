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
    public class UsersController : Controller
    {
        private readonly IConfiguration _configuration;

        IDataLayer _data;

        IBusinessLogicLayer _logic;

        public UsersController(IConfiguration config)
        {
            _configuration = config;
            string DBconnectionString = _configuration.GetConnectionString("DefaultConnection");
            _data = new DataBaseDL(DBconnectionString);
            _logic = new MainLogic(_data);
        }

        public IActionResult Index()
        {
            var users = _logic.GetUsers();

            return View(users);
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            _logic.DeleteUser(id);

            return RedirectToAction("Index");
        }

        public IActionResult EditUserAndRewards(int id)
        {
            // TODO: перенести инициализацию в модель
            var user = _logic.GetUsers().FirstOrDefault(i => i.ID == id);
            if (user is null)
                return new NotFoundResult();
            var userRewardsId = _logic.GetUserRewardsId(id).ToList();
            var allRewards = _logic.GetRewards().ToList();
            List<SelectListItem> allRewardsItemList = new List<SelectListItem>();
            foreach (var reward in allRewards)
            {
                SelectListItem item = new SelectListItem { Text = reward.Title, Value = reward.ID.ToString() };
                if (userRewardsId.Contains(int.Parse(item.Value)))
                    item.Selected = true;

                allRewardsItemList.Add(item);
            }
            UserAndRewardsEditViewModel userAndRewards = new UserAndRewardsEditViewModel(user, allRewardsItemList);

            return View(userAndRewards);
        }

        [HttpPost]
        public IActionResult EditUserAndRewards(UserAndRewardsEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userView = new SingleUserViewModel(model.UserView.ID, model.UserView.FirstName,
                model.UserView.LastName, model.UserView.BirthDate);
            User user = new User(userView.ID,
                userView.FirstName, userView.LastName, userView.BirthDate);

            _logic.EditUser(user.ID, user);

            for (int i = 0; i < model.AllRewards.Count; i++)
            {
                if (model.AllRewards[i].Selected == true)
                    _logic.AddRewardToUser(user.ID, int.Parse(model.AllRewards[i].Value));
                else
                    _logic.TakeAwayRewardFromUser(user.ID, int.Parse(model.AllRewards[i].Value));
            }

            return RedirectToAction("Index");
        }

        public IActionResult AddUser()
        {
            var allRewards = _logic.GetRewards().ToList();
            List<SelectListItem> allRewardsItemList = new List<SelectListItem>(
                allRewards.Select(x => new SelectListItem { Text = x.Title, Value = x.ID.ToString() }).ToList()
            );

            return View(new AddUserViewModel(allRewardsItemList));
        }

        [HttpPost]
        public IActionResult AddUser(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            int currentId = _data.AddUser(new User(model.UserView.FirstName,
                model.UserView.LastName, model.UserView.BirthDate));

            foreach (var item in model.AllRewards)
                if (item.Selected == true)
                    _data.AddUserAndReward(currentId,
                        int.Parse(item.Value));

            return RedirectToAction("Index");
        }
    }
}