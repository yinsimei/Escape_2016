using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class Digicode_signal : MonoBehaviour {
    public enum EDigiCodeSignal
    {
        Exact,
        ValueRight,
        Wrong
    }

    private EDigiCodeSignal m_eCurrentSignal;
    
    public void SetSignal(EDigiCodeSignal p_eNewSignal)
    {
        if (m_eCurrentSignal == p_eNewSignal)
            return;

        m_eCurrentSignal = p_eNewSignal;
        Animator signalAnim = GetComponent<Animator>();
        Assert.IsNotNull(signalAnim);

        signalAnim.SetInteger("SignalEnum", (int)m_eCurrentSignal);
    }
}
