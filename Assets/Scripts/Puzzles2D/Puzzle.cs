using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class Puzzle : MonoBehaviour {

    public InteractiveObject[] objectsToUnlock;

    [HideInInspector]
    public bool m_bPuzzleSolved = false;
    public bool m_bPuzzleEnabled;

    // Use this for initialization
    virtual protected void Start ()
    {
        // Hide puzzle
        EnablePuzzle(false);
	}

    // Update is called once per frame
    virtual protected void Update () {

	}

    virtual protected void UnlockDoors()
    {
        foreach (InteractiveObject obj in objectsToUnlock)
        {
            obj.locked = false;
        }
    }

    virtual public void StartPuzzle()
    {
        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnUIShow", gameObject, SendMessageOptions.DontRequireReceiver);
        transform.GetComponentInParent<PuzzlesPanel>().BeginFadeIn();
        EnablePuzzle(true);
    }

    // End Puzzle : Congratulations and move to next step
    virtual public void EndPuzzle(bool p_Win = true)
    {
        m_bPuzzleSolved = p_Win;

        if (p_Win)
        {
            // Congratulations
            DialogueManager.ShowAlert("Puzzle résolu");


            // Trigger next event
            UnlockDoors();
        }

        // Hide Game board
        transform.GetComponentInParent<PuzzlesPanel>().BeginFadeOut();
        EnablePuzzle(false);

        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnUIHide", gameObject, SendMessageOptions.DontRequireReceiver);
    }

    // Check if Puzzle Complete
    virtual protected bool CheckPuzzleComplete()
    {
        return false;
    }

    // Enable/Disable puzzle
    virtual public void EnablePuzzle(bool enabled)
    {
        m_bPuzzleEnabled = enabled;
        GetComponent<CanvasGroup>().blocksRaycasts = enabled;
        if (enabled)
        {
            GetComponent<CanvasGroup>().alpha = 1f;
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = 0f;
        }
        
    }
}
