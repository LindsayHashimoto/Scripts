using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class InventoryManager : MonoBehaviour {

    private Items m_activeItem;
    private Weapons m_registeredWeapon; 
    private Inventory m_playerInventory;

    private Button[] m_inventoryBtns;
    private Button m_useBtn;
    private Button m_cancelBtn;
    private Button m_registerBtn;
    private Button m_buySellBtn; 
    private Text m_itemDescription;
    private GameObject m_inventoryList;

    private GameObject m_smsObj;
    private GameObject m_inventoryMenu;

    private Text m_currencyTxt; 

    // Use this for initialization
    void Start ()
    {
        m_smsObj = SceneManagerScript.m_sm.gameObject;
        m_inventoryMenu = m_smsObj.transform.Find("Canvas").gameObject.transform.Find("Inventory Menu").gameObject;

        m_playerInventory = m_smsObj.transform.Find("Allies").gameObject.GetComponentInChildren<Inventory>();
        m_inventoryBtns = GetInventoryButtons();
        m_useBtn = m_inventoryMenu.transform.Find("Use Item").gameObject.GetComponent<Button>();
        m_registerBtn = m_inventoryMenu.transform.Find("Register Button").gameObject.GetComponent<Button>(); 
        m_buySellBtn = m_inventoryMenu.transform.Find("Buy and Sell Button").gameObject.GetComponent<Button>(); 
        m_itemDescription = m_inventoryMenu.transform.Find("Item Description Text").gameObject.GetComponent<Text>();

         
        m_currencyTxt = m_inventoryMenu.transform.Find("Currency").gameObject.GetComponent<Text>();
        m_inventoryList = m_inventoryMenu.transform.Find("Inventory List").gameObject;

        m_cancelBtn.onClick.AddListener(OnCancelClick);
        m_registerBtn.onClick.AddListener(OnRegiserClick); 


        m_registerBtn.gameObject.SetActive(false);
        m_buySellBtn.gameObject.SetActive(false); 
  
        m_useBtn.gameObject.SetActive(true);


        UpdateUIInventory(); 
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_playerInventory.GetIfNeedUpdate())
        {
            UpdateUIInventory(); 
        }
	}

    //https://docs.unity3d.com/ScriptReference/UI.Button-onClick.html
    public void BuildUIInventory()
    {
        for (int i = 0; i < m_playerInventory.GetInventory().Count; i++)
        {
            m_inventoryBtns[i].GetComponentInChildren<Text>().text = m_playerInventory.GetInventory()[i].GetName();
            m_inventoryBtns[i].onClick.AddListener(OnItemClick);
            m_inventoryBtns[i].gameObject.SetActive(true);
        }
        m_currencyTxt.text = "You have: $" + m_playerInventory.GetCurrency();
    }

    public void UpdateUIInventory()
    {
        foreach (Button button in m_inventoryBtns)
        {
            button.gameObject.SetActive(false);
        }
        BuildUIInventory();
        ResetActiveItem();
        m_playerInventory.Updated(); 
    }

    public void ResetActiveItem()
    {
        m_useBtn.gameObject.SetActive(false);
        m_registerBtn.gameObject.SetActive(false);
        m_activeItem = null;
        m_itemDescription.text = "No Item Currently Selected.";
    }

    public Items GetActiveItem()
    {
        return m_activeItem;
    }

    public Weapons GetRegisteredWeapon()
    {
        return m_registeredWeapon; 
    }

    private Button [] GetInventoryButtons()
    { 
        Button[] btns = m_inventoryMenu.transform.Find("Inventory List").gameObject.transform.Find("Inventory UI Grid").GetComponentsInChildren<Button>(true);
        m_cancelBtn = btns[btns.Length - 1]; 
        Button [] inventoryBtns = new Button[btns.Length - 1];
        for (int i = 0; i < btns.Length - 1; i++)
        {
            inventoryBtns[i] = btns[i];
            inventoryBtns[i].gameObject.SetActive(false); 
        }
        return inventoryBtns; 
    }

    private void OnItemClick()
    {
        //https://answers.unity.com/questions/828666/46-how-to-get-name-of-button-that-was-clicked.html
        GameObject thisButton = EventSystem.current.currentSelectedGameObject.gameObject;
        string itemName = thisButton.GetComponentInChildren<Text>().text;
        m_registerBtn.gameObject.SetActive(false);
        foreach (Items item in m_playerInventory.GetInventory())
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
                ". It can be used " + m_activeItem.GetDurability() + " more times. " +
                "It has a power of " + ((Weapons)m_activeItem).GetDamage() + " and an accuracy of " +
                ((Weapons)m_activeItem).GetAccuracy() + " It is worth $" + m_activeItem.GetSellPrice();

            if (((Weapons)m_activeItem).GetIsThrowable())
            {
                m_registerBtn.gameObject.SetActive(true); 
                if(m_activeItem == m_registeredWeapon)
                {
                    m_registerBtn.GetComponentInChildren<Text>().text = "Unregister"; 
                }
                else
                {
                    m_registerBtn.GetComponentInChildren<Text>().text = "Register";
                }
            }
        }
        if (m_activeItem.GetIsPotion())
        {
            m_itemDescription.text = "This is a potion called " + m_activeItem.GetName() +
                ". It can be used " + m_activeItem.GetDurability() + " more times. " +
                "It will heal for " + ((Potions)m_activeItem).GetHeal() + " health. It is worth $" + m_activeItem.GetSellPrice();
        }

        m_useBtn.gameObject.SetActive(true);
    }

    private void OnCancelClick()
    {
        this.gameObject.SetActive(false);
        ResetActiveItem(); 
    }

    private void OnRegiserClick()
    {
        if (m_registeredWeapon == (Weapons) m_activeItem)
        {
            m_registeredWeapon = null;
        }
        else
        {
            m_registeredWeapon = (Weapons)m_activeItem;
        }
        ResetActiveItem(); 
    }

    public Inventory GetPlayerInventory()
    {
        return m_playerInventory; 
    }
}
