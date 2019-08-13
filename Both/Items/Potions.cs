using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : Items {

    private int m_heal;

    /**/
    /*
     * Potions()
     * NAME
     *  Potions - constructor for the Potions class. 
     * SYNOPSIS
     *  Potions(Potions a_potions)
     *      a_potions --> the member variables in this will be copied and set to this new
     *          Potions object. 
     * DESCRIPTION 
     *  This sets the values of the member variables in this class and the Items class. 
     * RETURNS 
     *  None
     */
    /**/
    public Potions(Potions a_potions)
    {
        m_name = a_potions.GetName();
        m_durability = a_potions.GetDurability();
        m_sellPrice = a_potions.GetSellPrice();
        m_heal = a_potions.GetHeal();
        m_isPotion = true;
    }
    /*public Potions(Potions a_potions);*/

    /**/
    /*
     * Potions()
     * NAME
     *  Potions - constructor for the Potions class. 
     * SYNOPSIS
     *  Potions(string a_name, int a_durability, int a_sellPrice, int a_heal)
     *      a_name --> the name of the potion
     *      a_durability --> how many more times the item can be used 
     *      a_sellPrice --> how much the item is worth to shops
     *      a_heal --> how much health the potion will heal
     * DESCRIPTION 
     *  This sets the values of the member variables in this class and the Items class. 
     * RETURNS 
     *  None
     */
    /**/
    public Potions(string a_name, int a_durability, int a_sellPrice, int a_heal)
    {
        m_name = a_name;
        m_durability = a_durability;
        m_sellPrice = a_sellPrice; 
        m_heal = a_heal;
        m_isPotion = true; 
    }
    /*public Potions(string a_name, int a_durability, int a_sellPrice, int a_heal);*/

    /**/
    /*
     * GetHeal()
     * NAME
     *  GetHeal - accessor for m_heal
     * SYNOPSIS
     *  int GetHeal()
     * DESCRIPTION
     *  Returns the amount of health this potion will heal.
     * RETURNS
     *  m_heal
     */
    /**/
    public int GetHeal()
    {
        return m_heal; 
    }
    /*public int GetHeal();*/
}
