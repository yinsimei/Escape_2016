using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DocumentList : MonoBehaviour {

    public Document[] documents;
    public Object prefabButton;

    [HideInInspector]
    public GameObject m_relatedButton;

    private Dictionary<Document, GameObject> m_DocToButton = new Dictionary<Document, GameObject>();


    void Start()
    {
        // Create Button in a dynamic way
        foreach (Document doc in documents)
        {
            // Create and set Button
            GameObject button = (GameObject)Instantiate(prefabButton);
            button.transform.SetParent(transform, false);
            button.GetComponent<DocumentButton>().SetDoc(doc);

            // Set document
            doc.m_docList = this;

            // Add to dictionary
            m_DocToButton.Add(doc, button);

            // Set button to false
            button.SetActive(false);
        }

        GetComponent<UnityEngine.UI.VerticalLayoutGroup>().enabled = true;
    }

    public void Show()
    {
        GetComponent<Animator>().SetTrigger("Show");
    }

    public void Hide()
    {
        GetComponent<Animator>().SetTrigger("Hide");
    }

    public void FindDocument(Document doc)
    {
        m_DocToButton[doc].SetActive(true);
    }
}