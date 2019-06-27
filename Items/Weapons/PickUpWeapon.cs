using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour {
    private InventoryManager m_inventoryManager;
    private ExplinationText m_exTxt;
    // Use this for initialization
    /**/
    /*
     * Start()
     * NAME
     *  Start -
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     * 
     * RETURNS
     *  None
     */
    /**/
    void Start ()
    {
        m_inventoryManager = FindObjectOfType<InventoryManager>();
        
        m_exTxt = FindObjectOfType<ExplinationText>(); 
	}
    /*void Start();*/

    /**/
    /*
     * OnTriggerEnter2D()
     * NAME
     *  OnTriggerEnter2D -
     * SYNOPSIS 
     *  void OnTriggerEnter2D(Collider2D a_other)
     *      a_other -->
     * DESCRIPTION
     * 
     * RETURNS 
     * 
     */
    /**/
    private void OnTriggerEnter2D(Collider2D a_other)
    {
        if(a_other.tag == "Player")
        {
            Inventory playerInventory = m_inventoryManager.GetPlayerInventory();
            Weapons registeredWeapon = m_inventoryManager.GetRegisteredWeapon();
            registeredWeapon.SetDurability(1);
            playerInventory.AddItems(registeredWeapon, 1);
            this.gameObject.SetActive(false);
            m_exTxt.SetMessage("You picked up the " + registeredWeapon.GetName());
        }
    }
    /*private void OnTriggerEnter2D(Collider2D a_other);*/
}
