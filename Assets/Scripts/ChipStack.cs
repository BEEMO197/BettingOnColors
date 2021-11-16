using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A Container that holds values for Stacks of Chips
public class ChipStack : MonoBehaviour
{
    // The Chips that are in the Stack
    private List<Chip> m_ChipsInStack = new List<Chip>();

    // The amount of Chips in the Stack at the moment
    private int m_ChipCount;

    // The Type of chip that is in the Stack, useful when the stack has run out of chip objects, and you can't tell what type the stack was before
    private ChipType m_ChipStackType;

    // The Gameobject Prefabs that get spawned in to show how many chips you have (This caps out at 20 Chip prefabs, but the amount of actual chips can go up near infinite)
    [SerializeField]
    private GameObject[] m_ChipGameObjects = new GameObject[10];

    // Used to add chips to the stack, then make sure the prefab that spawns is the right color, and increases chip count properly
    public void addChipsToStack(int amountToAdd)
    {
        for(int i = 0; i < amountToAdd; i++)
        {
            m_ChipsInStack.Add(new Chip());
            m_ChipsInStack[m_ChipCount].setChipType(m_ChipStackType);
            m_ChipGameObjects[m_ChipCount > m_ChipGameObjects.Length - 1? m_ChipGameObjects.Length - 1: m_ChipCount].GetComponent<ChipClick>().setChipStackRef(this);
            m_ChipGameObjects[m_ChipCount > m_ChipGameObjects.Length - 1? m_ChipGameObjects.Length - 1: m_ChipCount].GetComponent<Renderer>().material.SetColor("_Color", m_ChipsInStack[i].getChipColor());
            m_ChipCount++;
        }

        updateChipGameobjects();
    }

    // Used to the remove chips from the stack, then make sure the count is properly decreased as well as preventing overages, so you can't remove more then you currently have
    public void removeChipsFromStack(int amountToRemove)
    {
        if (amountToRemove > m_ChipCount)
            amountToRemove = m_ChipCount;

        for (int i = 0; i < amountToRemove; i++)
        {
            m_ChipsInStack.RemoveAt(m_ChipCount - 1);
            m_ChipCount--;
        }

        updateChipGameobjects();
    }

    // Refills the Stack with 10 Chips, and makes sure that the chips get activated and the chips values are set
    public void refillStack()
    {
        for (int i = 0; i < 10; i++)
        {
            m_ChipsInStack.Add(new Chip());
            m_ChipsInStack[i].setChipType(m_ChipStackType);
            m_ChipGameObjects[i].SetActive(true);
            m_ChipGameObjects[i].GetComponent<ChipClick>().setChipStackRef(this);
            m_ChipGameObjects[i].GetComponent<Renderer>().material.SetColor("_Color", m_ChipsInStack[i].getChipColor());
            m_ChipCount++;
        }
    }

    // Sets the Chip type of the Stack
    public void setChipsInStack(ChipType chipType)
    {
        m_ChipStackType = chipType;
    }

    // Used to update the prefabs that get spawned to make sure that an accurate number is showing, if you only have 10 chips, 
    // only 10 of these will be active, if you have 20, 20 of them, 20 is the Max number of Prefabs
    public void updateChipGameobjects()
    {
        if (m_ChipCount <= m_ChipGameObjects.Length)
        {
            for (int i = 0; i < m_ChipGameObjects.Length; i++)
            {
                if (i >= m_ChipCount)
                    m_ChipGameObjects[i].SetActive(false);

                else
                    m_ChipGameObjects[i].SetActive(true);
            }
        }
        else
        {
            foreach(GameObject chipGameObject in m_ChipGameObjects)
            {
                chipGameObject.SetActive(true);
            }
        }
    }

    // Returns the amount of Chips in the Stack
    public int getChipCount()
    {
        return m_ChipCount;
    }

    // Returns the Chip Type of the Stack
    public ChipType getStackChipType()
    {
        return m_ChipStackType;
    }
}
