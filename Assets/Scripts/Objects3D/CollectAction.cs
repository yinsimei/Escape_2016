using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class CollectAction : MonoBehaviour
{

    public bool isCollectable = true;
    public string collectCoversation;
    public InventoryItem related2DItem;

    public IEnumerator StartAction()
    {
        if (!string.IsNullOrEmpty(collectCoversation))
        {
            DialogueManager.StartConversation(collectCoversation);
        }

        do
        {
            yield return null;
        } while (DialogueManager.IsConversationActive);

        if (isCollectable && related2DItem != null)
        {
            gameObject.SetActive(false);
            related2DItem.SetInInventory(true);
        }
    }
}
