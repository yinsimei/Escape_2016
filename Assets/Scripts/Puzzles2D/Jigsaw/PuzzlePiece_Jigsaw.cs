using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

// This script will allow a jigsaw puzzle piece to move horizontally vertically when being dragged
public class PuzzlePiece_Jigsaw : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int m_RightPosInBoard; // begin with 1

    [HideInInspector]
    public GameObject m_GameBoard;
    [HideInInspector]
    public static GameObject ms_PuzzleBeingDraged;
    private bool m_bMovable = true;

    public bool movable
    {
        get
        {
            return m_bMovable;
        }

        set
        {
            m_bMovable = value;
        }
    }

    //----------------------------------------------------
    // Start
    // ---------------------------------------------------
    void Start()
    {
        if (m_RightPosInBoard <= 0)
        {
            Debug.Log("Right Position For The Puzzle In Board Not Entered !");
            return;
        }

        // This has been commented because setted in the inspector
        // Set Anchor to Center
        //gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        //gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        //gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
    }

    //----------------------------------------------------
    // set parent cell
    // ---------------------------------------------------
    public void SetParentCell(GameObject p_cell, bool bResetPos = true)
    {
        gameObject.transform.SetParent(p_cell.transform);
        if (bResetPos)
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    public int GetPositionInBoard()
    {
        return transform.GetComponentInParent<PuzzleCell>().m_Pos;
    }

    //----------------------------------------------------
    // when drag
    // ---------------------------------------------------
    public bool CheckInRightPosition()
    {
        return (GetPositionInBoard() == m_RightPosInBoard);
    }

    //----------------------------------------------------
    // on drag Begins
    // ---------------------------------------------------
    public void OnBeginDrag(PointerEventData eventData)
    {
        // set statistic object to this
        ms_PuzzleBeingDraged = gameObject;

        // Set blocksRaycasts to false, else it won't work for drop event
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //----------------------------------------------------
    // on drag
    // ---------------------------------------------------
    public void OnDrag(PointerEventData eventData)
    {
        Canvas myCanvas = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<Canvas>();

        // if the render mode of canvas is ScreenSpaceOverlay
        if (myCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            // we can directly set the mouse position to it
            ms_PuzzleBeingDraged.transform.position = Input.mousePosition;
        }
        else
        {
            // we need to transform mouse position to fit it
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            ms_PuzzleBeingDraged.transform.position = myCanvas.transform.TransformPoint(pos);
        }

        m_GameBoard.GetComponent<Puzzle_Jigsaw>().MakeNewPositoinValid(ref ms_PuzzleBeingDraged);
    }

    //----------------------------------------------------
    // on drag end
    // ---------------------------------------------------
    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset values for next drag
        ms_PuzzleBeingDraged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Reset position
        gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        // Check if game end
        m_GameBoard.GetComponent<Puzzle_Jigsaw>().CheckAfterChange();
    }
}
