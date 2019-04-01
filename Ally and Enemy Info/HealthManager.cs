using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthManager : MonoBehaviour {

    private int m_maxHealth;
    public int m_currentHealth;
    public Slider m_healthBar;
    public Text m_healthText;

    public Image m_fill;  

	// Use this for initialization
	void Start () { 
        m_currentHealth = m_maxHealth;
        UpdateHealth();
	}
	
	// Update is called once per frame
	void Update () {
        
        // Remove the current entity when they have been defeated
        if (m_currentHealth <= 0)
        {
            gameObject.SetActive(false); 
        }
	}
    public void DealDamage(int a_damage)
    {
        m_currentHealth -= a_damage;
        UpdateHealth();
    }
    public void Heal(int a_amountToHeal)
    {
        m_currentHealth += a_amountToHeal;
        // Cannot have more than the set max health. 
        if (m_currentHealth > m_maxHealth)
        {
            m_currentHealth = m_maxHealth; 
        }
        UpdateHealth(); 
    }
    // Updates the UI health bar.  
    public void UpdateHealth()
    {
        m_healthBar.maxValue = m_maxHealth;
        m_healthBar.value = m_currentHealth;
        m_healthText.text = "HP: " + m_currentHealth + " / " + m_maxHealth;
        float healthPercentage = (float) m_currentHealth / m_maxHealth; 
        if (healthPercentage > 0.50)
        {
            //set color to green
            m_fill.color = new Color (0, 255, 0);
        }
        else if (healthPercentage <= 0.25)
        {
            //set color to red
            m_fill.color = new Color(255, 0, 0);
        }
        else
        {
            //set color to yellow
            m_fill.color = new Color(250, 255, 0);
        }
    }
    public void SetMaxHealth(int a_hp)
    {
        m_maxHealth = a_hp; 
    }
}
