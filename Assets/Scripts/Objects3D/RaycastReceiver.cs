using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

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
    private Material m_material;
    private Color m_initialEmissionColor;

    // Use this for initialization
    virtual protected void Start ()
    {
        Assert.IsNotNull(GetComponent<Collider>());
        m_material = GetComponent<Renderer>().material;
        m_initialEmissionColor = m_material.GetColor("_EmissionColor");

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
                    m_material.SetColor("_EmissionColor", m_initialEmissionColor);
                }
                break;

            default:
                {
                    if (m_highlightState == EHighlightState.None)
                    {
                        Color emissionColor = Color.grey * Mathf.LinearToGammaSpace(lightness);
                        m_material.SetColor("_EmissionColor", emissionColor);
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

            m_material.SetColor("_EmissionColor", finalColor);
        }
    }
}
