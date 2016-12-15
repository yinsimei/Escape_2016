using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class ReadableObject : RaycastReceiver
{
    public Document RelatedDoc;

    override protected void Start()
    {
        base.Start();

        // ignore parent
        ignoreParent = true;
    }

    public override void ClickAction()
    {
        // Show report
        RelatedDoc.OpenDocument();

        // Play read sound
        SoundManager.instance.Play("readPaper");

        StartCoroutine(WaitForDocEnd());    
    }

    private IEnumerator WaitForDocEnd()
    {
        // Wait for the end of document
        do
        {
            yield return null;
        } while (Document.currentDoc != null);

        // Hide game Object
        gameObject.SetActive(false);
        SoundManager.instance.Play("collectPaper");

        // Collect report into journal
        RelatedDoc.Find();
    }
}
