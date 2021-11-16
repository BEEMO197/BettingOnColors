using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A Class that is used to get Mouse clicks on the Chips/Stacks
public class ChipClick : MonoBehaviour
{
    // Each chip has a Reference to the Stack it currently belongs too
    private ChipStack m_ChipStackRef;

    // This holds a reference to the Follow Cursor Script so the FollowCursor can have access to the Stack's total count
    [SerializeField]
    private FollowCursor m_FollowCursorRef;

    private void Start()
    {
        m_FollowCursorRef = FindObjectOfType<FollowCursor>();
    }

    // When chips get added to the ChipStack this gets called to set the ChipStack Ref
    public void setChipStackRef(ChipStack chipStackRef)
    {
        m_ChipStackRef = chipStackRef;

    }

    // When the Mouse is clicked down on a Chip/Stack this gets called
    private void OnMouseDown()
    {
        // Check if the parent of the ChipStack Gameobject has the Tag "Player" or "Table" and either bet Chips or Return them to the player
        if (m_ChipStackRef.gameObject.transform.parent.CompareTag("Player"))
        {
            m_ChipStackRef.GetComponentInParent<PlayerBehaviour>().betChips(m_ChipStackRef.getStackChipType());
        }

        else if(m_ChipStackRef.gameObject.transform.parent.CompareTag("Table"))
        {
            m_ChipStackRef.GetComponentInParent<TableBehaviour>().returnChips(m_ChipStackRef.getStackChipType());
        }
    }

    // When the mouse hovers over the Stack, Enable the Floating chipcount Text
    private void OnMouseEnter()
    {
        m_FollowCursorRef.setIsHoveringStack(true);
        m_FollowCursorRef.setChipStackCountText(m_ChipStackRef.getChipCount().ToString());
    }

    // When the mouse stops hovering the Stack, disable the floating chipcount text
    private void OnMouseExit()
    { 
        m_FollowCursorRef.setIsHoveringStack(false);
    }
}
