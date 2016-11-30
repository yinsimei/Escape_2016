using System;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using System.Collections;

public class SetEnabledOnEvents : MonoBehaviour {

    public MonoBehaviour[] UIBehaviors;
    public MonoBehaviour[] playerBehaviors;

    private bool m_OnDialogue;  // When Dialogue, freeze all else
    private bool m_OnUI;        // When UI, don't freeze Dialogue
    
    void Start ()
    {
        m_OnDialogue = false;
        m_OnUI = false;
    }

    public void OnConversationStart(Transform actor)
    {
        m_OnDialogue = true;
        SetPlayerBehaviors(false);
        SetUIBehaviors(false);
    }

    public void OnConversationEnd(Transform actor)
    {
        m_OnDialogue = false;
        if (!m_OnUI)
        {
            SetPlayerBehaviors(true);
        }
    }

    public void OnUIShow()
    {
        m_OnUI = true;
        SetPlayerBehaviors(false);
        SetUIBehaviors(false);
    }

    public void OnUIHide()
    {
        m_OnUI = false;
        if (!m_OnDialogue)
        {
            SetPlayerBehaviors(true);
        }
    }

    private void SetPlayerBehaviors(bool enabled)
    {
        foreach (MonoBehaviour m in playerBehaviors)
        {
            m.enabled = enabled;
        }
    }

    private void SetUIBehaviors(bool enabled)
    {
        foreach (MonoBehaviour m in UIBehaviors)
        {
            m.enabled = enabled;
        }
    }
}
