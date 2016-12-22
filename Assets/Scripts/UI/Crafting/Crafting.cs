using UnityEngine;
using UnityEngine.Assertions;

public class Crafting : MonoBehaviour {

    public Transform ObjectOnPanel;
    public Transform HiddenObject;
    [HideInInspector]
    public CraftingText m_craftingText;

    void Start()
    {
        Assert.IsNotNull(ObjectOnPanel);
        Assert.IsNotNull(HiddenObject);
        m_craftingText = GetComponentInChildren<CraftingText>();
        Assert.IsNotNull(m_craftingText);
    }

    public void Show()
    {
        GetComponent<Animator>().SetTrigger("Show");
    }

    public void Hide()
    {
        GetComponent<Animator>().SetTrigger("Hide");
    }
}
