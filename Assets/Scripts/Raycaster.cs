﻿using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class Raycaster : MonoBehaviour {

    public float maxDistance = 7f;

    
    private GameObject m_receiver = null;
    private RaycastReceiver m_raycastReceiver = null;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        bool foundRaycastReceiver = false;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider != null)
            {
                // Call OnRayEnter(), OnRayExit() of receiver;
                if (m_receiver == hit.collider.gameObject)
                {
                    if (m_raycastReceiver != null)
                    {
                        foundRaycastReceiver = true;
                        RayHitTarget(hit.distance);
                    }
                }
                else
                {
                    RayExitTarget(m_receiver, hit.collider.gameObject);
                    m_raycastReceiver = hit.collider.transform.GetComponent<RaycastReceiver>();
                    m_receiver = hit.collider.gameObject;
                    if (m_raycastReceiver != null)
                    {
                        m_raycastReceiver.OnRayHit(hit.distance);
                        foundRaycastReceiver = true;
                    }
                }
            }
        }

        if (!foundRaycastReceiver)
        {
            DeselectTarget();
        }

        // When mouse clicked, see if the distance is good for interaction
        if (Input.GetMouseButtonDown(0))
        {
            ClickTarget();
        }
    }

    private void DeselectTarget()
    {
        m_raycastReceiver = null;
        m_receiver = null;
    }

    private void ClickTarget()
    {
        if (m_raycastReceiver != null )
        {
            if (m_raycastReceiver.IsClickable)
            {
                if (m_raycastReceiver.receiverParent != null)
                {
                    m_raycastReceiver.receiverParent.ClickActionParent();
                }
                else
                {
                    m_raycastReceiver.ClickAction();
                }
            }
            else
            {
                if (m_raycastReceiver.isInteractable())
                {
                    DialogueManager.ShowAlert("Object trop loin");
                }
            }
        }
    }

    private void RayHitTarget(float distance)
    {
        if (m_raycastReceiver.receiverParent != null)
        {
            m_raycastReceiver.receiverParent.OnRayHitParent(distance);
        }
        else
        {
            m_raycastReceiver.OnRayHit(distance);
        }
    }

    private void RayExitTarget(GameObject oldReceiver, GameObject newReceiver)
    {
        if (m_raycastReceiver != null)
        {
            if (m_raycastReceiver.receiverParent != null)
            {
                if (newReceiver != null && oldReceiver.transform.parent.Equals(newReceiver.transform.parent))
                {
                    return;
                }
                m_raycastReceiver.receiverParent.OnRayExitParent();
            }
            else
            {
                m_raycastReceiver.OnRayExit();
            }
        }
    }
}