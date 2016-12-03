using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class UnlockAction : MonoBehaviour
{
    public bool isLocked = true;
    public string conversationWhenLocked;
    public InventoryItem unlockItem;

    private Inventory m_inventory;

    void Start()
    {
        m_inventory = GameObject.FindGameObjectWithTag("UICanvas").transform.GetComponentInChildren<Inventory>();
        Assert.IsNotNull(m_inventory);
    }

    public IEnumerator StartAction()
    {
        if (!string.IsNullOrEmpty(conversationWhenLocked))
        {
            // Say object locked
            DialogueManager.StartConversation(conversationWhenLocked);
        }

        do
        {
            yield return null;
        } while (DialogueManager.IsConversationActive);

        if (unlockItem != null)
        {
            m_inventory.ShowItems(this);
        }
    }

    public void Unlock()
    {
        Assert.IsTrue(isLocked);
        isLocked = false;
    }
}
