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
            string speedStr = GetParameter(2);
            float animationSpeed = 1f;

            if (!string.IsNullOrEmpty(speedStr))
                animationSpeed = float.Parse(speedStr);

            if (string.IsNullOrEmpty(objectTag))
                objectTag = "Player";

            GameObject obj = GameObject.FindGameObjectWithTag(objectTag);
            if (obj == null)
            {
                Debug.Log("GameObject unfound: " + objectTag);
                Stop();
            }

            Animation anim = obj.GetComponent<Animation>();
            if (anim == null)
            {
                Debug.Log("Animation unfound in " + objectTag);
                Stop();
            }

            if (string.IsNullOrEmpty(animationName))
            {
                anim[""].speed = animationSpeed;
                anim.Play();
            }
            else
            {
                anim[animationName].speed = animationSpeed;
                anim.Play(animationName);
            }

            Stop();
        }

    }

}