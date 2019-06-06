using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : Items {

    private int m_damage;
    private int m_accuracy;
    private bool m_isThrowable; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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

    public int GetDamage()
    {
        return m_damage; 
    }

    public int GetAccuracy()
    {
        return m_accuracy; 
    }

    public bool GetIsThrowable()
    {
        return m_isThrowable; 
    }
}
