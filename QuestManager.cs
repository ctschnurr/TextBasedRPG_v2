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
        private static QuestManager instance;
        // This is the list of all possible quests.
        private static List<Quest> availableQuests = new List<Quest>();
        // This is the currently active quest.
        private static Quest activeQuest = null;
        private static Random Random = new Random();

        public static QuestManager Instance
        { get {
                if (instance == null)
                {
                    instance = new QuestManager();
                }
                return instance; 
            } 
        }
        public QuestManager()
        {
            availableQuests = new List<Quest>();
        }
        
        // This will send a message to the HUD.
        private static void SetMessage(string message)
        {
            HUD.SetMessage(message);
        }
        // This will select a random quest from the list of available quests and set it as the active quest.
        public static void SelectQuest()
        {
            // if statement to check if there are any quests left to give out.
            if (availableQuests.Count > 0)
            {
                int randomQuest = Random.Next(0, availableQuests.Count);
                activeQuest = availableQuests[randomQuest];
                SetMessage("You have selected a new quest: Kill " + activeQuest.GetTarget() + " " + activeQuest.GetName() + "s");
            }
            else
            {
                // Handle the case where there are no available quests.
                SetMessage("No available quests.");
            }
        }

        // This will remove the active quest from the list of available quests.
        public static void CompleteQuest()
        {
            if (activeQuest != null)
            {
                // reward player
                Player.AddGold(activeQuest.GetReward());
                availableQuests.Remove(activeQuest);
                activeQuest = null;
            }
            else
            {
                SetMessage("No active quest to complete.");
            }
        }

        public static string CurrentQuest()
        {
            if (activeQuest != null)
            {
                string questInfo = string.Format("Kill {1} {0}s, to receive {2} gold.",
                    activeQuest.GetName(), activeQuest.GetTarget(), activeQuest.GetReward());

                return questInfo;
            }
            else
            {
                return "No active quest.";
            }
        }
        
        // This will check if the active quest has been completed.
        public static bool CheckQuest()
        {
            if (activeQuest.GetTargetCount() >= activeQuest.GetTarget())
            {
                SetMessage("Task Complete! You have been rewarded " + activeQuest.GetReward() + " coins.");
                CompleteQuest();
                SelectQuest();
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool HasActiveQuest()
        {
            return activeQuest != null;
        }

        public static Quest GetActiveQuest()
        {
            return activeQuest;
        }


        // This will generate a random list of five quests. Each quest will have a target enemy chosen from the list of enemies, a target count, and a random reward in the form of coins.
        // This will be called from the main game loop.
        public static void GenerateQuests()
        {
            for (int i = 0; i < 5; i++)
            {
                int randomEnemy = Random.Next(0, EnemyManager.GetEnemies().Count);
                int randomTarget = Random.Next(1, 10);
                int randomReward = Random.Next(randomTarget, randomTarget * 10);
                Quest quest = new Quest(EnemyManager.GetEnemies()[randomEnemy].GetName(), randomTarget, randomReward);
                availableQuests.Add(quest);
            }
        }
    }
}