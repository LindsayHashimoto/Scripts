using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class ShopkeeperData : MonoBehaviour {

    private List<Items> m_itemList = new List<Items>();

    private GameObject m_itemsToBuy;
    private Button[] m_itemBtns;

    private Inventory m_playerInventory;
    

    private Items m_activeItem;

    private GameObject m_smsobj;
    private GameObject m_inventoryMenu; 
    private Text m_itemDescription;
    private GameObject m_inventoryList;
    private Text m_currencyTxt;
    private Button m_buySellBtn;
    // Use this for initialization
    void Start ()
    {
        m_itemList.Add(ItemList.m_knife);
        m_itemsToBuy = GameObject.Find("ShopKeeper Items");
        m_itemBtns = m_itemsToBuy.GetComponentsInChildren<Button>();

        m_playerInventory = GameObject.FindGameObjectWithTag("Allies").GetComponentInChildren<Inventory>();
        


        m_smsobj = SceneManagerScript.m_sm.gameObject;
        m_inventoryMenu = m_smsobj.transform.Find("Canvas").gameObject.transform.Find("Inventory Menu").gameObject;
        m_itemDescription = m_inventoryMenu.transform.Find("Item Description Text").gameObject.GetComponent<Text>();
        m_inventoryList = m_inventoryMenu.transform.Find("Inventory List").gameObject;
        m_currencyTxt = m_inventoryMenu.transform.Find("Currency").gameObject.GetComponent<Text>();
        m_buySellBtn = m_inventoryMenu.transform.Find("Buy and Sell Button").gameObject.GetComponent<Button>();

        m_inventoryList.SetActive(false); 
        UpdateShopData();
        m_itemsToBuy.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void BuildShopData()
    {
        for (int i = 0; i < m_itemList.Count; i++)
        {
            m_itemBtns[i].GetComponentInChildren<Text>().text = m_itemList[i].GetName();
            m_itemBtns[i].onClick.AddListener(OnItemClick);
            m_itemBtns[i].gameObject.SetActive(true);
        }
        m_currencyTxt.text = "You have: $" + m_playerInventory.GetCurrency();
    }

    public void UpdateShopData()
    {
        foreach (Button button in m_itemBtns)
        {
            button.gameObject.SetActive(false);
        }
        BuildShopData();
        ResetActiveItem();
    }

    public void ResetActiveItem()
    {
        m_activeItem = null;
        m_itemDescription.text = "No Item Currently Selected.";
    }

    private void OnItemClick()
    { 
        GameObject thisButton = EventSystem.current.currentSelectedGameObject.gameObject;
        string itemName = thisButton.GetComponentInChildren<Text>().text;
        foreach (Items item in m_itemList)
        {
            if (itemName == item.GetName())
            {
                m_activeItem = item;
                break;
            }
        }

        if (m_activeItem.GetIsWeapon())
        {
            m_itemDescription.text = "This is a weapon called " + m_activeItem.GetName() +
                ". It can be used " + m_activeItem.GetDurability() + " times. " +
                "It has a power of " + ((Weapons)m_activeItem).GetDamage() + " and an accuracy of " +
                ((Weapons)m_activeItem).GetAccuracy() + ". It will cost you $" + m_activeItem.GetSellPrice();
        }
        if (m_activeItem.GetIsPotion())
        {
            m_itemDescription.text = "This is a potion called " + m_activeItem.GetName() +
                ". It can be used " + m_activeItem.GetDurability() + " more times. " +
                "It will heal for " + ((Potions)m_activeItem).GetHeal() + " health. It will cost you $" + m_activeItem.GetSellPrice();
        }
        m_buySellBtn.gameObject.SetActive(true);
    }
    public Items GetActiveItem()
    {
        return m_activeItem; 
    }
}
