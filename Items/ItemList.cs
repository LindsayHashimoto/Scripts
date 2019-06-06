using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemList
{
    
    public static Weapons m_knife = new Weapons("Knife", 30, 10, 10, 95, true);
    public static Weapons m_legendarySword = new Weapons("Legendary Sword", 999, 100000, 100, 95, false);
    public static Weapons m_claw = new Weapons("Claw", 999, 1, 10, 95, false);
    public static Weapons m_toySword = new Weapons("Toy Sword", 30, 1, 0, 95, false); 

    public static Potions m_minorPotion = new Potions("Minor Health Potion", 3, 30, 20);
    public static Potions m_normalPotion = new Potions("Health Potion", 3, 50, 50);

}
