using UnityEngine;
using System.Collections;

public class DocumentsSystem : MonoBehaviour {

    private bool isShowing = false;
	// Use this for initialization
	void Start ()
    {
        isShowing = false;
    }
	
	// Show
    public void Show()
    {
        if (isShowing)
            return;

        isShowing = true;

        // Run show Animation
        GetComponent<Animator>().SetTrigger("Show");
    }

    // Hide
    public void Hide()
    {
        if (!isShowing)
            return;

        isShowing = false;

        // Run hide animation
        GetComponent<Animator>().SetTrigger("Hide");
    }
}
