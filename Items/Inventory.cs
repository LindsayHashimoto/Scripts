using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; 

public class Inventory : MonoBehaviour {

    private List<Items> m_inventory = new List<Items>();
    private int m_currency;
    public Button [] m_UIbuttons;
    public Text m_descriptionText;
    public Button m_cancelButton;
    public Button m_useButton; 
    private Items m_activeItem; 
	// Use this for initialization
	void Start ()
    {
        m_inventory.Add(ItemList.m_knife);
        m_inventory.Add(ItemList.m_legendarySword);
        m_inventory.Add(ItemList.m_minorPotion);
        BuildUIInventory(); 
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void BuyItem(Items a_item, int a_cost)
    {
        m_currency -= a_cost; 
        m_inventory.Add(a_item); 
    }

    public void PickUpItem(Items a_item)
    {
        m_inventory.Add(a_item); 
    }

    //https://docs.unity3d.com/ScriptReference/UI.Button-onClick.html
    private void BuildUIInventory()
    {
        for (int i = 0; i < m_inventory.Count; i++)
        {
            m_UIbuttons[i].GetComponentInChildren<Text>().text = m_inventory[i].GetName();
            m_UIbuttons[i].onClick.AddListener(delegate { OnItemClick(i); }); 
            m_UIbuttons[i].gameObject.SetActive(true); 
        }
    }

    private void OnItemClick(int a_index)
    {
        m_activeItem = m_inventory[a_index]; 
        if (m_activeItem.GetIsWeapon())
        {
            m_descriptionText.text = "This is a Weapon called " + m_activeItem.GetName() +
                ". It can be used " + m_activeItem.GetDurability() + " more times. " +
                "It has a power of " + ((Weapons)m_activeItem).GetDamage() + " and an accuracy of " +
                ((Weapons)m_activeItem).GetAccuracy();
                    
        }
        if (m_activeItem.GetIsPotion())
        {
            m_descriptionText.text = "This is a Potion called " + m_activeItem.GetName() +
                ". It can be used " + m_activeItem.GetDurability() + " more times. " +
                "It will heal for " + ((Potions)m_activeItem).GetHeal() + " health. ";
        }
        m_useButton.gameObject.SetActive(true); 
    }
}
