using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Inventory : MonoBehaviour {

    public Transform inventoryCells;
    public Transform invisibleRoot;

    [HideInInspector]
    public int m_nItemNb;

	// Use this for initialization
	void Start ()
    {
        m_nItemNb = 0;
        Assert.IsNotNull(inventoryCells);
        Assert.IsNotNull(invisibleRoot);
    }
}
