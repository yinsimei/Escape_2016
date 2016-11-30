using UnityEngine;
using System.Collections;

public class Puzzle_Digicode : Puzzle {
    private const int SIZE = 6;
    public int code;
    private int[] codeNumbers = new int[SIZE];

    // Use this for initialization
    override protected void Start()
    {
        base.Start();

        // Initialize code array
        int pos = 1;
        for (int i = 0; i < SIZE; ++i)
        {
            codeNumbers[SIZE - 1 - i] = (code / pos) % 10;
            pos *= 10;
        }

        // Erase frequently used keys in pad
        for (int i = 0; i < SIZE; ++i)
        {

        }
	}

    // Update is called once per frame
    override protected void Update() {
	
	}
}
