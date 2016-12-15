using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class InventoryCells : MonoBehaviour
{
    private Animator m_anim;
    void Start()
    {
        m_anim = GetComponent<Animator>();
        Assert.IsNotNull(m_anim);
    }

    public void Show()
    {
        GetComponentInParent<Inventory>().m_eMode = Inventory.EInventoryMode.e_Crafting;
        GetComponent<CanvasGroup>().ignoreParentGroups = true;
        m_anim.SetTrigger("Show");
    }

    public void Hide()
    {
        StartCoroutine(HideAnim());
    }

    private IEnumerator HideAnim()
    {
        m_anim.SetTrigger("Hide");
        yield return new WaitForSeconds(m_anim.GetCurrentAnimatorStateInfo(0).length); 
        GetComponent<CanvasGroup>().ignoreParentGroups = false;
    }
}
