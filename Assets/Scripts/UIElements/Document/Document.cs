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

    private int m_nCurrentPage; 

    void Start()
    {
        m_nCurrentPage = 0;

        if (pages.Length == 1)
        {
            buttonLastPage.SetActive(false);
            buttonNextPage.SetActive(false);
        }
    }

    public void LastPage()
    {
        --m_nCurrentPage;
        Assert.IsTrue(m_nCurrentPage >= 0, "It's already the first page");
        StartCoroutine(PageTransition(pages[m_nCurrentPage]));

        // Set Buttons
        if (m_nCurrentPage == 0)
            buttonLastPage.SetActive(false);
        buttonNextPage.SetActive(true);
    }

    public void NextPage()
    {
        ++m_nCurrentPage;
        if (m_nCurrentPage >= pages.Length)
        {
            CloseDocument();
            return;
        }

        StartCoroutine(PageTransition(pages[m_nCurrentPage]));

        // Set Buttons
        if (m_nCurrentPage == pages.Length - 1)
            buttonNextPage.SetActive(false);
        buttonLastPage.SetActive(true);
    }

    public void OpenDocument()
    {
        Assert.IsNull(currentDoc);
        currentDoc = this;

        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnDocumentStart", this.transform);
    }

    public void CloseDocument()
    {
        currentDoc = null;

        // Broadcast
        DialogueManager.Instance.gameObject.BroadcastMessage("OnDocumentEnd", this.transform);
    }

    public void OnPointerClick(PointerEventData eventData)
    { 
        NextPage();
    }

    private IEnumerator PageTransition(GameObject nextPage)
    {
        GetComponent<Animator>().SetTrigger("Hide");
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        nextPage.GetComponent<Animator>().SetTrigger("Show");
    }
}