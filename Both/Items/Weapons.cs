using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : Items {

    private int m_damage;
    private int m_accuracy;
    private bool m_isThrowable;

    /**/
    /*
     * Weapons()
     * NAME
     *  Weapons - constructor of a weapon. 
     * SYNOPSIS
     *  Weapons(string a_name, int a_durability, int a_sellPrice, int a_damage, int a_accuracy, bool a_isThrowable)
     *      a_name --> the name of the weapon. 
     *      a_durability --> how many times a weapon can be used before being destroyed. 
     *      a_sellPrice --> how much money this will sell to a shopkeeper. 
     *      a_damage --> how much damage this will do before damage calculation.  
     *      a_accuracy --> the chance of this weapon hitting its target. 
     *      a_isThrowable --> if true, this item can be thrown in the overworld. 
     * DESCRIPTION
     *  This is the constructor for the Weapons class. This sets the values of the member variables in this class and the 
     *  parent class: Items. 
     * RETURNS
     *  None
     */
    /**/
    public Weapons(string a_name, int a_durability, int a_sellPrice, int a_damage, int a_accuracy, bool a_isThrowable)
    {
        m_name = a_name;
        m_durability = a_durability;
        m_sellPrice = a_sellPrice; 
        m_damage = a_damage;
        m_accuracy = a_accuracy;
        m_isThrowable = a_isThrowable;
        m_isWeapon = true; 
    }
    /*public Weapons(string a_name, int a_durability, int a_sellPrice, int a_damage, int a_accuracy, bool a_isThrowable);*/

    /**/
    /*
     * GetDamage()
     * NAME
     *  GetDamage - accessor for m_damage
     * SYNOPSIS
     *  int GetDamage()
     * DESCRIPTION
     *  Returns the amount of damage this weapon will do without damage calulaiton. 
     * RETURNS
     *  m_damage
     */
    /**/
    public int GetDamage()
    {
        return m_damage; 
    }
    /*public int GetDamage();*/

    /**/
    /*
     * GetAccuracy()
     * NAME
     *  GetAccuracy - accessor for m_accuracy
     * SYNOPSIS
     *  int GetAccuracy()
     * DESCRIPTION
     *  Returns the accuracy of the weapon. 
     * RETURNS
     *  m_accuracy
     */
    /**/
    public int GetAccuracy()
    {
        return m_accuracy; 
    }
    /*public int GetAccuracy();*/

    /**/
    /*
     * GetIsThrowable()
     * NAME
     *  GetIsTrowable - accessor for m_isThrowable
     * SYNOPSIS
     *  bool GetIsThrowable()
     * DESCRIPTION
     *  Returns the value of m_isThrowable. 
     * RETURNS
     *  m_isThrowable
     */
    /**/
    public bool GetIsThrowable()
    {
        return m_isThrowable; 
    }
    /*public bool GetIsThrowable();*/
}
