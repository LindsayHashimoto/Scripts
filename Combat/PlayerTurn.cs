using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerTurn : MonoBehaviour {
    public Inventory m_playerInventory;

    private CombatManager m_combatManager;

    private InventoryManager m_inventoryManager;

    private GameObject m_turnMenu;

    private bool m_activateMenu = false; 

    // Use this for initialization
    void Start ()
    {
        m_combatManager = FindObjectOfType<CombatManager>();
        m_turnMenu = GameObject.FindGameObjectWithTag("TurnMenu");
        m_inventoryManager = FindObjectOfType<InventoryManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Time.timeScale > 0)
        {
            if (m_activateMenu)
            {
                m_turnMenu.SetActive(true);
                m_activateMenu = false; 
            }
        }
        else
        {
            m_turnMenu.SetActive(false);
            m_activateMenu = true; 
        }
	}

    //Is called when the player clicks on a target
    public void GetTargetFromUser(Stats a_target)
    {
        if (m_combatManager.GetAttackClicked())
        {
            m_combatManager.BasicAttack(a_target);
        }
        else if (m_combatManager.GetItemClicked())
        {
            Items activeItem = m_inventoryManager.GetActiveItem();
            if (activeItem.GetIsWeapon())
            {
                m_combatManager.UseWeapon(a_target, (Weapons)activeItem);
            }
            else if (activeItem.GetIsPotion())
            {
                m_combatManager.UsePotion(a_target, (Potions)activeItem);
            }
            m_playerInventory.UpdateInventory();
        }
        m_combatManager.SetAttackClicked(false);
        m_combatManager.SetItemClicked(false); 
    }

    
}
