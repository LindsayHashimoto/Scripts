/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public Items[] inventory;
    public int maxSize;
    public GameObject invBox;
    public MenuMovement mm; 
	// Use this for initialization
	void Start () {
        maxSize = 10;
        mm.numNodes = 0; 
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.A))
        {
            if (mm.isActive)
            {
                invBox.SetActive(false);
                mm.isActive = false;
            }
        }
    }

    void addToInventory(Items newObject)
    {
        string newName = newObject.name; 
        for(int i = 0; i < maxSize; i++)
        {
            if (newName.CompareTo(inventory[i]) > 0)
            {
                for (int j = maxSize; j > i; j--)
                {
                    inventory[j] = inventory[j - 1];
                    setUI(j); 
                }
                inventory[i] = newObject;
                inventory[i].m_durability = 1;
                setUI(i);
                mm.numNodes++; 
                return; 
            }
            else if (newName.CompareTo(inventory[i]) == 0)
            {
                inventory[i].m_durability++;
                setUI(i); 
            }
        }
    }
    void setUI(int index)
    {
        if (index == 0)
        {
            mm.nodes[index].text = ">" + inventory[index].name + " x" + inventory[index].numberOf;
        }
        else
        {
            mm.nodes[index].text = inventory[index].name + " x" + inventory[index].numberOf;
        }
    }
}
*/
