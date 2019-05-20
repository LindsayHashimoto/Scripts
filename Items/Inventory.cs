using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class Inventory : MonoBehaviour {

    private List<Items> m_inventory = new List<Items>();
    private int m_currency;
    public Button [] m_UIbuttons;
    public Text m_descriptionText;
    public Button m_cancelButton;
    public Button m_useButton;
    private Items m_activeItem;
    public bool m_isPlayer; 
	// Use this for initialization
	void Start ()
    {
//        m_inventory.Add(ItemList.m_knife);
//        m_inventory.Add(ItemList.m_legendarySword);
//        m_inventory.Add(ItemList.m_minorPotion);
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
        UpdateUIInventory(); 
    }

    public void PickUpItem(Items a_item)
    {
        m_inventory.Add(a_item);
        UpdateUIInventory(); 
    }

    public Items GetActiveItem()
    {
        return m_activeItem; 
    }

    public List<Items> GetInventory()
    {
        return m_inventory; 
    }

    public void UpdateUIInventory()
    {
        if (!m_isPlayer)
        {
            return;
        }
        foreach (Button button in m_UIbuttons)
        {
            button.gameObject.SetActive(false); 
        }
        List<Items> newList = new List<Items>();  
        foreach (Items item in m_inventory)
        {
            if (item.GetDurability() > 0)
            {
                newList.Add(item); 
            }
        }
        m_inventory = newList; 
        BuildUIInventory();
        m_useButton.gameObject.SetActive(false);
        m_activeItem = null;
        m_descriptionText.text = "No Item Currently Selected.";
    }
    //https://docs.unity3d.com/ScriptReference/UI.Button-onClick.html
    private void BuildUIInventory()
    {
        if (!m_isPlayer)
        {
            return; 
        }
        for (int i = 0; i < m_inventory.Count; i++)
        {
            m_UIbuttons[i].GetComponentInChildren<Text>().text = m_inventory[i].GetName();
            m_UIbuttons[i].onClick.AddListener(OnItemClick); 
            m_UIbuttons[i].gameObject.SetActive(true); 
        }
    }

    private void OnItemClick()
    {
        //https://answers.unity.com/questions/828666/46-how-to-get-name-of-button-that-was-clicked.html
        GameObject thisButton = EventSystem.current.currentSelectedGameObject.gameObject;
        string itemName = thisButton.GetComponentInChildren<Text>().text;
        foreach (Items item in m_inventory)
        {
            if(itemName == item.GetName())
            {
                m_activeItem = item;
                break; 
            }
        }
        
        if (m_activeItem.GetIsWeapon())
        {
            m_descriptionText.text = "This is a weapon called " + m_activeItem.GetName() +
                ". It can be used " + m_activeItem.GetDurability() + " more times. " +
                "It has a power of " + ((Weapons)m_activeItem).GetDamage() + " and an accuracy of " +
                ((Weapons)m_activeItem).GetAccuracy();
                    
        }
        if (m_activeItem.GetIsPotion())
        {
            m_descriptionText.text = "This is a potion called " + m_activeItem.GetName() +
                ". It can be used " + m_activeItem.GetDurability() + " more times. " +
                "It will heal for " + ((Potions)m_activeItem).GetHeal() + " health. ";
        }
        m_useButton.gameObject.SetActive(true); 
    }
}
