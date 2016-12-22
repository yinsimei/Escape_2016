using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class Puzzle_Digicode : Puzzle
{
    public int correctCode;
    public Text[] codeInputs;
    public Digicode_signal[] codeSignals;
    public GameObject[] digiKeys;

    private const int SIZE = 6;
    private const int RANGE = 10;
    private int[] m_pnCorrectCode = new int[SIZE];
    private int[] m_pCodeNumOccurrence = new int[RANGE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    private int[] m_pnCodeInputs = new int[SIZE];
    private int m_nCodeInputsNb = 0;

    // Result Evaluation
    private Digicode_signal.EDigiCodeSignal[] m_pResultEvaluation = new Digicode_signal.EDigiCodeSignal[SIZE];
    private bool m_bResultValid;

    // Use this for initialization
    override protected void Start()
    {
        base.Start();

        // Verify all external objects setted
        Assert.AreEqual(codeInputs.Length, SIZE);
        Assert.AreEqual(codeSignals.Length, SIZE);

        // Initialize code array
        int pos = 1;
        for (int i = 0; i < SIZE; ++i)
        {
            int code = (correctCode / pos) % 10;
            m_pnCorrectCode[SIZE - 1 - i] = code;
            pos *= 10;


            Assert.IsTrue(code < RANGE && code >= 0);
            ++m_pCodeNumOccurrence[code];
        }

        // Commented here because we don't need this fonction now
        // Erase frequently used keys in pad
        //for (int i = 0; i < RANGE; ++i)
        //{
        //    digiKeys[i].transform.GetChild(0).GetComponent<Text>().color = 
        //        (m_pCodeNumOccurrence[i] > 0) ? new Color(0f, 0f, 0f, 0.35f) : Color.black;
        //}
    }

    public override void StartPuzzle()
    {
        // base function
        base.StartPuzzle();
        Reset();
    }

    public void Reset()
    {
        // reset input
        m_nCodeInputsNb = 0;
        for (int i = 0; i < SIZE; ++i)
        {
            m_pnCodeInputs[i] = -1;
            // reset input field
            codeInputs[i].text = "";

            // reset signal color
            codeSignals[i].SetSignal(Digicode_signal.EDigiCodeSignal.Wrong);

            // reset result evaluation
            m_pResultEvaluation[i] = Digicode_signal.EDigiCodeSignal.Wrong;
            m_bResultValid = false;
        }
        
    }

    // Input functoin for button click
    public void Input(int p_num)
    {
        if (m_nCodeInputsNb == SIZE)
            return;

        // Update code input
        m_pnCodeInputs[m_nCodeInputsNb] = p_num;
        ++m_nCodeInputsNb;

        // Update Display
        for (int i = 0; i < m_nCodeInputsNb; ++i)
        {
            codeInputs[SIZE - m_nCodeInputsNb + i].text = "" + m_pnCodeInputs[i];
        }

        // If 6 code entered
        if (m_nCodeInputsNb == SIZE)
        {
            UpdateSignals();
        }
    }

    // Update signals
    private void UpdateSignals()
    {
        // Update Values
        int[] pOccurrence = new int[RANGE];
        System.Array.Copy(m_pCodeNumOccurrence, pOccurrence, RANGE);

        m_bResultValid = true;
        for (int i = 0; i < SIZE; ++i)
        {
            if (m_pnCodeInputs[i] == m_pnCorrectCode[i])
            {
                m_pResultEvaluation[i] = Digicode_signal.EDigiCodeSignal.Exact;
                --pOccurrence[m_pnCodeInputs[i]];
            }
            else
                m_bResultValid = false;
        }

        if (!m_bResultValid)
        {
            for (int i = 0; i < SIZE; ++i)
            {
                if (m_pnCodeInputs[i] == m_pnCorrectCode[i])
                    continue;

                if (pOccurrence[m_pnCodeInputs[i]] > 0)
                {
                    m_pResultEvaluation[i] = Digicode_signal.EDigiCodeSignal.ValueRight;
                    --pOccurrence[m_pnCodeInputs[i]];
                }
                else
                {
                    m_pResultEvaluation[i] = Digicode_signal.EDigiCodeSignal.Wrong;
                }
            }
        }

        // Update Display
        for (int i = 0; i < SIZE; ++i)
        {
            codeSignals[i].SetSignal(m_pResultEvaluation[i]);
        }

        if (m_bResultValid)
        {
            EndPuzzle(true);
        }
        else
        {
            Animator animator = GetComponent<Animator>();
            Assert.IsNotNull(animator);
            animator.SetTrigger("Error");
        }
    }
}