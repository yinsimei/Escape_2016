using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class FlashLight : MonoBehaviour
{
    public static FlashLight instance;
    
    public bool IsOn
    {
        get { return GetComponent<Light>().enabled; }
    }

    private bool m_bUsable;

    // Use this for initialization
    void Start ()
    {
        if (instance != null)
        {
            Debug.LogError("Flash light has already been instantiated!");
        }
        instance = this;
        m_bUsable = false;
    }
	
    public void SetUsable(bool p_bUsable)
    {
        m_bUsable = p_bUsable;
    }

    // Switch On/Off the flashlight
	public void SwitchOnOff(bool p_bOn)
    {
        if (!m_bUsable)
        {
            Assert.IsFalse(IsOn);
            return;
        }
        GetComponent<Light>().enabled = p_bOn;
        SoundManager.instance.Play("flashLight");
    }
}