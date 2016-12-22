using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class RuntimeDialogueUIChange : MonoBehaviour
{
    public GameObject npcSubtitleLine;
    public UnityUIContinueButtonFastForward npcContinuousButton;
    public UnityUIDialogueUI dialogueUI;

    void SetTextToCenter(string p_bTrue)
    {
        if (p_bTrue.Equals("true"))
            npcSubtitleLine.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        else
            npcSubtitleLine.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
    }

    void SetNpcAlwaysVisible(string p_bVisible)
    {
        dialogueUI.dialogue.npcSubtitle.alwaysVisible = p_bVisible.Equals("true");
    }

    void SetToPlayerType(string p_bTrue)
    {
        if (p_bTrue.Equals("true"))
        {
            UnityUITypewriterEffect effect = npcSubtitleLine.AddComponent<UnityUITypewriterEffect>();
            npcContinuousButton.typewriterEffect = effect;
            effect.charactersPerSecond = 20;
        }  
        else
        {
            npcContinuousButton.typewriterEffect = null;
            Destroy(npcSubtitleLine.GetComponent<UnityUITypewriterEffect>());
        }
    }
}