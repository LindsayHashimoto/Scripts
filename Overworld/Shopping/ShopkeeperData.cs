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

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization.
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  This sets the intial values of the above memeber variables, builds the shop data and sets the interface 
     *  to be not active. 
     * RETURNS
     *  None
     */
    /**/
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
    /*void Start();*/

    /**/
    /*
     * BuildShopData()
     * NAME
     *  BuildShopData - build the user interface for the items that the shopkeeper sells. 
     * SYNOPSIS
     *  void BuildShopData()
     * DESCRIPTION
     *  The items that the shopkeeper sells are displayed as buttons. The buttons gain the on click listener 
     *  "OnItemClick". The amount of money the player has is also displayed. 
     * RETURNS
     *  None
     */
    /**/
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
    /*public void BuildShopData();*/

    /**/
    /*
     * UpdateShopData()
     * NAME
     *  UpdateShopData - the shop interface is updated.
     * SYNOPSIS
     *  void UpdateShopData()
     * DESCRIPTION
     *  The shop interface is rebuilt and the active item is reset. 
     * RETURNS
     *  None
     */
    /**/
    public void UpdateShopData()
    {
        foreach (Button button in m_itemBtns)
        {
            button.gameObject.SetActive(false);
        }
        BuildShopData();
        ResetActiveItem();
    }
    /*public void UpdateShopData();*/

    /**/
    /*
     * ResetActiveItem()
     * NAME
     *  ResetActiveItem - sets the current active item to be null. 
     * SYNOPSIS
     *  void ResetActiveItem()
     * DESCRIPTION
     *  The active item is set to null and the text describing the active item is set to "No Item Currently Selected.".
     * RETURNS
     *  None
     */
    /**/
    public void ResetActiveItem()
    {
        m_activeItem = null;
        m_itemDescription.text = "No Item Currently Selected.";
    }
    /*public void ResetActiveItem();*/

    /**/
    /*
     * OnItemClick()
     * NAME
     *  OnItemClick - is called when the user clicks on an item. 
     * SYNOPSIS
     *  void OnItemClick()
     * DESCRIPTION
     *  When an item is clicked, a short description of the item appears and the buy and sell button is set to be 
     *  active. 
     * RETURNS
     *  None
     */
    /**/
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
    /*private void OnItemClick();*/

    /**/
    /*
     * GetActiveItem()
     * NAME
     *  GetActiveItem - accessor for m_activeItem
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
}
