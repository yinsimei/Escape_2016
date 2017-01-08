using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.SceneManagement;

public class ToBeContinued : MonoBehaviour
{
    public GameObject BlackOutMask;
    // To be continued
    public IEnumerator CloseScene()
    {
        DialogueManager.StartConversation("ToBeContinued");

        // Animation black out
        Animator blackMaskAnimator = BlackOutMask.GetComponent<Animator>();
        blackMaskAnimator.SetTrigger("BlackOut");

        do
        {
            yield return null;
        } while (DialogueManager.Instance.IsConversationActive);

        SceneManager.LoadScene("MainMenu");
    }
}