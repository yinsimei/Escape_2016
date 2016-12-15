using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class RuntimeDialogueUIChange : MonoBehaviour
{
    public GameObject npcDialoguePanel;
    public UnityUIDialogueUI dialogueUI;

    void SetTextToCenter(string p_bTrue)
    {
        if (p_bTrue.Equals("true"))
            npcDialoguePanel.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        else
            npcDialoguePanel.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
    }

    void SetNpcAlwaysVisible(string p_bVisible)
    {
        dialogueUI.dialogue.npcSubtitle.alwaysVisible = p_bVisible.Equals("true");
    }

    void SetToPlayerType(string p_bTrue)
    {
        if (p_bTrue.Equals("true"))
        {
            UnityUITypewriterEffect effect = npcDialoguePanel.AddComponent<UnityUITypewriterEffect>();
            effect.charactersPerSecond = 20;
        }  
        else
        {
            Destroy(npcDialoguePanel.GetComponent<UnityUITypewriterEffect>());
        }
    }
}