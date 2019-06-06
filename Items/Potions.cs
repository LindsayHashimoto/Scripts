using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : Items {

    private int m_heal; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Potions(string a_name, int a_durability, int a_sellPrice, int a_heal)
    {
        m_name = a_name;
        m_durability = a_durability;
        m_sellPrice = a_sellPrice; 
        m_heal = a_heal;
        m_isPotion = true; 
    }

    public int GetHeal()
    {
        return m_heal; 
    }
}
