using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class SetEnabledOnEvents : MonoBehaviour {

    public MonoBehaviour[] UIBehaviors;          // Icon button behavoirs
    public MonoBehaviour[] playerBehaviors;      // Player behaviors : raycast, mouselook, keyboard behaviors for UI, ...

    private bool m_bDialogue;  // When Dialogue, freeze all else
    private bool m_bUI;        // When UI, don't freeze Dialogue

    private CursorLockMode m_eCursorOldState;

    void Start ()
    {
        m_bDialogue = false;
        m_bUI = false;
    }

    public void OnConversationStart(Transform actor)
    {
        Assert.IsFalse(m_bUI);
        m_bDialogue = true;
        SetPlayerBehaviors(false);
        SetUIBehaviors(false);

        // Set UI Controller State
        UIController.instance.SetState(UIController.EControllerState.e_InConversation);
    }

    public void OnConversationEnd(Transform actor)
    {
        m_bDialogue = false;
        SetPlayerBehaviors(true);
        SetUIBehaviors(true);

        // Set UI Controller State
        UIController.instance.GoBackState();
    }

    public void OnUIShow()
    {
        if (m_bUI)
            return;

        Assert.IsFalse(m_bDialogue);

        m_bUI = true;
        SetPlayerBehaviors(false);

        // Set UI Controller State
        UIController.instance.SetState(UIController.EControllerState.e_UI);
    }

    public void OnUIHide()
    {
        Assert.IsTrue(m_bUI, "Get multiple times of UI Hide messages");
        m_bUI = false;
        if (!m_bDialogue)
        {
            SetPlayerBehaviors(true);
        }

        // Set UI Controller State
        UIController.instance.GoBackState();
    }

    public void OnInventoryShow()
    {
        Assert.IsFalse(m_bUI, "Inventory can't be called when UI is on");
        Assert.IsFalse(m_bDialogue, "Inventory can't be call when dialogue is on");

        SetPlayerBehaviors(false);
        SetUIBehaviors(false);

        // Set UI Controller State
        UIController.instance.SetState(UIController.EControllerState.e_InventoryBar);
    }

    public void OnInventoryHide()
    {
        SetPlayerBehaviors(true);
        SetUIBehaviors(true);

        // Set UI Controller State
        UIController.instance.GoBackState();
    }

    public void OnPuzzleStart()
    {
        Assert.IsFalse(m_bUI, "Puzzle can't be called when UI is on");
        Assert.IsFalse(m_bDialogue, "Puzzle can't be call when dialogue is on");

        SetPlayerBehaviors(false);
        SetUIBehaviors(false);

        // Set UI Controller State
        UIController.instance.SetState(UIController.EControllerState.e_InPuzzle);
    }

    public void OnPuzzleEnd()
    {
        SetPlayerBehaviors(true);
        SetUIBehaviors(true);

        // Set UI Controller State
        UIController.instance.GoBackState();
    }

    public void OnDocumentStart()
    {
        SetPlayerBehaviors(false);
        SetUIBehaviors(false);

        // Set UI Controller State
        UIController.instance.SetState(UIController.EControllerState.e_ReadingDoc);
    }

    public void OnDocumentEnd()
    {
        // Set UI Controller State
        UIController.instance.GoBackState();

        if (m_bUI)
            return;

        SetPlayerBehaviors(true);
        SetUIBehaviors(true);
    }

    private void SetPlayerBehaviors(bool enabled)
    {
        foreach (MonoBehaviour m in playerBehaviors)
        {
            m.enabled = enabled;
        }

        // Change Cursor
        Cursor.visible = !enabled;
        if (Cursor.visible)
        {
            m_eCursorOldState = Cursor.lockState;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = m_eCursorOldState;
        }
    }

    private void SetUIBehaviors(bool enabled)
    {
        foreach (MonoBehaviour i in UIBehaviors)
        {
            i.enabled = enabled;
        }
    }
}