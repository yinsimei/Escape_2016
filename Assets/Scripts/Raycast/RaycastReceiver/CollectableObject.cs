using UnityEngine;
using System.Collections;

public class CollectableObject : RaycastReceiver
{
    override public void ClickAction()
    {
        CollectAction collectAction = transform.GetComponent<CollectAction>();
        if (collectAction != null)
        {
            collectAction.StartAction();
        }
    }
}