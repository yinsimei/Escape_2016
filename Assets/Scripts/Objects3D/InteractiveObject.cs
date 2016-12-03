using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class InteractiveObject : RaycastReceiver
{
    public string triggerNameWhenClick = "Open";

    override public void ClickAction()
    {
        UnlockAction unlockAction = transform.GetComponent<UnlockAction>();
        if (unlockAction == null || !unlockAction.isLocked)
        {
            ClickAnimation();
        }
        else
        {
            if (unlockAction != null)
            {
                StartCoroutine(unlockAction.StartAction());
            }
        }
    }

    public void ClickAnimation()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator != null && !string.IsNullOrEmpty(triggerNameWhenClick))
        {
            bool trigger = animator.GetBool(triggerNameWhenClick);
            animator.SetBool(triggerNameWhenClick, !trigger);
        }
    }
}