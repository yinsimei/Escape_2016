using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

// This is the class for the puzzle in 3D
public class Puzzle3D : RaycastReceiver
{
    public Puzzle puzzle;

    protected override void Start()
    {
        base.Start();
        Assert.IsNotNull(puzzle);
    }

    override public bool isInteractable()
    {
        return (interactive && !puzzle.GetComponent<Puzzle>().m_bPuzzleSolved);
    }

    override public void ClickAction()
    {
        base.ClickAction();
        if (!puzzle.m_bPuzzleSolved)
        {
            // Show puzzle2D
            puzzle.StartPuzzle();
        }
    }
}