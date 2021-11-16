using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{
    // Max number of chips to give to a player when they start, or when they run out of chips
    public static int MaxChipCount = 100;

    // Max number of stacks of chips that are allowed. This number can be a max of 10 (Based on how many Chip colors available in the chip script)
    public static int MaxChipStacks = 10;

    // The number of chips to bet at once
    public static int BetAmount = 10;

    // The Positions of the players ChipStacks
    public Vector3[] m_PlayerChipPositions;

    // The Players Stacks of Chips
    private ChipStack[] m_ChipStacks = new ChipStack[MaxChipStacks];

    // The Prefab for spawning Chip Stacks
    [SerializeField]
    private GameObject m_ChipStackPrefab;

    // A Reference to the Table Behaviour
    [SerializeField]
    private TableBehaviour m_Table;

    // The Chosen Color you want to bet on Landing
    private BettingColor m_ChosenColor = BettingColor.Green;

    // The Image of the Chosen Color, Used for visually showing which Color you currently have chosen
    [SerializeField]
    private Image m_ChosenColorImage;

    // Text on the screen that shows how much the current bet amount is
    [SerializeField]
    private TextMeshProUGUI m_BetAmountText;

    // Returns the Players Chosen Color
    public BettingColor getChosenColor()
    {
        return m_ChosenColor;
    }

    // Used for the Button on screen that picks the Red Color
    public void setChosenColorRed()
    {
        if (!m_Table.getBettingStarted())
        {
            m_ChosenColor = BettingColor.Red;
            m_ChosenColorImage.color = Color.red;
        }
    }

    // Used for the Button on screen that picks the Green Color
    public void setChosenColorGreen()
    {

        if (!m_Table.getBettingStarted())
        {
            m_ChosenColor = BettingColor.Green;
            m_ChosenColorImage.color = Color.green;
        }
    }

    // Used for the Button on screen that Increases the Bet Amount
    public void increaseBetAmount()
    {
        BetAmount++;
        if (BetAmount > 20)
            BetAmount = 20;
        m_BetAmountText.text = BetAmount.ToString();
    }

    // Used for the Button on screen that Decreases the Bet Amount
    public void decreaseBetAmount()
    {
        BetAmount--;
        if (BetAmount < 1)
            BetAmount = 1;
        m_BetAmountText.text = BetAmount.ToString();
    }

    // Bets chips, by removing them from the Stack you clicked on and adding them to the Stack that is the same Chiptype on the Table
    public void betChips(ChipType chipTypeToBet)
    {
        if (!m_Table.getBettingStarted())
        {
            m_Table.betChips(chipTypeToBet, (BetAmount > m_ChipStacks[(int)chipTypeToBet].getChipCount() ? m_ChipStacks[(int)chipTypeToBet].getChipCount() : BetAmount));
            m_ChipStacks[(int)chipTypeToBet].removeChipsFromStack(BetAmount);
        }
    }

    // Checks if the players ChipStacks have anything in them, if none do, refill the players Chip Stacks
    public void checkHasChips()
    {
        int i = 0;
        foreach(ChipStack chipStack in m_ChipStacks)
        {
            if (chipStack.getChipCount() <= 0)
                i++;
        }

        if (i >= MaxChipStacks)
            refillPlayerChips();
    }

    // Used to add Chips to the Stack type
    public void addChips(ChipType chipTypeToAdd, int amountToAdd)
    {
        m_ChipStacks[(int)chipTypeToAdd].addChipsToStack(amountToAdd);
    }

    private void Start()
    {
        fillPlayerChips();
    }

    // Refills the Players Chips when they are empty
    public void refillPlayerChips()
    {
        foreach(ChipStack chipStack in m_ChipStacks)
        {
            chipStack.refillStack();
        }
    }

    // Initial Player Chip Fill, runs on start, and instantiate Chip Stacks, position, types and parents.
    public void fillPlayerChips()
    {
        for (int i = 0; i < MaxChipStacks; i++)
        {
            m_ChipStacks[i] = Instantiate(m_ChipStackPrefab).GetComponent<ChipStack>();
            m_ChipStacks[i].transform.position = transform.position + m_PlayerChipPositions[i];
            m_ChipStacks[i].setChipsInStack((ChipType)i);
            m_ChipStacks[i].transform.parent = gameObject.transform;
            m_ChipStacks[i].refillStack();
        }
    }

    // Draws Gizmos to show the Position of the Players chips.
    private void OnDrawGizmos()
    {
        foreach (Vector3 playerChipPosition in m_PlayerChipPositions)
        {
            Gizmos.DrawSphere(transform.position + playerChipPosition, 0.1f);
        }
    }
}
