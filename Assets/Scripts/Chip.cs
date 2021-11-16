using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An enum to define chip types, with values ranging from 0 - 9, to define it with numbers as well as a name
public enum ChipType
{
    White   = 0,
    Blue    = 1,
    Yellow  = 2,
    Green   = 3,
    Purple  = 4,
    Red     = 5,
    Cyan    = 6,
    Black   = 7,
    Silver  = 8,
    Gold    = 9,
}

// Basic Chip class used to hold value of a single Chip, useful for if you wanted to have different chips in a stack all with different values
public class Chip
{
    // The type of chip it is
    private ChipType m_ChipType;

    // The Color of the Chip, used for models
    private Color m_ChipColor;

    // The Value the Chip holds (Not used for anything just thought It'd be fun to add this)
    private int m_ChipValue;

    // Sets the Chip Type and changes it's Colors and Values accordingly
    public void setChipType(ChipType chipType)
    {
        m_ChipType = chipType;

        switch(m_ChipType)
        {
            case ChipType.White:
                m_ChipColor = Color.white;
                m_ChipValue = 1;
                break;

            case ChipType.Blue:
                m_ChipColor = Color.blue;
                m_ChipValue = 5;
                break;

            case ChipType.Yellow:
                m_ChipColor = Color.yellow;
                m_ChipValue = 10;
                break;

            case ChipType.Green:
                m_ChipColor = Color.green;
                m_ChipValue = 20;
                break;

            case ChipType.Purple:
                m_ChipColor = Color.magenta;
                m_ChipValue = 40;
                break;

            case ChipType.Red:
                m_ChipColor = Color.red;
                m_ChipValue = 50;
                break;

            case ChipType.Cyan:
                m_ChipColor = Color.cyan;
                m_ChipValue = 75;
                break;

            case ChipType.Black:
                m_ChipColor = Color.black;
                m_ChipValue = 100;
                break;

            case ChipType.Silver:
                m_ChipColor = Color.gray;
                m_ChipValue = 150;
                break;

            case ChipType.Gold:
                m_ChipColor = Color.gray + Color.yellow;
                m_ChipValue = 300;
                break;
        }

    }

    // Returns the Chips Color
    public Color getChipColor()
    {
        return m_ChipColor;
    }

    // Returns the Chips Value
    public int getChipValue()
    {
        return m_ChipValue;
    }

}
