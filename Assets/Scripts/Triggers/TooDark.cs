using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.Assertions;

public class TooDark : MonoBehaviour {

    public float animationLength = 2.0f; 

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Start Monologue
            DialogueManager.StartConversation("TooDark");
            do
            {
                yield return null;
            } while (DialogueManager.IsConversationActive);

            // Start step back animation
            DialogueManager.Instance.gameObject.BroadcastMessage("OnConversationStart", this.transform);
            Animation animPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation>();
            Assert.IsNotNull(animPlayer);
            animPlayer.CrossFade("TooDark_StepBack", animationLength);
            do
            {
                yield return null;
            } while (animPlayer.isPlaying);
            DialogueManager.Instance.gameObject.BroadcastMessage("OnConversationEnd", this.transform);

        }
    }
}