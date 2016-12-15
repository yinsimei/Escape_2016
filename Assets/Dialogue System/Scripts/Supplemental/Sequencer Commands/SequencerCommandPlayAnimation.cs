using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandPlayAnimation : SequencerCommand
    {

        public void Start()
        {
            string objectTag = GetParameter(0);
            string animationName = GetParameter(1);
            float animationSpeed = GetParameterAsFloat(2);

            if (string.IsNullOrEmpty(objectTag))
                objectTag = "Player";

            GameObject obj = GameObject.FindGameObjectWithTag(objectTag);
            if (obj == null)
            {
                Debug.Log("GameObject unfound: " + objectTag);
                Stop();
            }

            if (string.IsNullOrEmpty(animationName))
            {
                Debug.LogError("Non animation name entered " + objectTag);
                Stop();
            }

            
            Animation anim = obj.GetComponent<Animation>();
            if (anim == null)
            {
                Debug.Log("Animation unfound in " + objectTag);
                Stop();
            }

            StartCoroutine(StartAnimation(anim, animationName, animationSpeed));

            Stop();
        }

        IEnumerator StartAnimation(Animation p_anim, string p_sAnimName, float p_fSpeed)
        {
            p_anim[p_sAnimName].speed = p_fSpeed;
            p_anim.Play(p_sAnimName);
            do
            {
                yield return null;
            } while (p_anim.IsPlaying(p_sAnimName));
            DialogueManager.Instance.gameObject.BroadcastMessage("OnConversationEnd", this.transform);
        }
    }

}