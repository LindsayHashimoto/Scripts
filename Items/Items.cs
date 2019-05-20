using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

    protected int m_durability;
    protected string m_name;
    protected bool m_isWeapon = false;
    protected bool m_isPotion = false; 
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void RemoveDurability()
    {
        m_durability--; 
    }

    public string GetName()
    {
        return m_name; 
    }

    public int GetDurability()
    {
        return m_durability;
    }

    public bool GetIsWeapon()
    {
        return m_isWeapon; 
    }

    public bool GetIsPotion()
    {
        return m_isPotion; 
    }
}
