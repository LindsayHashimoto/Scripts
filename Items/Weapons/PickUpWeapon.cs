using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour {
    private InventoryManager m_inventoryManager;
    private ExplinationText m_exTxt;
    private SceneManagerScript m_sm;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  This sets the inital values of the above member variables. 
     * RETURNS
     *  None
     */
    /**/
    void Start ()
    {
        m_sm = SceneManagerScript.m_sm;

        m_inventoryManager = m_sm.transform.Find("Canvas").gameObject.transform.Find("Inventory Menu").gameObject.GetComponent<InventoryManager>();
        m_exTxt = m_sm.transform.Find("Canvas").gameObject.transform.Find("Explination Text").gameObject.GetComponent<ExplinationText>();

    }
    /*void Start();*/

    /**/
    /*
     * OnTriggerEnter2D()
     * NAME
     *  OnTriggerEnter2D -
     * SYNOPSIS 
     *  void OnTriggerEnter2D(Collider2D a_other)
     *      a_other --> the collison that enters this object.
     * DESCRIPTION
     *  When the player enters this collider, they will pick up the registered weapon and add it to their inventory. This
     *  item will also be set to be inactive after the user picks this item up. 
     * RETURNS 
     *  None
     */
    /**/
    private void OnTriggerEnter2D(Collider2D a_other)
    {
        if(a_other.tag == "Player")
        {
            Inventory playerInventory = m_inventoryManager.GetPlayerInventory();
            Weapons registeredWeapon = m_inventoryManager.GetRegisteredWeapon();
            registeredWeapon.SetDurability(registeredWeapon.GetDurability() + 1);
            if (registeredWeapon.GetDurability() == 1)
            {
                playerInventory.AddItems(registeredWeapon, 1);
            }
            playerInventory.UpdateInventory();            
            this.gameObject.SetActive(false);
            m_exTxt.SetMessage("You picked up the " + registeredWeapon.GetName());
        }
    }
    /*private void OnTriggerEnter2D(Collider2D a_other);*/
}
