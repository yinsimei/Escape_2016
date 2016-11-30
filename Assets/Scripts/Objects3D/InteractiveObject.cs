using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class InteractiveObject : RaycastReceiver
{ 

    public bool locked = false;
    public string triggerNameWhenClick = "Open";
    public string conversationWhenLocked;

    override public void ClickAction()
    {
        if (!locked)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            if (animator != null && !string.IsNullOrEmpty(triggerNameWhenClick))
            {
                bool trigger = animator.GetBool(triggerNameWhenClick);
                animator.SetBool(triggerNameWhenClick, !trigger);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(conversationWhenLocked))
            {
                // Say object locked
                DialogueManager.StartConversation(conversationWhenLocked);
            }
        }
    }
}
