using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading; 

public class PlayerTurn : MonoBehaviour {
    private Inventory m_playerInventory;

    private CombatManager m_combatManager;

    private InventoryManager m_inventoryManager;

    private GameObject m_turnMenu;

    private bool m_activateMenu;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  This sets the value of m_combatManager and assignes m_inventoryManager and m_turnMenu when m_combatManager assigns their
     *  values. 
     * RETURNS
     *  None
     */
    /**/
    void Start ()
    {
        m_combatManager = FindObjectOfType<CombatManager>();
        m_activateMenu = false;

        Thread t = new Thread(new ThreadStart(WaitForAssignment));
        t.Start(); 
        
    }
    /*void Start();*/

    /**/
    /*
     * Update()
     * NAME 
     *  Update - Update is called once per frame
     * SYNOPSIS
     *  void Update()
     * DESCRIPTION
     *  If the game is paused, the turn menu should be set to be not active. When the game is un-paused, the turn menu should be 
     *  re-activated. 
     * RETURNS
     *  None
     */
    /**/
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
    /*void Update();*/

    /**/
    /*
     * GetTargetFromUser()
     * NAME
     *  GetTargetFromUser - performs action when the user selects a target. 
     * SYNOPSIS
     *  void GetTargetFromUser(Stats a_target)
     *      a_target --> the target the user selected
     * DESCRIPTION
     *  If the user clicked on the attack button before selecting a target, they will do a basic attack. If the user clicked on the 
     *  item button, selected an item and then clicked on a target, the user will use that item on the selected target. 
     * RETURNS
     *  None
     */
    /**/
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
    /*public void GetTargetFromUser(Stats a_target);*/

    /**/
    /*
     * WaitForAssignment()
     * NAME
     *  WaitForAssignment - this waits for m_turnMenu and m_inventoryManager to be assigned in m_combatManager
     * SYNOPSIS
     *  void WaitForAssignment()
     * DESCRIPTION
     *  This occurs on a separate thread. It watis for the turn menu and inventory manager in the combat manager to not be null 
     *  and then assigns the value to m_turnMenu and m_inventoryManager. When m_inventoryManager is assigned, m_playerInventory
     *  can also be assigned. 
     * RETURNS
     *  None
     */
    /**/
    private void WaitForAssignment()
    {
        //These while loops makes sure the functions are not called before the items are given values. 
        while (m_turnMenu == null)
        {
            m_turnMenu = m_combatManager.GetTurnMenu();
        }
        while (m_inventoryManager == null)
        {
            m_inventoryManager = m_combatManager.GetInventoryManager();
        }
        m_playerInventory = m_inventoryManager.GetPlayerInventory(); 
    }
    /*private void WaitForAssignment();*/
}
