using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class HorrorEffects : MonoBehaviour {

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
            DialogueManager.StartConversation("HorrorEffects");

            // To be continued
            GetComponent<ToBeContinued>().CloseScene();

            // Destroy the trigger
            //Destroy(this.gameObject);
        }
    }
}