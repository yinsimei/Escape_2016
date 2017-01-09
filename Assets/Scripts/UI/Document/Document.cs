using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using PixelCrushers.DialogueSystem;

public class Document : MonoBehaviour, IPointerClickHandler
{
    public static Document currentDoc = null;

    public GameObject[] pages;
    public GameObject buttonLastPage;
    public GameObject buttonNextPage;

    public UnityEngine.UI.Text titleText;

    public string Title
    {
        get { return titleText.text; }
    }

    [HideInInspector]
    public DocumentList m_docList;

    private int m_nCurrentPage; 

    void Start()
    {
        m_nCurrentPage = 0;

        if (pages.Length == 1)
        {
            if (buttonLastPage != null)
                buttonLastPage.SetActive(false);
            if (buttonNextPage != null)
                buttonNextPage.SetActive(false);
        }
    }

    public void LastPage()
    {
        int nNewPage = m_nCurrentPage - 1;
        Assert.IsTrue(nNewPage >= 0, "It's already the first page");

        ChangePage(pages[m_nCurrentPage], pages[nNewPage]);

        // Set Buttons
        if (nNewPage == 0)
            if (buttonLastPage != null)
                buttonLastPage.SetActive(false);
        if (buttonNextPage != null)
            buttonNextPage.SetActive(true);

        m_nCurrentPage = nNewPage;
    }

    public void NextPage()
    {
        int nNewPage = m_nCurrentPage + 1;
        if (nNewPage >= pages.Length)
        {
            CloseDocument();
            return;
        }

        ChangePage(pages[m_nCurrentPage], pages[nNewPage]);

        // Set Buttons
        if (nNewPage == pages.Length - 1)
            if (buttonNextPage != null)
                buttonNextPage.SetActive(false);
        if (buttonLastPage != null)
            buttonLastPage.SetActive(true);

        m_nCurrentPage = nNewPage;
    }

    public void OpenDocument()
    {
        Assert.IsNull(currentDoc);
        currentDoc = this;

        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnDocumentStart", this.transform);

        // Open Document
        GetComponent<Animator>().SetTrigger("Show");

        // First Page
        m_nCurrentPage = 0;
        pages[m_nCurrentPage].GetComponent<Animator>().SetTrigger("Show");
        if (buttonLastPage != null)
            buttonLastPage.SetActive(false);
        if (buttonNextPage != null && pages.Length > 1)
        {
            buttonNextPage.SetActive(true);
        }
    }

    public void CloseDocument()
    {
        StartCoroutine(CloseDoc());
    }

    private IEnumerator CloseDoc()
    {
        // Close Document
        GetComponent<Animator>().SetTrigger("Hide");

        // Close Last Page
        pages[pages.Length - 1].GetComponent<Animator>().SetTrigger("Hide");

        // Wait for end of animation
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        // Set currentDoc to null
        currentDoc = null;

        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnDocumentEnd", this.transform);
    }

    public void OnPointerClick(PointerEventData eventData)
    { 
        NextPage();
    }

    private void ChangePage(GameObject oldPage, GameObject newPage)
    {
        // Play sound
        SoundManager.instance.Play("readPaper");
        // Hide this page
        oldPage.GetComponent<Animator>().SetTrigger("Hide");
        // Show next page
        newPage.GetComponent<Animator>().SetTrigger("Show");
    }

    public void Find()
    {
        m_docList.FindDocument(this);
    }
}