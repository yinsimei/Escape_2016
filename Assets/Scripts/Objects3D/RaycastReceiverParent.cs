using UnityEngine;
using System.Collections;

public class RaycastReceiverParent : MonoBehaviour
{
    public void OnRayHitParent(float distance)
    {
        foreach (RaycastReceiver receiver in transform.GetComponentsInChildren<RaycastReceiver>())
        {
            receiver.OnRayHit(distance);
        }
    }

    public void OnRayExitParent()
    {
        foreach (RaycastReceiver receiver in transform.GetComponentsInChildren<RaycastReceiver>())
        {
            receiver.OnRayExit();
        }
    }

    public void ClickActionParent()
    {
        foreach (RaycastReceiver receiver in transform.GetComponentsInChildren<RaycastReceiver>())
        {
            receiver.ClickAction();
        }
    }
}
