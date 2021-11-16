using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Behaviour for the Table, this is where all the Betting and "roulette" portion are
public class TableBehaviour : MonoBehaviour
{
    // The Stacks of chips that have betted chips in
    private ChipStack[] m_BetChipStacks = new ChipStack[PlayerBehaviour.MaxChipStacks];

    // The Prefab of the Chip Stack
    [SerializeField]
    private GameObject m_ChipStackPrefab;

    // The positions of where the ChipStack prefabs will spawn, useful here just to see custom Gizmos, and have access to them within the class
    [SerializeField]
    private Vector3[] m_BettingChipPositions = new Vector3[PlayerBehaviour.MaxChipStacks];

    // The Spinner Gameobject, the object that spins around to determine the Color
    [SerializeField]
    private GameObject m_SpinnerObject;

    // A Reference to the Pointer, this keeps track of the currently hovered Color of the roulette
    [SerializeField]
    private PointerBehaviour m_Pointer;

    // how many Rotations along the Y Axis Per second
    [SerializeField]
    private float m_RotationsPerSecond;

    // A Reference to the Player
    [SerializeField]
    private PlayerBehaviour m_Player;

    // A Boolean that keeps track of when the Roulette is spinning
    private bool bettingStarted = false;

    // Sets the Betting chip stacks, by instantiated the Prefab, and setting their positions, types and Parents
    public void setBettingChipStacks()
    {
        for (int i = 0; i < PlayerBehaviour.MaxChipStacks; i++)
        {
            m_BetChipStacks[i] = Instantiate(m_ChipStackPrefab).GetComponent<ChipStack>();
            m_BetChipStacks[i].transform.position = transform.position + m_BettingChipPositions[i];
            m_BetChipStacks[i].setChipsInStack((ChipType)i);
            m_BetChipStacks[i].transform.parent = gameObject.transform;
        }
    }

    // This is called when the player clicks on their chips and wants to bet, this just adds what they bet to the Betting Stacks
    public void betChips(ChipType chipTypeToBet, int amountToBet)
    {
        m_BetChipStacks[(int)chipTypeToBet].addChipsToStack(amountToBet);
    }

    // When the player clicks on the chips he has betted, and hasn't already started the Roulette, he can take back the stack of chips he put in
    public void returnChips(ChipType chipTypeToReturn)
    {
        if (!bettingStarted)
        {
            m_Player.addChips(chipTypeToReturn, m_BetChipStacks[(int)chipTypeToReturn].getChipCount());
            m_BetChipStacks[(int)chipTypeToReturn].removeChipsFromStack(m_BetChipStacks[(int)chipTypeToReturn].getChipCount());
        }
    }

    // Rotates the Table by m_RotationsPerSecond
    private void rotateTable()
    {
        m_SpinnerObject.transform.Rotate(new Vector3(0, m_RotationsPerSecond * Time.deltaTime, 0));
    }

    // A Coroutine function that Starts the Roulette Spinner, and sets the bool betting Started to show that the game is currently going and you can't place or take bets back
    // Acts like an Update Function and the perks of this is to have a seperate thread running, so I can run it whenever
    public IEnumerator StartSpinner()
    {
        bettingStarted = true;
        m_RotationsPerSecond = 720;
        StartCoroutine(SlowSpinner());

        while (m_RotationsPerSecond >= 10)
        {
            // Every Time.deltaTime (every frame basically) call the Rotate table Function
            yield return new WaitForSeconds(Time.deltaTime);
            rotateTable();
        }
    }

    // A Coroutine function that Works to Slow the Spinners Speed, and ultimately decides the game
    public IEnumerator SlowSpinner()
    {
        // Waits 1.5 to 5 seconds before stopping to spin, Adds some suspense, and a "Sense" of "Loading"
        yield return new WaitForSeconds(Random.Range(1.5f, 5.0f));

        while (m_RotationsPerSecond >= 10)
        {
            // Every 0.1 to 0.175 Seconds decrease the Rotations persecond by 90% of what it was
            yield return new WaitForSeconds(Random.Range(0.1f, 0.175f));
            m_RotationsPerSecond *= 0.90f;
        }

        // Stops spinning
        bettingStarted = false;

        // Compare Colors
        if (m_Pointer.getCurrentBettingColor() == m_Player.getChosenColor())
        {
            // Player wins, give back double what they bet
            foreach (ChipStack betChipStack in m_BetChipStacks)
            {
                if (betChipStack.getChipCount() > 0)
                {
                    m_Player.addChips(betChipStack.getStackChipType(), betChipStack.getChipCount() * 2);
                    betChipStack.removeChipsFromStack(betChipStack.getChipCount());
                }
            }
        }
        else
        {
            // Player Lost, Gain back nothing, and check if they ran out of chips
            foreach (ChipStack betChipStack in m_BetChipStacks)
            {
                if (betChipStack.getChipCount() > 0)
                {
                    betChipStack.removeChipsFromStack(betChipStack.getChipCount());
                }
            }

            m_Player.checkHasChips();
        }

        m_RotationsPerSecond = 0;
    }

    // Returns if the Betting has Started or not
    public bool getBettingStarted()
    {
        return bettingStarted;
    }

    // Used for the Spin Button on the Game Screen
    public void StartBetButton()
    {
        StartCoroutine(StartSpinner());
    }

    private void Start()
    {
        setBettingChipStacks();
    }

    // Draws Gizmos on the Editor screen to show where the Betting Chips will land
    private void OnDrawGizmos()
    {
        foreach (Vector3 bettingChipPosition in m_BettingChipPositions)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position + bettingChipPosition, 0.05f);
        }
    }
}
