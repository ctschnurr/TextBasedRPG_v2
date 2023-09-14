using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG_v2
{

    // This class will be used to manage quests, and will be called from the main game loop.
    internal class QuestManager
    {
        // This is the list of all possible quests.
        private static List<Quest> availableQuests = new List<Quest>();
        // This is the currently active quest.
        private static Quest activeQuest = null;
        
        // This will send a message to the HUD.
        private static void SetMessage(string message)
        {
            HUD.SetMessage(message);
        }
        // This will select a random quest from the list of available quests and set it as the active quest.
        public static void SelectQuest()
        {
            Random random = new Random();
            int randomQuest = random.Next(0, availableQuests.Count);
            activeQuest = availableQuests[randomQuest];
            SetMessage("You have selected a new quest: Kill " + activeQuest.GetTargetCount() + " " + activeQuest.GetName() + "s");
        }

        // This will remove the active quest from the list of available quests.
        public static void CompleteQuest()
        {
            // reward player
            Player.AddCoins(activeQuest.GetReward());
            availableQuests.Remove(activeQuest);
            activeQuest = null;
        }
        
        // This will check if the active quest has been completed.
        public static bool CheckQuest()
        {
            if (activeQuest.GetTargetCount() >= activeQuest.GetTarget())
            {
                SetMessage("You have completed the quest: Kill " + activeQuest.GetTargetCount() + " " + activeQuest.GetName() + "s killed. You have been rewarded " + activeQuest.GetReward() + " coins.");
                CompleteQuest();
                SelectQuest();
                return true;
            }
            else
            {
                return false;
            }
        }
        
        // This will generate a random list of five quests. Each quest will have a target enemy chosen from the list of enemies, a target count, and a random reward in the form of coins.
        // This will be called from the main game loop.
        public static void GenerateQuests()
        {
            for (int i = 0; i < 5; i++)
            {
                Random random = new Random();
                int randomEnemy = random.Next(0, EnemyManager.GetEnemyReferences().Count);
                int randomTarget = random.Next(1, 10);
                int randomReward = random.Next(randomTarget, randomTarget * 10);
                Quest quest = new Quest(EnemyManager.GetEnemyReferences()[randomEnemy], randomTarget, randomReward);
                availableQuests.Add(quest);
            }
        }
    }
}