using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class Puzzle_Digicode : Puzzle
{
    public int correctCode;
    public Text[] codeInputs;
    public Digicode_signal[] codeSignals;

    private const int SIZE = 6;
    private const int RANGE = 10;
    private int[] m_pnCorrectCode = new int[SIZE];
    private int[] m_pCodeNumOccurrence = new int[RANGE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    private int[] m_pnCodeInputs = new int[SIZE];
    private int m_nCodeInputsNb = 0;

    private struct SResultEvaluation
    {
        public int m_nExactCodeNb;
        public int m_nRightCodeNb;
        public int m_nValueRightCodeNb { get { return (m_nRightCodeNb - m_nExactCodeNb); } }
        public int m_nWrongCodeNb { get { return (SIZE - m_nRightCodeNb); } }
        public bool m_bValidResult { get { return (m_nExactCodeNb == SIZE); } }

        public void Reset()
        {
            m_nExactCodeNb = 0;
            m_nRightCodeNb = 0;
        }
    }
    private SResultEvaluation m_sResult;

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

        // Erase frequently used keys in pad
        for (int i = 0; i < RANGE; ++i)
        {

        }
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
        }
        m_sResult.Reset();
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

        for (int i = 0; i < SIZE; ++i)
        {
            if (m_pnCodeInputs[i] == m_pnCorrectCode[i])
                ++m_sResult.m_nExactCodeNb;

            if (pOccurrence[m_pnCodeInputs[i]] > 0)
            {
                --pOccurrence[m_pnCodeInputs[i]];
                ++m_sResult.m_nRightCodeNb;
            }
        }

        // Update Display
        int k = 0;
        for (int i = 0; i < m_sResult.m_nWrongCodeNb; ++i)
        {
            codeSignals[k].SetSignal(Digicode_signal.EDigiCodeSignal.Wrong);
            ++k;
        }

        for (int i = 0; i < m_sResult.m_nValueRightCodeNb; ++i)
        {
            codeSignals[k].SetSignal(Digicode_signal.EDigiCodeSignal.ValueRight);
            ++k;
        }

        for (int i = 0; i < m_sResult.m_nExactCodeNb; ++i)
        {
            codeSignals[k].SetSignal(Digicode_signal.EDigiCodeSignal.Exact);
            ++k;
        }

        Assert.AreEqual(k, SIZE);

        if (m_sResult.m_bValidResult)
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