using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	public void NewGame ()
    {
        SceneManager.LoadScene("Escape");
    }
	
	// Update is called once per frame
	public void Quit ()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
	}
}
