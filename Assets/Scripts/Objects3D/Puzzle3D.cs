using UnityEngine;
using System.Collections;

// This is the class for the puzzle in 3D
public class Puzzle3D : RaycastReceiver
{
    public GameObject puzzle;

    override public bool isInteractable()
    {
        return (interactive && !puzzle.GetComponent<Puzzle>().m_bPuzzleSolved);
    }

    override public void ClickAction()
    {
        base.ClickAction();

        Puzzle p = puzzle.GetComponent<Puzzle>();
        if (!p.m_bPuzzleSolved)
        {
            // Show puzzle2D
            p.StartPuzzle();
        }
    }
}