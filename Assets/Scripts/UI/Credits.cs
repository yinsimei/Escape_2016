using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Credits : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject creditText;
    public float rollingSpeed = 10f;
    public float rollingSpeedFast = 30f;

    private float m_fCurrentSpeed;
    private bool m_bIsPlaying = false;
    private bool m_bBottomEntered = false;
    private Vector3 m_textOriginalPosition;
    private Rect screenRect;

    // Use this for initialization
    void Start ()
    {
        Assert.IsNotNull(creditText, "credit text should be assigned !");
        m_textOriginalPosition = creditText.transform.position;
        m_fCurrentSpeed = rollingSpeed;
        
        GameObject UICam = GameObject.FindGameObjectWithTag("UICamera");
        Camera cam;
         if (UICam == null)
        {
            cam = Camera.main;
        }
        else
        {
            cam = UICam.GetComponent<Camera>();
        }
        Vector3 c1 = cam.ScreenToWorldPoint(new Vector3(0f, 0f, cam.nearClipPlane));
        Vector3 c2 = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));
        screenRect = new Rect(c1.x, c1.y, c2.x - c1.x, c2.y - c1.y);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_bIsPlaying)
        {
            // Translate
            RectTransform textRect = creditText.GetComponent<RectTransform>();
            creditText.transform.Translate(0f, m_fCurrentSpeed * Time.deltaTime, 0f);

            // Check if overlap
            Vector3[] corners = new Vector3[4];
            textRect.GetWorldCorners(corners);
            Rect rec = new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);
            if (rec.Overlaps(screenRect))
            {
                if (!m_bBottomEntered)
                {
                    m_bBottomEntered = true;
                }
            }
            else
            {
                if (m_bBottomEntered)
                {
                    End();
                }
            }
        }
    }

    public void Play()
    {
        GetComponent<Animator>().SetTrigger("Show");
        m_bIsPlaying = true;
    }

    private void End()
    {
        // Reset
        m_bIsPlaying = false;
        m_bBottomEntered = false;
        creditText.transform.position = m_textOriginalPosition;

        // Reload Main Menu
        SceneManager.LoadScene("MainMenu");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_fCurrentSpeed = rollingSpeedFast;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_fCurrentSpeed = rollingSpeed;
    }
}
