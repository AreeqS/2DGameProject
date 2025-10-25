using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questName;
    public string description;
    public string requiredItem;
    public int requiredAmount;
    public int currentAmount;
    public bool isCompleted;

    public Quest(string name, string desc, string item, int amount)
    {
        questName = name;
        description = desc;
        requiredItem = item;
        requiredAmount = amount;
        currentAmount = 0;
        isCompleted = false;
    }

    public void AddProgress(int amount)
    {
        if (isCompleted) return;

        currentAmount += amount;
        if (currentAmount >= requiredAmount)
        {
            currentAmount = requiredAmount;
            isCompleted = true;
        }
    }

    public string GetProgressText()
    {
        return $"{questName}: {currentAmount} / {requiredAmount}";
    }
}