using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class PuzzleCell : MonoBehaviour, IDropHandler {

    public int m_Pos;

    public GameObject puzzle
    {
        get {
            if (transform.childCount > 0)
                return transform.GetChild(0).gameObject;
        
             return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {

    }
}