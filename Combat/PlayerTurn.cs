using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerTurn : MonoBehaviour {
    public Inventory m_playerInventory;

    private bool m_attackClicked = false;
    private bool m_itemClicked = false;

    private UnityCombatComponents m_components; 

    // Use this for initialization
    void Start ()
    {
        m_components = FindObjectOfType<UnityCombatComponents>();
        SetListeners(); 
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Is called when the player clicks on a target
    public void GetTargetFromUser(Stats a_target)
    {
        if (m_attackClicked)
        {
            m_components.GetCombatManager().BasicAttack(a_target);
        }
        else if (m_itemClicked)
        {
            Items activeItem = m_playerInventory.GetActiveItem();
            if (activeItem.GetIsWeapon())
            {
                m_components.GetCombatManager().UseWeapon(a_target, (Weapons)activeItem);
            }
            else if (activeItem.GetIsPotion())
            {
                m_components.GetCombatManager().UsePotion(a_target, (Potions)activeItem);
            }
            m_playerInventory.UpdateUIInventory();
        }
        m_attackClicked = false;
        m_itemClicked = false;
    }

    private void SetListeners()
    {
        m_components.GetAttackBtn().onClick.AddListener(OnAttackClick);
        m_components.GetInventoryBtn().onClick.AddListener(OnInventoryClick);
        m_components.GetPassBtn().onClick.AddListener(OnPassClick);
        m_components.GetBackBtn().onClick.AddListener(OnBackClick);
        m_components.GetUseBtn().onClick.AddListener(OnUseClick);
        m_components.GetCancelBtn().onClick.AddListener(OnBackClick);
    }

    private void OnAttackClick()
    {
        m_attackClicked = true;
        m_components.GetTurnMenu().SetActive(false);
        m_components.GetBackBtn().gameObject.SetActive(true);
    }

    private void OnInventoryClick()
    {
        m_components.GetTurnMenu().SetActive(false);
        m_components.GetInventoryUI().SetActive(true);
    }

    private void OnPassClick()
    {
        m_components.GetCombatManager().NextTurn(); 
    }

    private void OnBackClick()
    {
        m_attackClicked = false;
        m_itemClicked = false;
        m_components.GetTurnMenu().SetActive(true);
        m_components.GetInventoryUI().SetActive(false);
        m_components.GetBackBtn().gameObject.SetActive(false);
    }

    private void OnUseClick()
    {
        m_components.GetInventoryUI().SetActive(false);
        m_itemClicked = true;
        m_components.GetTurnMenu().SetActive(false);
        m_components.GetBackBtn().gameObject.SetActive(true);
    }
}
