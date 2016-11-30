using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PieceMode
{
    EqualPieces,
    IrregularPieces
}

public class Puzzle_Jigsaw : Puzzle {
    public PieceMode m_PieceMode;

    // puzzle board dimension
    public int m_NbLines;
    public int m_NbColumns;

    // cells size
    public float m_PuzzleWidth;
    public float m_PuzzleHeight;

    // cells begin position
    public int m_UpLeftX_Pixel;
    public int m_UpLeftY_Pixel;

    public GameObject[] m_pPuzzlePieces;

    override protected void Start()
    {
        base.Start();
        int NbCells = m_NbLines * m_NbColumns;

        // create cells for game board
        foreach (GameObject puzzle in m_pPuzzlePieces)
        {
            puzzle.GetComponent<PuzzlePiece_Jigsaw>().m_GameBoard = gameObject;
            if (m_PieceMode == PieceMode.EqualPieces)
                puzzle.GetComponent<RectTransform>().sizeDelta = new Vector2(m_PuzzleWidth, m_PuzzleHeight);
        }
        
        // Create GridLayoutGroup to range cells
        GridLayoutGroup layout = gameObject.AddComponent<GridLayoutGroup>();
        layout.padding.left = m_UpLeftX_Pixel;
        layout.padding.top = m_UpLeftY_Pixel;
        layout.cellSize = new Vector2(m_PuzzleWidth, m_PuzzleHeight);
        layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        layout.constraintCount = m_NbColumns;

        // Create dynamic puzzle cells
        for (int i = 0; i < NbCells; ++i)
        {
            GameObject panel = new GameObject("Cell");
            panel.AddComponent<CanvasRenderer>();
            Image im = panel.AddComponent<Image>();
            PuzzleCell cell = panel.AddComponent<PuzzleCell>();
            cell.m_Pos = i + 1;
            im.color = Color.clear;

            panel.transform.SetParent(gameObject.transform, false);

            // Set puzzles to puzzle cells's children
            if (i < m_pPuzzlePieces.Length)
            {
                m_pPuzzlePieces[i].GetComponent<PuzzlePiece_Jigsaw>().SetParentCell(panel);
            }
        }
}

    private void KeepPuzzleInGameBoard(ref GameObject p_puzzle)
    {

        float OffsetX = 0f;
        float OffsetY = 0f;

        // Get Moved puzzle position
        Vector3[] puzzleCorners = new Vector3[4];
        p_puzzle.GetComponent<RectTransform>().GetWorldCorners(puzzleCorners);

        // Get Game Board Limits
        Vector3[] corners = new Vector3[4];
        transform.GetChild(0).GetComponent<RectTransform>().GetWorldCorners(corners);
        float UpLeftX = corners[1].x;
        float UpLeftY = corners[1].y;
        transform.GetChild(m_NbColumns * m_NbLines - 1).GetComponent<RectTransform>().GetWorldCorners(corners);
        float DownRightX = corners[3].x;
        float DownRightY = corners[3].y;

        // check if go beyond border
        // Up
        if (puzzleCorners[1].y > UpLeftY)
        {
            OffsetY = UpLeftY - puzzleCorners[1].y;
        }
        // Down
        if (puzzleCorners[3].y < DownRightY)
        {
            OffsetY = DownRightY - puzzleCorners[3].y;
        }

        // Left
        if (puzzleCorners[1].x < UpLeftX)
        {
            OffsetX = UpLeftX - puzzleCorners[1].x;
        }

        // Right
        if (puzzleCorners[3].x > DownRightX)
        {
            OffsetX = DownRightX - puzzleCorners[3].x;
        }

        p_puzzle.transform.position += new Vector3(OffsetX, OffsetY, 0f);
    }

    private void KeepAwayFromOtherPuzzles(ref GameObject p_puzzle)
    {
        Vector3[] puzzleCorners = new Vector3[4];
        p_puzzle.GetComponent<RectTransform>().GetWorldCorners(puzzleCorners);

        // Movable region size
        Vector3[] puzzleCellCorners = new Vector3[4];
        p_puzzle.transform.parent.GetComponent<RectTransform>().GetWorldCorners(puzzleCellCorners);

        float UpLeftX = puzzleCellCorners[1].x;
        float UpLeftY = puzzleCellCorners[1].y;
        float DownRightX = puzzleCellCorners[3].x;
        float DownRightY = puzzleCellCorners[3].y;

        // check if movable
        p_puzzle.GetComponent<PuzzlePiece_Jigsaw>().movable = false;

        int nPos = p_puzzle.GetComponent<PuzzlePiece_Jigsaw>().GetPositionInBoard() - 1;
        int[] nPosJoint = { nPos + 1, nPos - 1, nPos - m_NbColumns, nPos + m_NbColumns };

        foreach (int pos in nPosJoint) // go over other puzzles
        {
            if (pos >= 0 && pos < m_NbColumns * m_NbLines && transform.GetChild(pos).childCount == 0)
            {
                Vector3[] corners = new Vector3[4];
                transform.GetChild(pos).GetComponent<RectTransform>().GetWorldCorners(corners);

                UpLeftX = (UpLeftX <= corners[1].x) ? UpLeftX : corners[1].x;
                UpLeftY = (UpLeftY >= corners[1].y) ? UpLeftY : corners[1].y;
                DownRightX = (DownRightX >= corners[3].x) ? DownRightX : corners[3].x;
                DownRightY = (DownRightY <= corners[3].y) ? DownRightY : corners[3].y;
                p_puzzle.GetComponent<PuzzlePiece_Jigsaw>().movable = true;
            }
        }

        if (!p_puzzle.GetComponent<PuzzlePiece_Jigsaw>().movable)
        {
            p_puzzle.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            return;
        }

        // To implement
        float OffsetX = 0f;
        float OffsetY = 0f;

        // Up
        if (puzzleCorners[1].y > UpLeftY)
        {
            OffsetY = UpLeftY - puzzleCorners[1].y;
        }
        // Down
        if (puzzleCorners[3].y < DownRightY)
        {
            OffsetY = DownRightY - puzzleCorners[3].y;
        }

        // Left
        if (puzzleCorners[1].x < UpLeftX)
        {
            OffsetX = UpLeftX - puzzleCorners[1].x;
        }

        // Right
        if (puzzleCorners[3].x > DownRightX)
        {
            OffsetX = DownRightX - puzzleCorners[3].x;
        }

        p_puzzle.transform.position += new Vector3(OffsetX, OffsetY, 0f);

        // Set cell to puzzle
        int nNextPos = nPos;
        foreach (int pos in nPosJoint) // go over other puzzles
        {
            if (pos >= 0 && pos < m_NbColumns * m_NbLines && transform.GetChild(pos).childCount == 0)
            {
                if (Vector3.Distance(transform.GetChild(nNextPos).transform.position, p_puzzle.transform.position)
                    >= Vector3.Distance(transform.GetChild(pos).transform.position, p_puzzle.transform.position))
                {
                    nNextPos = pos;
                }
            }
        }

        PuzzlePiece_Jigsaw.ms_PuzzleBeingDraged.GetComponent<PuzzlePiece_Jigsaw>().SetParentCell(transform.GetChild(nNextPos).gameObject, false);
    }

    // Modify the position following mouse to a valid one
    public void MakeNewPositoinValid(ref GameObject p_puzzle)
    {
        KeepPuzzleInGameBoard(ref p_puzzle);
        KeepAwayFromOtherPuzzles(ref p_puzzle);
    }

    // Check whether every pieces are in the right place
    override protected bool CheckPuzzleComplete()
    {
        foreach (GameObject puzzle in m_pPuzzlePieces)
        {
            if (!puzzle.GetComponent<PuzzlePiece_Jigsaw>().CheckInRightPosition())
                return false;
        }
        return true;
    }

    // Check After Drop
    public void CheckAfterChange()
    {
        if (CheckPuzzleComplete())
        {
            EndPuzzle(true);
        }
    }
}
