using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour {
    private InventoryManager m_inventoryManager;
    private ExplinationText m_exTxt; 
	// Use this for initialization
	void Start ()
    {
        m_inventoryManager = FindObjectOfType<InventoryManager>();
        
        m_exTxt = FindObjectOfType<ExplinationText>(); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D a_other)
    {
        if(a_other.tag == "Player")
        {
            Inventory playerInventory = m_inventoryManager.GetPlayerInventory();
            Weapons registeredWeapon = m_inventoryManager.GetRegisteredWeapon();
            registeredWeapon.SetDurability(1);
            playerInventory.PickUpItem(registeredWeapon);
            this.gameObject.SetActive(false);
            m_exTxt.SetMessage("You picked up the " + registeredWeapon.GetName());
        }
    }
}
