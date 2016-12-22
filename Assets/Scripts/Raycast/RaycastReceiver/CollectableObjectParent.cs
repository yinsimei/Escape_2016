using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class CollectableObjectParent : RaycastReceiverParent
{
    override public void ClickActionParent()
    {
        CollectAction collectAction = transform.GetComponent<CollectAction>();
        if (collectAction != null)
        {
            collectAction.StartAction();
        }
    }
}