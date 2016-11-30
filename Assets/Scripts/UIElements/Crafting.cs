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
}
