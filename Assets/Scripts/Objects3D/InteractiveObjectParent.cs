using UnityEngine;
using System.Collections;

public class InteractiveObjectParent : RaycastReceiverParent
{
    public override void ClickActionParent()
    {
        UnlockAction unlockAction = transform.GetComponent<UnlockAction>();
        if (unlockAction == null || !unlockAction.isLocked)
        {
            foreach (InteractiveObject obj in transform.GetComponentsInChildren<InteractiveObject>())
            {
                obj.ClickAnimation();
            }
        }
        else
        {
            if (unlockAction != null)
            {
                StartCoroutine(unlockAction.StartAction());
            }
        }
    }
}