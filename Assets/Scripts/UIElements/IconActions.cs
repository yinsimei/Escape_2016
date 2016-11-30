using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class IconActions : MonoBehaviour {

    public float transitionTime;
    public CraftingPanel inventoryPanel;
    public GameObject DocumentPanel;

	void Start () {
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
	}

    public void ClickList()
    {
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

        // Hide inventory panel
        inventoryPanel.Hide();

        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnUIHide", gameObject, SendMessageOptions.DontRequireReceiver);
    }

    public void ClickInventory()
    {
        // Show inventory panel
        inventoryPanel.Show();
    }

    public void ClickDocs()
    {
        // Hide inventory panel
        inventoryPanel.Hide();
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
            animatorIcon.Play("Show", 0, transitionTime);
        }
        else
        {
            if (canvasGroupIcon.alpha != 1f)
                Debug.LogError("Icon " + icon.name + " already hidden");
            animatorIcon.Play("Hide", 0, transitionTime);
        }
    }
}
