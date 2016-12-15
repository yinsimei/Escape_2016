using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.Assertions;

public class Puzzle : MonoBehaviour {

    public string endAnimationTrigger = "End";
    public UnlockAction[] unlockActions;

    [HideInInspector]
    public bool m_bPuzzleSolved = false;

    // Use this for initialization
    virtual protected void Start ()
    {
        // Unset this to current puzzle
        SetCurrentPuzzle(false);
	}

    // Update is called once per frame
    virtual protected void Update () {

	}

    // Unlock objects by solving this puzzle
    private void UnlockObjects()
    {
        foreach (UnlockAction unlock in unlockActions)
        {
            unlock.Unlock();
        }
    }

    // Start Puzzle : Show puzzle panel & set this puzzle to current puzzle
    virtual public void StartPuzzle()
    {
        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnPuzzleStart", this.transform);
        // Show puzzle panel
        transform.GetComponentInParent<PuzzlesPanel>().BeginFadeIn();
        // Set this puzzle to current puzzle
        SetCurrentPuzzle(true);
    }

    // End Puzzle : Congratulations and move to next step
    virtual public void EndPuzzle(bool p_Win = true)
    {
        m_bPuzzleSolved = p_Win;

        if (p_Win)
        {
            StartCoroutine(EndPuzzleAnimation());
        }
        else
        {
            HidePuzzle();
        }
}

    // Play end animation
    private IEnumerator EndPuzzleAnimation()
    {
        Animator animator = GetComponent<Animator>();
       
        if (animator != null)
        {
            animator.SetTrigger(endAnimationTrigger);

            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        // Congratulations
        DialogueManager.ShowAlert("Puzzle résolu");

        // Trigger next event
        UnlockObjects();

        // Hide puzzle
        HidePuzzle();
    }

    // Hide puzzle
    private void HidePuzzle()
    {
        // Hide Game board
        transform.GetComponentInParent<PuzzlesPanel>().BeginFadeOut();
        SetCurrentPuzzle(false);

        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnPuzzleEnd", this.transform);
    }

    // Set this puzzle to current puzzle on panel
    private void SetCurrentPuzzle(bool enabled)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = enabled;
        GetComponent<CanvasGroup>().interactable = enabled;
        if (enabled)
            GetComponent<CanvasGroup>().alpha = 1f;
        else
            GetComponent<CanvasGroup>().alpha = 0f;
    }
}
