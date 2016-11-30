using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class TheBegining : MonoBehaviour {
    
    public GameObject BlackInMask;

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DialogueManager.Instance.gameObject.BroadcastMessage("OnConversationStart", this.transform);

            Animator blackMaskAnimator = BlackInMask.GetComponent<Animator>();
            blackMaskAnimator.Play("BlackIn");

            //do
            //{
            //    yield return null;
            //}
            //while (blackMaskAnimator.GetCurrentAnimatorStateInfo(0).IsName("BlackIn"));

            Animation anim = other.transform.GetComponent<Animation>();
            anim.Play("TheBeginning_GetUp");
            do
            {
                yield return null;
            } while (anim.IsPlaying("TheBeginning_GetUp"));
            
            DialogueManager.StartConversation("TheBeginning");
            Destroy(this.gameObject);
        }
    }
}
