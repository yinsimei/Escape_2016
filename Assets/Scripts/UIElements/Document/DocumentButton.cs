using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class DocumentButton : MonoBehaviour
{
    private Document relatedDoc = null;

    public void SetDoc(Document p_doc)
    {
        Assert.IsNull(relatedDoc);
        relatedDoc = p_doc;
        transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = relatedDoc.Title;
    }

    public void OpenDoc()
    {
        Assert.IsNotNull(relatedDoc);
        relatedDoc.OpenDocument();
    }
}
