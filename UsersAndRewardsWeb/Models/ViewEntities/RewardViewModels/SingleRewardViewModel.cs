using Entities;

using System.ComponentModel.DataAnnotations;


namespace UsersAndRewardsWeb.Models.ViewEntities
{
    public class SingleRewardViewModel
    {
        public SingleRewardViewModel()
        {

        }
        public SingleRewardViewModel(Reward reward)
        {
            ID = reward.ID;
            Title = reward.Title;
            Description = reward.Description;
        }

        public SingleRewardViewModel(int id, string title, string description = "")
        {
            ID = id;
            Title = title;
            Description = description;
        }

        public SingleRewardViewModel(string title, string description = "") : this(0, title, description)
        {}

        public int ID { get; set; }

        [Required(ErrorMessage = "Title can not be empty")]
        [MaxLength(50, ErrorMessage = "Title is too long")]
        public string Title { get; set; }

        [MaxLength(150, ErrorMessage = "Description is too long")]
        public string Description { get; set; }
    }
}
