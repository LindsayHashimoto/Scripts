using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class InventoryManager : MonoBehaviour {

    private Items m_activeItem;
    private Weapons m_registeredWeapon; 
    private Inventory m_playerInventory;

    private GameObject m_inventoryBtnParent; 
    private List<Button> m_inventoryBtns = new List<Button>();
    private Button m_useBtn;
    private Button m_registerBtn;
    private Button m_buySellBtn;
    private Button m_cancelBtn; 
    private Text m_itemDescription;
    private GameObject m_inventoryList;

    private GameObject m_smsObj;
    private GameObject m_inventoryMenu;

    private Text m_currencyTxt;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization.
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  This sets the inital values of the above member variables and adds on click listeners to the register and 
     *  cancel buttons. The register button and the buy and sell button are set to be not active and the use button 
     *  is set to be active. Then, the interface elements are built. 
     * RETURNS
     *  None
     */
    /**/
    void Start ()
    {
        m_smsObj = SceneManagerScript.m_sm.gameObject;
        m_inventoryMenu = m_smsObj.transform.Find("Canvas").gameObject.transform.Find("Inventory Menu").gameObject;

        m_playerInventory = m_smsObj.transform.Find("Allies").gameObject.GetComponentInChildren<Inventory>();

        m_inventoryBtnParent = m_inventoryMenu.transform.Find("Inventory List").gameObject.transform.Find("Inventory UI Grid").gameObject;
        m_inventoryBtns.Add(m_inventoryBtnParent.GetComponentInChildren<Button>());

        m_useBtn = m_inventoryMenu.transform.Find("Use Item").gameObject.GetComponent<Button>();
        m_registerBtn = m_inventoryMenu.transform.Find("Register Button").gameObject.GetComponent<Button>(); 
        m_buySellBtn = m_inventoryMenu.transform.Find("Buy and Sell Button").gameObject.GetComponent<Button>();
        m_cancelBtn = m_inventoryMenu.transform.Find("Cancel").gameObject.GetComponent<Button>();
        m_itemDescription = m_inventoryMenu.transform.Find("Item Description Text").gameObject.GetComponent<Text>();

         
        m_currencyTxt = m_inventoryMenu.transform.Find("Currency").gameObject.GetComponent<Text>();
        m_inventoryList = m_inventoryMenu.transform.Find("Inventory List").gameObject;

        m_registerBtn.onClick.AddListener(OnRegiserClick);
        m_cancelBtn.onClick.AddListener(OnCancelClick); 


        m_registerBtn.gameObject.SetActive(false);
        m_buySellBtn.gameObject.SetActive(false); 
  
        m_useBtn.gameObject.SetActive(true);


        UpdateUIInventory(); 
    }
    /*void Start();*/

    /**/
    /*
     * Update()
     * NAME 
     *  Update - Update is called once per frame.
     * SYNOPSIS
     *  void Update()
     * DESCRIPTION
     *  If the inventory user interface needs to be updated, this will update it. 
     * RETURNS
     *  None
     */
    /**/
    void Update ()
    {
        if (m_playerInventory.GetIfNeedUpdate())
        {
            UpdateUIInventory(); 
        }
	}
    /*void Update();*/

    //https://docs.unity3d.com/ScriptReference/UI.Button-onClick.html
    /**/
    /*
     * BuildUIInventory()
     * NAME
     *  BuildUIInventory - builds the interface of the inventory.
     * SYNOPSIS
     *  void BuildUIInventory()
     * DESCRIPTION
     *  This clones the original button up to the number of items in the player's inventory. The button's text 
     *  matches the names of the items in the inventory. On click listeners are added to each of the buttons.
     * RETURNS
     *  None
     */
    /**/
    public void BuildUIInventory()
    {
        Button origin = m_inventoryBtns[0];
        m_inventoryBtns.Clear(); 
        for (int i = 0; i < m_playerInventory.GetInventory().Count; i++)
        {
            m_inventoryBtns.Add(Instantiate(origin, m_inventoryBtnParent.transform, true));
            m_inventoryBtns[i].GetComponentInChildren<Text>().text = m_playerInventory.GetInventory()[i].GetName();
            m_inventoryBtns[i].onClick.AddListener(OnItemClick);
            m_inventoryBtns[i].gameObject.SetActive(true);
        }
    }
    /*public void BuildUIInventory();*/

    /**/
    /*
     * UpdateCurrency()
     * NAME
     *  UpdateCurrency - updates the UI element that shows the user how much money they have.
     * SYNOPISIS
     *  void UpdateCurrency()
     * DESCRIPTION
     *  This sets the text of the currency text to show the user how much money they have.
     * RETURNS
     *  None
     */
    /**/
    public void UpdateCurrency()
    {
        m_currencyTxt.text = "You have: $" + m_playerInventory.GetCurrency();
    }
    /*public void UpdateCurrency();*/

    /**/
    /*
     * UpdateUIInventory()
     * NAME
     *  UpdateUIInvntory - is called whenever the inventory interface needs to be updated. 
     * SYNOPSIS
     *  void UpdateUIInventory()
     * DESCRIPTION
     *  First, this informs m_playerInventory that the inventory no longer needs to be updated. Then this destroys 
     *  and rebuilds the buttons when this needs to be updated. It also updates the currency and sets the active item
     *  to be null.   
     * RETURNS
     *  None
     */
    /**/
    public void UpdateUIInventory()
    {
        m_playerInventory.Updated();
        Button[] btns = m_inventoryBtnParent.GetComponentsInChildren<Button>(); 
        foreach (Button button in btns)
        {
            button.gameObject.SetActive(false);
        }
        BuildUIInventory();
        ResetActiveItem();
        UpdateCurrency(); 
    }
    /*public void UpdateUIInventory();*/

    /**/
    /*
     * ResetActiveItem()
     * NAME
     *  ResetActiveItem - makes the player have no active item. 
     * SYNOPSIS
     *  void ResetActiveItem()
     * DESCRIPTION
     *  The active item is set to be null, the buttons that appear when an item is click are set to not be active and 
     *  the description text is set to say "No Item Currently Selected.".
     * RETURNS
     *  None
     */
    /**/
    public void ResetActiveItem()
    {
        m_useBtn.gameObject.SetActive(false);
        m_registerBtn.gameObject.SetActive(false);
        m_activeItem = null;
        m_itemDescription.text = "No Item Currently Selected.";
    }
    /*public void ResetActiveItem();*/

    /**/
    /*
     * ResetRegisteredWeapon()
     * NAME
     *  ResetRegisteredWeapon - resets the current registered weapon.
     * SYNOPSIS
     *  void ResetRegisteredWeapon()
     * DESCRIPTION
     *  m_registeredWeapon is set to null. 
     * RETURNS
     *  None
    /**/
    public void ResetRegisteredWeapon()
    {
        m_registeredWeapon = null; 
    }
    /*public void ResetRegisteredWeapon();*/

    /**/
    /*
     * OnItemClick()
     * NAME
     *  OnItemClick - is called when the user clicks on an item button. 
     * SYNOPSIS
     *  void OnItemClick()
     * DESCRIPTION
     *  When the user clicks on an item, a short description is given. If the items is throwable, the register button 
     *  appears. The use button also is set to be active. 
     * RETURNS
     *  None
     */
    /**/
    private void OnItemClick()
    {
        //https://answers.unity.com/questions/828666/46-how-to-get-name-of-button-that-was-clicked.html
        GameObject thisButton = EventSystem.current.currentSelectedGameObject.gameObject;
        m_registerBtn.gameObject.SetActive(false);
        int index = 0; 
        foreach (Button button in m_inventoryBtns)
        {
            if (button.gameObject == thisButton)
            {
                m_activeItem = m_playerInventory.GetInventory()[index]; 
                break;
            }
            index++; 
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
                if (m_activeItem == m_registeredWeapon)
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
    /*private void OnItemClick();*/

    /**/
    /*
     * OnCancelClick() 
     * NAME
     *  OnCancelClick - is called when the user clicks on the cancel button. 
     * SYNOPSIS
     *  void OnCancelClick()
     * DESCRIPTION
     *  When the user clicks on the cancel button, the inventory menu disappears and the active items is reset. 
     * RETURNS
     *  None
     */
    /**/
    private void OnCancelClick()
    {
        this.gameObject.SetActive(false);
        ResetActiveItem();
    }
    /*private void OnCancelClick();*/

    /**/
    /*
     * OnRegisterClick()
     * NAME
     *  OnRegisterClick - is called when the user clicks on the register button. 
     * SYNOPSIS
     *  void OnRegiserClick()
     * DESCRIPTION
     *  If the active item is already the current registered weapon, the registered weapon is un-registered. Ohterwise,
     *  the registered weapon is now the active item. Finally, the active item is reset. 
     * RETURNS
     *  None
     */
    /**/
    private void OnRegiserClick()
    {
        if (m_registeredWeapon == (Weapons)m_activeItem)
        {
            m_registeredWeapon = null;
        }
        else
        {
            m_registeredWeapon = (Weapons)m_activeItem;
        }
        ResetActiveItem();
    }
    /*private void OnRegiserClick();*/

    /**/
    /*
     * GetPlayerInventory()
     * NAME
     *  GetPlayerInventory - accessor for m_playerInventory. 
     * SYNOPSIS
     *  Inventory GetPlayerInventory()
     * DESCRIPTION
     *  Returns the player's inventory. 
     * RETURNS
     *  m_playerInventory
     */
    /**/
    public Inventory GetPlayerInventory()
    {
        return m_playerInventory;
    }
    /*public Inventory GetPlayerInventory();*/

    /**/
    /*
     * GetActiveItem()
     * NAME
     *  GetActiveItem - accessor for m_activeItem.
     * SYNOPSIS
     *  Items GetActiveItem()
     * DESCRIPTION
     *  Returns the current active item. 
     * RETURNS
     *  m_activeItem
     */
    /**/
    public Items GetActiveItem()
    {
        return m_activeItem;
    }
    /*public Items GetActiveItem();*/

    /**/
    /*
     * GetRegisteredWeapon()
     * NAME
     *  GetRegisteredWeapon - accessor for m_registeredWeapon. 
     * SYNOPSIS
     *  Weapons GetRegisteredWeapon()
     * DESCRIPTION
     *  Returns the weapon that the player set to be thrown. 
     * RETURNS
     *  m_registeredWeapon
     */
    /**/
    public Weapons GetRegisteredWeapon()
    {
        return m_registeredWeapon; 
    }
    /*public Weapons GetRegisteredWeapon();*/
}
