using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemName = "Cherry";
    public string itemName2 = "Herb";
    public int amount = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(itemName, amount);
                playerInventory.AddItem(itemName2, amount);
                Destroy(gameObject);
            }
        }
    }
}