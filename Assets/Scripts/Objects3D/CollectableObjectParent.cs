using UnityEngine;
using System.Collections;

public class CollectableObjectParent : RaycastReceiverParent
{
    public bool isCollectable = true;
    public string collectCoversation;
    public InventoryItem related2DItem;
}