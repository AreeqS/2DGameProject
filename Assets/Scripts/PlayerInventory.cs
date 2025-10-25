using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<string, int> items = new Dictionary<string, int>();
    public TextMeshProUGUI cherryText; // assign in inspector

    void Start()
    {
        // Initialize cherries if not present
        if (!items.ContainsKey("Cherry"))
            items["Cherry"] = 0;

        UpdateInventoryUI();
    }

    public bool HasItem(string itemName, int amount)
    {
        return items.ContainsKey(itemName) && items[itemName] >= amount;
    }

    public void RemoveItem(string itemName, int amount)
    {
        if (!HasItem(itemName, amount)) return;

        items[itemName] -= amount;
        if (items[itemName] < 0) items[itemName] = 0;

        UpdateInventoryUI();
    }

    public void AddItem(string itemName, int amount)
    {
        if (!items.ContainsKey(itemName))
            items[itemName] = 0;

        items[itemName] += amount;
        QuestManager.Instance.UpdateQuestProgress("Cherry", 25);
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        if (cherryText != null)
        {
            int count = items.ContainsKey("Cherry") ? items["Cherry"] : 0;
            cherryText.text =  count.ToString();
        }
    }
}