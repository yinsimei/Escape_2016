using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class ReadableObject : RaycastReceiver
{
    public GameObject RelatedReport;

    public override void ClickAction()
    {
        // Show report


        // Play read sound
        SoundManager.instance.Play("readPaper");        
    }

    void OnReadEnd()
    {
        // Hide game Object
        gameObject.SetActive(false);
        SoundManager.instance.Play("collectPaper");
        
        // Collect report into journal
    }
}
