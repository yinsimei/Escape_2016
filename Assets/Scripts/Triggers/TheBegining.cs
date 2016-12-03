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

            // Animation black in
            Animator blackMaskAnimator = BlackInMask.GetComponent<Animator>();
            blackMaskAnimator.Play("BlackIn");

            // Animation turn camera
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
