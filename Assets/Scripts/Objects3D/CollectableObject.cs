using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class CollectableObject : RaycastReceiver
{
    public bool isCollectable = true;
    public string collectCoversation;
    public InventoryItem related2DItem;

    private CollectableObjectParent m_objectsParent;

    protected override void Start()
    {
        base.Start();
        m_objectsParent = transform.GetComponentInParent<CollectableObjectParent>();
        if (m_objectsParent != null)
        {
            collectCoversation = m_objectsParent.collectCoversation;
            related2DItem = m_objectsParent.related2DItem;
            isCollectable = m_objectsParent.isCollectable;
        }
    }


    override public void ClickAction()
    {
        StartCoroutine(StartConversation());
    }

    private IEnumerator StartConversation()
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
            if (m_objectsParent == null)
            {
                gameObject.SetActive(false);
            }
            else
            {
                m_objectsParent.transform.gameObject.SetActive(false);
            }
            related2DItem.SetInInventory(true);
        }
    }
}
