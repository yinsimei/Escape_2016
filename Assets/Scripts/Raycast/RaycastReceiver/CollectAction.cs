using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class CollectAction : MonoBehaviour
{
    public string collectCoversation;
    public InventoryItem related2DItem;
    public string collectSoundName = "collectSound";
    public string dialogVar;

    public void StartAction()
    {
        StartCoroutine(Collect());
    }

    private IEnumerator Collect()
    {
        if (!string.IsNullOrEmpty(collectCoversation))
        {
            DialogueManager.StartConversation(collectCoversation);

        }

        do
        {
            yield return null;
        } while (DialogueManager.IsConversationActive);
 
        if (related2DItem != null)
        {
            if (string.IsNullOrEmpty(dialogVar) || DialogueLua.GetVariable(dialogVar).AsBool)
            {
                if (!string.IsNullOrEmpty(collectSoundName))
                {
                    SoundManager.instance.Play(collectSoundName);
                }
                gameObject.SetActive(false);
                related2DItem.SetInInventory(true);
            }
        }
    }
}