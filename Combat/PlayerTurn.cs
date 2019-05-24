using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerTurn : MonoBehaviour {
    public Inventory m_playerInventory;

    private CombatManager m_combatManager;

    private InventoryManager m_inventoryManager; 

    // Use this for initialization
    void Start ()
    {
        m_combatManager = FindObjectOfType<CombatManager>();
        m_inventoryManager = FindObjectOfType<InventoryManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
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
