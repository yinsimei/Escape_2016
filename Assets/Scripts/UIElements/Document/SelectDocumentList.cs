using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class SelectDocumentList : MonoBehaviour
{
    public DocumentList documentList;

    public void OnToggleChange()
    {
        bool isOn = transform.GetComponent<UnityEngine.UI.Toggle>().isOn;
        if (isOn)
        {
            documentList.Show();
        }
        else
        {
            documentList.Hide();
        }
            
    }
}