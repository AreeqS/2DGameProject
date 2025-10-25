using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    [TextArea(2, 5)]
    public string text;
}

public class NPCQuest : MonoBehaviour
{
    [Header("NPC Info")]
    public string npcName = "Master Bear";

    [Header("Initial Requirement")]
    public string requiredItem = "Cherry";
    public int requiredAmount = 3;

    [Header("Second Quest Requirement")]
    public string questItem1 = "Cherry";
    public int questAmount1 = 50;
    public string questItem2 = "Herb";
    public int questAmount2 = 5;

    [Header("Dialogue Lines")]
    public DialogueLine[] dialogueLines;          // Normal conversation
    [TextArea(2, 3)] public string notEnoughText = "You need more cherries to talk to me!";
    [TextArea(2, 3)] public string questAssignedText = "Bring me 50 cherries and 5 herbs.";
    [TextArea(2, 3)] public string questCompleteText = "Great work! You brought everything I asked for.";
    [TextArea(2, 3)] public string goodbyeText = "Goodbye!";

    [Header("UI Setup")]
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    private PlayerInventory playerInventory;
    private bool playerInRange = false;
    private bool isTalking = false;
    private bool npcUnlocked = false; // after paying first cherries
    private bool questAssigned = false;
    private bool questCompleted = false;

    private DialogueLine[] currentDialogue;
    private int currentLineIndex = 0;

    void Start()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isTalking)
                StartConversation();
            else
                ContinueDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<PlayerInventory>();
            playerInRange = true;
            ShowText("Press E to talk");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerInventory = null;
            isTalking = false;
            currentLineIndex = 0;
            HideText();
        }
    }

    void ShowText(string message)
    {
        if (dialogueText != null && dialogueBox != null)
        {
            dialogueText.text = message;
            dialogueBox.SetActive(true);
        }
    }

    void HideText()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);
    }

    void StartConversation()
    {
        if (playerInventory == null) return;

        isTalking = true;

        // Stage 1: NPC locked, requires initial cherries
        if (!npcUnlocked)
        {
            if (playerInventory.HasItem(requiredItem, requiredAmount))
            {
                playerInventory.RemoveItem(requiredItem, requiredAmount);
                npcUnlocked = true;
                ShowText($"{npcName}: Thanks! You may now speak with me.");
                currentDialogue = dialogueLines;
                currentLineIndex = 0;
            }
            else
            {
                ShowText($"{npcName}: {notEnoughText}");
                currentDialogue = null;
            }
            return;
        }

        // Stage 2: Quest assigned but not completed
        if (npcUnlocked && questAssigned && !questCompleted)
        {
            if (playerInventory.HasItem(questItem1, questAmount1) &&
                playerInventory.HasItem(questItem2, questAmount2))
            {
                playerInventory.RemoveItem(questItem1, questAmount1);
                playerInventory.RemoveItem(questItem2, questAmount2);
                questCompleted = true;
                ShowText($"{npcName}: {questCompleteText}");
            }
            else
            {
                ShowText($"{npcName}: You're still on the quest. Bring 50 cherries and 5 herbs.");
            }
            return;
        }

        // Stage 3: NPC unlocked, quest not yet assigned
        if (npcUnlocked && !questAssigned)
        {
            questAssigned = true;
            ShowText($"{npcName}: {questAssignedText}");
            return;
        }

        // Stage 4: All done
        if (npcUnlocked && questCompleted)
        {
            ShowText($"{npcName}: {goodbyeText}");
        }
    }

    void ContinueDialogue()
    {
        if (!isTalking || currentDialogue == null) return;

        currentLineIndex++;
        ShowNextLine();
    }

    void ShowNextLine()
    {
        if (currentDialogue == null || currentLineIndex >= currentDialogue.Length)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = currentDialogue[currentLineIndex];
        ShowText($"{line.speaker}: {line.text}");
    }

    void EndDialogue()
    {
        isTalking = false;
        currentDialogue = null;
        currentLineIndex = 0;
    }
}