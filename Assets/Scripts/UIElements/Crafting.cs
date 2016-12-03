using UnityEngine;
using UnityEngine.Assertions;

public class Crafting : MonoBehaviour {

    public Transform ObjectOnPanel;
    public Transform HiddenObject;

    void Start()
    {
        Assert.IsNotNull(ObjectOnPanel);
        Assert.IsNotNull(HiddenObject);
    }

    public void Show()
    {
        GetComponent<Animator>().Play("Show");
    }

    public void Hide()
    {
        GetComponent<Animator>().Play("Hide");
    }
}
