using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthManager : MonoBehaviour {

    public int m_maxHealth;
    public int m_currentHealth;

    private GameObject m_hpObj; 
    private Slider m_healthBar;
    private Text m_healthText;
    private Image m_fill;  

	// Use this for initialization
	void Start ()
    {
        m_hpObj = GameObject.FindGameObjectsWithTag(this.tag)[0];
        if (m_hpObj == this.gameObject)
        {
            m_hpObj = GameObject.FindGameObjectsWithTag(this.tag)[1];
        }
        m_healthBar = m_hpObj.GetComponentInChildren<Slider>();
        m_healthText = m_hpObj.GetComponentInChildren<Text>();
        m_fill = m_hpObj.GetComponentsInChildren<Image>()[1];
        UpdateHealth();
	}
	
	// Update is called once per frame
	void Update () {
        
        // Remove the current entity when they have been defeated
       
        
        //UpdateHealth();
    }
    public void DealDamage(int a_damage)
    {
        m_currentHealth -= a_damage;
        if (m_currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
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
        if(m_currentHealth > 0)
        {
            gameObject.SetActive(true);
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

    public void SetCurrentHealth(int a_hp)
    {
        m_currentHealth = a_hp; 
    }

    public int GetMaxHealth()
    {
        return m_maxHealth; 
    }

    public int GetCurrentHealth()
    {
        return m_currentHealth; 
    }

    public float GetHealthPercentage()
    {
        return (float) m_currentHealth / m_maxHealth;
    }
}
