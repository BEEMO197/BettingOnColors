using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An Enum to hold the available Betting Colors
public enum BettingColor
{
    Red = 1,
    Green = 2
}

// A pointer class that selects which Color is currently hovered
public class PointerBehaviour : MonoBehaviour
{
    // The current Color it is hovering
    private BettingColor m_currentColor;

    // Returns the Current color hovered
    public BettingColor getCurrentBettingColor()
    {
        return m_currentColor;
    }

    // When the pointer hits the Betting Color Triggers, set it's color to the Current Color
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("BettingWheelRed"))
        {
            Debug.Log("Hit Red");
            m_currentColor = BettingColor.Red;
        }

        else if(collider.CompareTag("BettingWheelGreen"))
        {
            Debug.Log("Hit Green");
            m_currentColor = BettingColor.Green;
        }
    }
}
