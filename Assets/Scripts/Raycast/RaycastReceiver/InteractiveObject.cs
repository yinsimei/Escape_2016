using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class InteractiveObject : RaycastReceiver
{
    public AudioSource openSound;
    public AudioSource closeSound;

    private bool m_bOpen = false;
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
                unlockAction.StartAction();
            }
        }
    }

    public void ClickAnimation()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator != null)
        {
            m_bOpen = !m_bOpen;
            if (m_bOpen)
            {
                animator.SetTrigger("Open");
                if (openSound != null)
                    openSound.Play();
            }
            else
            {
                animator.SetTrigger("Close");
                if (closeSound != null)
                    closeSound.Play();
            }
        }
    }
}