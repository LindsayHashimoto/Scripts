using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items {

    protected int m_durability;
    protected string m_name;
    protected bool m_isWeapon = false;
    protected bool m_isPotion = false;
    protected int m_sellPrice;

    /**/
    /*
     * RemoveDurability()
     * NAME
     *  RemoveDurability - removes one durability point from the item. 
     * SYNOPSIS
     *  void RemoveDurability()
     * DESCRIPTION
     *  When an item is used, this should be called to remove one durability value from it. 
     * RETURNS
     *  None
     */
    /**/
    public void RemoveDurability()
    {
        m_durability--; 
    }
    /*public void RemoveDurability();*/

    /**/
    /*
     * SetDurability()
     * NAME
     *  SetDurability - setter for m_durability.
     * SYNOPSIS
     *  void SetDurability(int a_durability)
     *      a_durability --> the value m_durability will be set to. 
     * DESCRIPTION
     *  Sets the durability value to a set amount.
     * RETURNS
     *  None
     */
    /**/
    public void SetDurability(int a_durability)
    {
        m_durability = a_durability; 
    }
    /*public void SetDurability(int a_durability);*/

    /**/
    /*
     * GetName()
     * NAME
     *  GetName - accessor for m_name.
     * SYNOPSIS
     *  string GetName()
     * DESCRIPTION
     *  Returns the name of this item.
     * RETURNS
     *  m_name
     */
    /**/
    public string GetName()
    {
        return m_name; 
    }
    /*public string GetName();*/

    /**/
    /*
     * GetDurability()
     * NAME
     *  GetDurability - accessor for m_durability. 
     * SYNOPSIS
     *  int GetDurability()
     * DESCRIPTION
     *  Returns how much durability this item has left. 
     * RETURNS
     *  m_durability
     */
    /**/
    public int GetDurability()
    {
        return m_durability;
    }
    /*public int GetDurability();*/

    /**/
    /*
     * GetIsWeapon()
     * NAME
     *  GetIsWeapon - accessor for m_isWeapon.
     * SYNOPSIS
     *  bool GetIsWeapon()
     * DESCRIPTION
     *  Returns true if this item is a weapon, false if it is not.  
     * RETURNS
     *  m_isWeapon
     */
    /**/
    public bool GetIsWeapon()
    {
        return m_isWeapon; 
    }
    /*public bool GetIsWeapon();*/

    /**/
    /*
     * GetIsPotion()
     * NAME
     *  GetIsPotion - accessor for m_isPotion.
     * SYNOPSIS
     *  bool GetIsPotion()
     * DESCRIPTION
     *  Returns true if this is a potion, false if not. 
     * RETURNS
     *  m_isPotion
     */
    /**/
    public bool GetIsPotion()
    {
        return m_isPotion; 
    }
    /*public bool GetIsPotion();*/

    /**/
    /*
     * GetSellPrice()
     * NAME
     *  GetSellPrice - accessor for m_sellPrice.
     * SYNOPSIS
     *  int GetSellPrice()
     * DESCRIPTION
     *  Returns the sell price of this item. 
     * RETURNS
     *  m_sellPrice
     */
    /**/
    public int GetSellPrice()
    {
        return m_sellPrice; 
    }
    /*public int GetSellPrice();*/
}
