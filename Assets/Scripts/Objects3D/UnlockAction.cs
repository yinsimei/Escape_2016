using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class UnlockAction : MonoBehaviour
{
    public bool isLocked = true;
    public string conversationWhenLocked;
    public InventoryItem unlockItem;
    public string dialogueVar;

    private Inventory m_inventory;

    [HideInInspector]
    public Puzzle3D m_lockedByPuzzle;

    void Start()
    {
        m_inventory = GameObject.FindGameObjectWithTag("UICanvas").transform.GetComponentInChildren<Inventory>();
        Assert.IsNotNull(m_inventory);
    }

    public void StartAction()
    {
        StartCoroutine(Action());
    }

    private IEnumerator Action()
    {
        if (!string.IsNullOrEmpty(conversationWhenLocked))
        {
            // Say object locked
            DialogueManager.StartConversation(conversationWhenLocked);

            if (m_lockedByPuzzle != null)
                m_lockedByPuzzle.StartHighlight();
        }

        do
        {
            yield return null;
        } while (DialogueManager.IsConversationActive);

        if (m_lockedByPuzzle != null)
        {
            m_lockedByPuzzle.EndHighlight();
        }

        if (unlockItem != null)
        {
            if (string.IsNullOrEmpty(dialogueVar ) || DialogueLua.GetVariable(dialogueVar).AsBool)
            {
                m_inventory.ShowInventory(this);
            }
        }
    }

    public void Unlock()
    {
        Assert.IsTrue(isLocked);
        isLocked = false;
    }
}
