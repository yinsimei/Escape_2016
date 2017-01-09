using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.SceneManagement;

public class ToBeContinued : MonoBehaviour
{
    public GameObject BlackOutMask;
    public Credits credits;
    // To be continued

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            DialogueManager.Instance.gameObject.BroadcastMessage("OnEventAnimationStart", this.transform);
            AudioSource source1 = SoundManager.instance.Play("cry");
            AudioSource source2 = SoundManager.instance.Play("slamDoor");
            SoundManager.instance.Play("cryLong");
            do
            {
                yield return null;
            } while (source1.isPlaying || source2.isPlaying);
            DialogueManager.Instance.gameObject.BroadcastMessage("OnEventAnimationEnd", this.transform);

            // Start monologue
            DialogueManager.StartConversation("ToBeContinued");

            do
            {
                yield return null;
            } while (DialogueManager.Instance.IsConversationActive);

            // Animation black out
            DialogueManager.Instance.gameObject.BroadcastMessage("OnEventAnimationStart", this.transform);
            Animator blackMaskAnimator = BlackOutMask.GetComponent<Animator>();
            blackMaskAnimator.SetTrigger("ToBeContinued");
            yield return new WaitForSeconds(blackMaskAnimator.GetCurrentAnimatorStateInfo(0).length);

            // Play Credits
            credits.Play();
            DialogueManager.Instance.gameObject.BroadcastMessage("OnEventAnimationEnd", this.transform);
        }
    }
}