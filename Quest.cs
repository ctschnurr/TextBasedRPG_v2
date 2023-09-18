using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{
    public class Quest
    {
        private string name;
        private int target;
        private int targetCount;
        private int reward;

        public Quest(string name, int target, int reward)
        {
            this.name = name;
            this.target = target;
            this.reward = reward;
            this.targetCount = 0; // Initialize the target count to zero.
        }

        public string GetName()
        {
            return name;
        }

        public int GetTarget()
        {
            return target;
        }

        public int GetTargetCount()
        {
            return targetCount;
        }

        public int GetReward()
        {
            return reward;
        }

        // Method to update the quest progress when the player makes progress toward the target.
        public void UpdateProgress(int amount)
        {
            targetCount += amount;
        }
    }
}
