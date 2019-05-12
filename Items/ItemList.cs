using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Weapons m_knife = new Weapons("Knife", 30, 10, 95, true);
    public static Weapons m_legendarySword = new Weapons("Legendary Sword", 999, 100, 95, false);
    public static Potions m_minorPotion = new Potions("Minor Health Potion", 1, 20);
    public static Potions m_normalPotion = new Potions("Health Potion", 1, 50);
}
