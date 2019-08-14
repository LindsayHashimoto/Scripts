using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour {
    private InventoryManager m_inventoryManager;
    private ExplinationText m_exTxt;
    private SceneManagerScript m_sm;
    private Weapons m_pickedUpWeapon; 

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization.
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
     *  OnTriggerEnter2D - performs actions when something enters this object. 
     * SYNOPSIS 
     *  void OnTriggerEnter2D(Collider2D a_other)
     *      a_other --> the collison that enters this object.
     * DESCRIPTION
     *  When the player enters this collider, they will pick up the registered weapon and add it to their inventory. 
     *  This item will also be set to be inactive after the user picks this item up. 
     * RETURNS 
     *  None
     */
    /**/
    private void OnTriggerEnter2D(Collider2D a_other)
    {
        if(a_other.tag == "Player")
        {
            Inventory playerInventory = m_inventoryManager.GetPlayerInventory(); 
            if (m_pickedUpWeapon.GetDurability() == 0)
            {
                m_pickedUpWeapon.SetDurability(1); 
                playerInventory.AddItems(m_pickedUpWeapon, 1);
            }
            else
            {
                m_pickedUpWeapon.SetDurability(m_pickedUpWeapon.GetDurability() + 1);
            }
            playerInventory.UpdateInventory();            
            this.gameObject.SetActive(false);
            m_exTxt.SetMessage("You picked up the " + m_pickedUpWeapon.GetName());
        }
    }
    /*private void OnTriggerEnter2D(Collider2D a_other);*/

    /**/
    /*
     * SetPickedUpWeapon()
     * NAME
     *  SetPickedUpWeapon - setter for m_pickedUpWeapon.
     * SYNOPSIS
     *  void SetPickedUpWeapon(Weapons a_pickedUpWeapon)
     *      a_pickedUpWeapon --> the value m_pickedUpWeapon will be assigned to. 
     * DESCRIPTION
     *  The value of a_pickedUpWeapon is assigned to m_pickedUpWeapon. 
     * RETURNS
     *  None
     */
    /**/
    public void SetPickedUpWeapon(Weapons a_pickedUpWeapon)
    {
        m_pickedUpWeapon = a_pickedUpWeapon; 
    }
    /*public void SetPickedUpWeapon(Weapons a_pickedUpWeapon);*/
}
