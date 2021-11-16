using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// A Script used to follow the cursor, and keep track of the Hovered chip stack count text
public class FollowCursor : MonoBehaviour
{
    // Check if the stack is being hovered to show to display the text box
    private bool m_IsHoveringStack = false;

    // The text that holds the Chip stack count
    [SerializeField]
    private TextMeshProUGUI m_ChipStackCountText;

    // Sets the text value of the Text box
    public void setChipStackCountText(string chipStackCountText)
    {
        m_ChipStackCountText.text = chipStackCountText;
    }

    // Returns IsHoveringStack
    public bool getIsHoveringStack()
    {
        return m_IsHoveringStack;
    }

    // Sets IsHoveringStack and enables the text gameobject
    public void setIsHoveringStack(bool value)
    {
        m_IsHoveringStack = value;
        gameObject.SetActive(m_IsHoveringStack);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsHoveringStack)
            transform.position = Input.mousePosition;
    }
}
