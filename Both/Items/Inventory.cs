using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private List<Items> m_inventory = new List<Items>();
    private int m_currency;

    private bool m_NeedToUpdate;
 
    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization.
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  This adds items that are initally in the inventory. 
     * RETURNS
     *  None
     */
    /**/
    void Start ()
    {
        //test data 
        m_inventory.Add(new Weapons(ItemList.m_knife));
        //m_inventory.Add(ItemList.m_legendarySword);
        m_inventory.Add(new Potions(ItemList.m_minorPotion));
        m_inventory.Add(new Weapons(ItemList.m_toySword));
        m_currency = 100; 
        m_NeedToUpdate = true; 
    }
    /*void Start();*/

    /**/
    /*
     * BuyItem()
     * NAME
     *  BuyItem - is called when the player purchases a new item. 
     * SYNOPSIS
     *  void BuyItem(Items a_item)
     *      a_item --> the item that is being bought. 
     * DESCRIPTION
     *  If the player has less money than what the item is worth, they should not be able to buy it. Otherwise, the 
     *  amount of money the item is worth is deducted from the player's currency and the item is added to their 
     *  inventory. 
     * RETURNS
     *  None
     */
    /**/
    public void BuyItem(Items a_item)
    {
        if (m_currency < a_item.GetSellPrice())
        {
            return; 
        }
        m_currency -= a_item.GetSellPrice();
        if (a_item.GetIsWeapon())
        {
            m_inventory.Add(new Weapons((Weapons)a_item));
        }
        else if (a_item.GetIsPotion())
        {
            m_inventory.Add(new Potions((Potions)a_item));
        }
        UpdateInventory(); 
    }
    /*public void BuyItem(Items a_item)*/

    /**/
    /*
     * SellItem()
     * NAME
     *  SellItem - is called when the user sells an item. 
     * SYNOPSIS
     *  void SellItem(Items a_item)
     *      a_item --> the item that will be sold. 
     * DESCRIPITON
     *  The item is removed from the player's inventory and they gain currency equal to the price of the item. 
     * RETURNS
     *  None
     */
    /**/
    public void SellItem(Items a_item)
    {
        m_currency += a_item.GetSellPrice();
        m_inventory.Remove(a_item);
        UpdateInventory();
    }
    /*public void SellItem(Items a_item);*/

    /*public void PickUpItem(Items a_item);*/

    /**/
    /*
     * AddItems()
     * NAME 
     *  AddItems - a specified amount of items is added to the inventory. 
     * SYNOPSIS
     *  void AddItems(Items a_item, int a_amountToAdd)
     *      a_item --> the item that will be added to the inventory.
     *      a_amountToAdd --> the number of items that will be added. 
     * DESCRIPTION
     *  A specified amount of the same item is added to the inventory. 
     * RETURNS 
     *  None
     */
    /**/
    public void AddItems(Items a_item, int a_amountToAdd)
    {
        for(int i = 1; i <= a_amountToAdd; i++)
        {
            if (a_item.GetIsWeapon())
            {
                m_inventory.Add(new Weapons((Weapons)a_item));
            }
            else if (a_item.GetIsPotion())
            {
                m_inventory.Add(new Potions((Potions)a_item));
            } 
        }
        UpdateInventory();
    }
    /*public void AddItems(Items a_item, int a_amountToAdd);*/

    /**/
    /*
     * RecieveCurrency()
     * NAME
     *  RecieveCurrency - the user recieves a specified amount of currency. 
     * SYNOPSIS
     *  void RecieveCurrency(int a_amountToGive)
     *      a_amountToGive --> the amount of currency that will be added. 
     * DESCRIPTION
     *  The amount specified by a_amountToGive is added to m_currency. 
     * RETURNS
     *  None
     */
    /**/
    public void RecieveCurrency(int a_amountToGive)
    {
        m_currency += a_amountToGive;
        UpdateInventory();
    }
    /*public void RecieveCurrency(int a_amountToGive);*/

    /**/
    /*
     * UpdateInventory()
     * NAME
     *  UpdateInventory - is called whenever the inventory needs to update.
     * SYNOPSIS
     *  void UpdateInventory()
     * DESCRIPTION
     *  All items that have no durability left are removed from the inventory. This also lets the InventoryManager 
     *  class know that the interface needs to be updated. 
     * RETURNS
     *  None
     */
    /**/
    public void UpdateInventory()
    {
        foreach (Items item in m_inventory)
        {
            if (item.GetDurability() <= 0)
            {
                m_inventory.Remove(item);
            }
        }
        m_NeedToUpdate = true; 
    }
    /*public void UpdateInventory();*/

    /**/
    /*
     * Updated()
     * NAME
     *  Updated - is called when InventoryManager has updated the interface. 
     * SYNOPSIS
     *  void Updated()
     * DESCRIPTION
     *  This lets the update loop in InventoryManager know that the inventory interface no longer needs to be updated.  
     * RETURNS
     *  None
     */
    /**/
    public void Updated()
    {
        m_NeedToUpdate = false; 
    }
    /*public void Updated();*/

    /**/
    /*
     * GetIfNeedUpdate()
     * NAME
     *  GetIfNeedUpdate - accessor for m_NeedToUpdate.
     * SYNOPSIS
     *  bool GetIfNeedUpdate()
     * DESCRIPTION
     *  Lets the InventoryManager class know if the interface needs to be updated. 
     * RETURNS
     *  m_NeedToUpdate
     */
    /**/
    public bool GetIfNeedUpdate()
    {
        return m_NeedToUpdate; 
    }
    /*public bool GetIfNeedUpdate();*/

    /**/
    /*
     * GetCurrency()
     * NAME
     *  GetCurrency - accessor for m_currency.
     * SYNOPSIS
     *  int GetCurrency()
     * DESCRIPTION
     *  Returns the amount of money the player has. 
     * RETURNS
     *  m_currency
     */
    /**/
    public int GetCurrency()
    {
        return m_currency;
    }
    /*public int GetCurrency();*/

    /**/
    /*
     * GetInventory()
     * NAME
     *  GetInventory - accessor for m_inventory.
     * SYNOPSIS
     *  List<Items> GetInventory()
     * DESCRIPTION
     *  Returns the list of items in the inventory. 
     * RETURNS
     *  m_inventory
     */
    /**/
    public List<Items> GetInventory()
    {
        return m_inventory;
    }
    /*public List<Items> GetInventory();*/

   
}
