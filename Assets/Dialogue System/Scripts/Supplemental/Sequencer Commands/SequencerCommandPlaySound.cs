using UnityEngine;
using System.Collections;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandPlaySound : SequencerCommand
    {
        public void Start()
        {
            string soundName = GetParameter(0);
            SoundManager.instance.Play(soundName);
            Stop();
        }
    }
}