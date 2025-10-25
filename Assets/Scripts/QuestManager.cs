using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("UI Setup")]
    public GameObject questPanel;           // Assign the main quest panel in Canvas
    public Transform questContent;          // Parent object for quest entries
    public GameObject questItemPrefab;      // Prefab for a single quest entry

    // Internal quest representation
    public class Quest
    {
        public string name;
        public int targetAmount;          // Total required
        public int currentAmount;         // Delivered so far
        public TextMeshProUGUI uiText;

        public Quest(string name, int targetAmount, int alreadyDelivered = 0)
        {
            this.name = name;
            this.targetAmount = targetAmount;
            this.currentAmount = alreadyDelivered;
            this.uiText = null;
        }

        public void UpdateUI()
        {
            if (uiText != null)
                uiText.text = $"{name}: {currentAmount}/{targetAmount}";
        }

        public bool IsComplete()
        {
            return currentAmount >= targetAmount;
        }
    }

    private Dictionary<string, Quest> activeQuests = new Dictionary<string, Quest>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Assign a new quest
    public void AssignQuest(string questName, int targetAmount, int alreadyDelivered = 0)
    {
        if (activeQuests.ContainsKey(questName))
        {
            // Quest already exists, just refresh UI
            activeQuests[questName].UpdateUI();
            return;
        }

        Quest newQuest = new Quest(questName, targetAmount, alreadyDelivered);
        activeQuests.Add(questName, newQuest);

        // Create UI element
        GameObject questUI = Instantiate(questItemPrefab, questContent);
        newQuest.uiText = questUI.GetComponentInChildren<TextMeshProUGUI>();
        newQuest.UpdateUI();

        questPanel.SetActive(true);
    }

    // Update progress for a quest (e.g., player delivers items)
    public void UpdateQuestProgress(string questName, int amountDelivered)
    {
        if (!activeQuests.ContainsKey(questName)) return;

        Quest quest = activeQuests[questName];
        quest.currentAmount += amountDelivered;

        // Clamp to max target
        if (quest.currentAmount > quest.targetAmount)
            quest.currentAmount = quest.targetAmount;

        quest.UpdateUI();

        if (quest.IsComplete())
        {
            Debug.Log($"Quest Completed: {questName}");
            // Optional: remove quest from UI or mark completed
        }
    }

    // Check if quest is complete
    public bool IsQuestComplete(string questName)
    {
        if (!activeQuests.ContainsKey(questName)) return false;
        return activeQuests[questName].IsComplete();
    }
}