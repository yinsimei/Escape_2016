using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.Assertions;

public class IconActions : MonoBehaviour {

    public static IconActions instance;

    public float transitionTime;
    public CraftingSystem craftingSystem;
    public DocumentsSystem documentSystem;

    [HideInInspector]
    public bool m_bShown = false;

	void Start ()
    {
        // Singleton
        Assert.IsNull(instance);
        instance = this;

        // Hide all icons but List
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform icon = transform.GetChild(i);
            CanvasGroup canvasGroupIcon = icon.GetComponent<CanvasGroup>();
            if (canvasGroupIcon == null)
            {
                Debug.LogError("Icon " + icon.name + " doesn't have a canvas group");
            }

            if (icon.name == "List")
            {
                canvasGroupIcon.alpha = 1f;
                canvasGroupIcon.blocksRaycasts = true;
                canvasGroupIcon.interactable = true;
            }
            else if (icon.name == "Close" || icon.name == "Inventory" || icon.name == "Docs" || icon.name == "Menu")
            {
                canvasGroupIcon.alpha = 0f;
                canvasGroupIcon.blocksRaycasts = false;
                canvasGroupIcon.interactable = false;
            }
            else
            {
                Debug.LogError("Icon " + icon.name + " unknown");
            }
        }

        m_bShown = false;
    }

    public void ClickList()
    {
        Assert.IsFalse(m_bShown);
        m_bShown = true;

        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform icon = transform.GetChild(i);

            if (icon.name == "List")
            {
                ShowIcon(icon, false);
            }
            else if (icon.name == "Close" || icon.name == "Inventory" || icon.name == "Docs" || icon.name == "Menu")
            {
                ShowIcon(icon);
            }
            else
            {
                Debug.LogError("Icon " + icon.name + " unknown");
            }
        }

        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnUIShow", gameObject, SendMessageOptions.DontRequireReceiver);
    }

    public void ClickClose()
    {
        Assert.IsTrue(m_bShown);
        m_bShown = false;

        // Hide Icons
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform icon = transform.GetChild(i);

            if (icon.name == "List")
            {
                ShowIcon(icon);
            }
            else if (icon.name == "Close" || icon.name == "Inventory" || icon.name == "Docs" || icon.name == "Menu")
            {
                ShowIcon(icon, false);
            }
            else
            {
                Debug.LogError("Icon " + icon.name + " unknown");
            }
        }

        // Hide crafting panel
        craftingSystem.Hide();

        // Hide document panel
        documentSystem.Hide();

        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnUIHide", gameObject, SendMessageOptions.DontRequireReceiver);
    }

    public void ClickInventory()
    {
        // Hide document panel
        documentSystem.Hide();

        // Show crafting panel
        craftingSystem.Show();
    }

    public void ClickDocs()
    {
        // Hide inventory panel
        craftingSystem.Hide();

        // Show Document system
        documentSystem.Show();
    }

    public void ClickMenu()
    {
        // To call game menu
    }

    private void ShowIcon(Transform icon, bool show = true)
    {
        Animator animatorIcon = icon.GetComponent<Animator>();
        CanvasGroup canvasGroupIcon = icon.GetComponent<CanvasGroup>();
        if (show)
        {
            if (canvasGroupIcon.alpha != 0f)
                Debug.LogError("Icon " + icon.name + " already shown");
            animatorIcon.SetTrigger("Show");
        }
        else
        {
            if (canvasGroupIcon.alpha != 1f)
                Debug.LogError("Icon " + icon.name + " already hidden");
            animatorIcon.SetTrigger("Hide");
        }
    }
}
