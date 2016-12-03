using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class RaycastReceiver : MonoBehaviour {
    [Serializable]
    public enum EHighlightState
    {
        None,
        Highlight,
        Blink
    }

    public float reactionDistance = 3f;
    public bool interactive = true;
    public float lightness = 0.1f; // between 0 and 1

    [HideInInspector]
    public bool IsClickable
    {
        get { return (m_highlightState == EHighlightState.Blink); }
    }

    [HideInInspector]
    public RaycastReceiverParent receiverParent = null;

    private EHighlightState m_highlightState = EHighlightState.None;

    private struct SMaterialInfo
    {
        public Material m_material;
        public Color m_initialEmissionColor;

        public SMaterialInfo(Material p_material)
        {
            m_material = p_material;
            m_initialEmissionColor = m_material.GetColor("_EmissionColor");
        }
    }
    private List<SMaterialInfo> m_materialInfos = new List<SMaterialInfo>();
    // Use this for initialization
    virtual protected void Start ()
    {
        Assert.IsNotNull(GetComponent<Collider>(), "The raycast receiver should have a collider!");
        foreach (Material m in GetComponent<Renderer>().materials)
        {
            m_materialInfos.Add(new SMaterialInfo(m));
        }

        m_highlightState = EHighlightState.None;

        receiverParent = transform.GetComponentInParent<RaycastReceiverParent>();
    }


    virtual public bool isInteractable()
    {
        return interactive;
    }

    private void SetHighlightState(EHighlightState newState)
    {
        switch(newState)
        {
            case EHighlightState.None:
                {
                    foreach (SMaterialInfo sm in m_materialInfos)
                    {
                        sm.m_material.SetColor("_EmissionColor", sm.m_initialEmissionColor);
                    }  
                }
                break;

            default:
                {
                    if (m_highlightState == EHighlightState.None)
                    {
                        Color emissionColor = Color.grey * Mathf.LinearToGammaSpace(lightness);
                        foreach (SMaterialInfo sm in m_materialInfos)
                        {
                            sm.m_material.SetColor("_EmissionColor", emissionColor);
                        }
                    }
                }
                break;
        }
        m_highlightState = newState;
    }

    public void OnRayHit(float distance)
    {
        if (!isInteractable())
            return;

        if (reactionDistance >= distance)
        {
            SetHighlightState(EHighlightState.Blink);
        }
        else
        {
            SetHighlightState(EHighlightState.Highlight);
        }
    }

    public void OnRayExit()
    {
        SetHighlightState(EHighlightState.None);
    }

    // Update is called once per frame
    protected void Update ()
    {
        HighlightAnimation();
    }

    virtual public void ClickAction()
    {
        SetHighlightState(EHighlightState.None);
    }

    protected void HighlightAnimation()
    {
        if (m_highlightState == EHighlightState.None)
            return;

        if (m_highlightState == EHighlightState.Blink)
        {
            float blinkSpeed = 2f;
            float emission = Mathf.PingPong(Time.time, 1.0f/blinkSpeed);
            Color baseColor = Color.grey;
            Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission * blinkSpeed * lightness * 0.8f + lightness * 0.2f);

            foreach (SMaterialInfo sm in m_materialInfos)
            {
                sm.m_material.SetColor("_EmissionColor", finalColor);
            }
        }
    }
}
