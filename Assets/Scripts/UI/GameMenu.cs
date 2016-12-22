using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using PixelCrushers.DialogueSystem;

public class GameMenu : MonoBehaviour, IPointerClickHandler
{
    public static GameMenu instance;

    void Start()
    {
        if (instance != null)
        {
            Debug.LogError("Game menu has already been instantiated!");
        } 
        instance = this;
    }

    public void Show()
    {
        // Show
        GetComponent<Animator>().SetTrigger("Show");

        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnGameMenuShow", this.transform);
    }

    public void Hide()
    {
        // Hide
        GetComponent<Animator>().SetTrigger("Hide");

        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnGameMenuHide", this.transform);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Hide();
    }
}