using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private List<Items> m_inventory = new List<Items>();
    private int m_currency;

    private bool m_NeedToUpdate = false; 

	// Use this for initialization
	void Start ()
    {
        //test data
        m_inventory.Add(ItemList.m_knife);
        //m_inventory.Add(ItemList.m_legendarySword);
        m_inventory.Add(ItemList.m_minorPotion);
        m_inventory.Add(ItemList.m_toySword);
        m_currency = 100; 
        m_NeedToUpdate = true; 
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void BuyItem(Items a_item)
    {
        m_currency -= a_item.GetSellPrice(); 
        m_inventory.Add(a_item);
        UpdateInventory(); 
    }

    public void SellItem(Items a_item)
    {
        m_currency += a_item.GetSellPrice();
        m_inventory.Remove(a_item);
        UpdateInventory();
    }

    public void PickUpItem(Items a_item)
    {
        m_inventory.Add(a_item);
        UpdateInventory(); 
    }

    public void AddItems(Items a_item, int a_amountToAdd)
    {
        for(int i = 0; i<= a_amountToAdd; i++)
        {
            m_inventory.Add(a_item); 
        }
        UpdateInventory();
    }

    public List<Items> GetInventory()
    {
        return m_inventory; 
    }

    public void UpdateInventory()
    {
        List<Items> newList = new List<Items>();
        foreach (Items item in m_inventory)
        {
            if (item.GetDurability() > 0)
            {
                newList.Add(item);
            }
        }
        m_inventory = newList;
        m_NeedToUpdate = true; 
    }  

    public void Updated()
    {
        m_NeedToUpdate = false; 
    }

    public bool GetIfNeedUpdate()
    {
        return m_NeedToUpdate; 
    }

    public int GetCurrency()
    {
        return m_currency;
    }

    public void RecieveCurrency(int a_amountToGive)
    {
        m_currency += a_amountToGive;
    }

}
