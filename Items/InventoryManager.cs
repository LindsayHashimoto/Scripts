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
    private Text m_itemDescription; 

    // Use this for initialization
    void Start ()
    {
        m_playerInventory = GameObject.FindGameObjectWithTag("Allies").GetComponentInChildren<Inventory>();
        m_inventoryBtns = GetInventoryButtons();
        m_useBtn = GameObject.FindGameObjectWithTag("UseBtn").GetComponent<Button>();
        m_cancelBtn = GameObject.FindGameObjectWithTag("CancelBtn").GetComponent<Button>();
        m_registerBtn = GameObject.FindGameObjectWithTag("RegisterBtn").GetComponent<Button>(); 
        m_itemDescription = GameObject.FindGameObjectWithTag("ItemTxt").GetComponent<Text>();

        m_cancelBtn.onClick.AddListener(OnCancelClick);
        m_registerBtn.onClick.AddListener(OnRegiserClick); 


        m_useBtn.gameObject.SetActive(false);
        m_registerBtn.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
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
        Button[] btns = GameObject.FindGameObjectWithTag("InventoryUI").GetComponentsInChildren<Button>();
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
                ((Weapons)m_activeItem).GetAccuracy();

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
                "It will heal for " + ((Potions)m_activeItem).GetHeal() + " health. ";
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
        this.gameObject.SetActive(false);
        ResetActiveItem(); 
    }
}
