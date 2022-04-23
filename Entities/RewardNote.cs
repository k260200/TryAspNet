using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    // пара награда-пользователь
    public class RewardNote
    {
        private Reward _reward;

        public Reward Reward { set => _reward = value; }

        public int Id { get => _reward.ID; }

        public string Title { get => _reward.Title; }

        public string Description { get => _reward.Description; }

        public string Users { get; set; }
    }
}
