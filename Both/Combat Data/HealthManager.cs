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

    /**/
    /*
     * UpdateHealth()
     * NAME
     *  UpdateHealth - Updates the UI health bar.
     * SYNOPSIS
     *  void UpdateHealth()
     * DESCRIPTION
     *  This fucntion is called whenever the UI health bar element needs to be updated. This makes the member variables:
     *  m_maxHealth and m_currentHealth consistent to what is shown to the user. This also changes the color of the 
     *  health bar to green (>50% of health left), yellow(between 25% and 50%) or red (<25% of health left) depending 
     *  on how hurt the current entity is.  
     * RETURNS
     *  None
     */
    /**/
    public void UpdateHealth()
    {
        m_healthBar.maxValue = m_maxHealth;
        m_healthBar.value = m_currentHealth;
        m_healthText.text = "HP: " + m_currentHealth + " / " + m_maxHealth;
        float healthPercentage = GetHealthPercentage(); 
        if (healthPercentage > 0.50)
        {
            //set color to green
            m_fill.color = new Color(0, 255, 0);
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
    /*void UpdateHealth();*/

    /**/
    /*
     * SetUIHealthBars()
     * NAME
     *  SetUIHealthBars - Sets up the UI elements of the health bars.
     * SYNOPSIS
     *  void SetUIHealthBars()
     * DESCRIPTION
     *  This function searches for the two GameObjects named "Ally Health Bars" and "Enemy Health Bars". 
     *  These two GameObjects have children GameObjects which are the UI health bar elements. This funciton 
     *  finds the health bar element that corresponds to the object that this class is attached to and sets 
     *  the member variables: m_healthBar, m_healthText, and m_fill to be the values under the UI health bars. 
     *  After setting the values, the UpdateHealth function is called.
     * RETURNS
     *  None
    */
    /**/
    public void SetUIHealthBars()
    {
        GameObject [] allyOrEnemyHealthBars = GameObject.FindGameObjectsWithTag("HealthBars");
        foreach (GameObject allyOrEnemyHB in allyOrEnemyHealthBars)
        {
            Slider[] healthBars = allyOrEnemyHB.GetComponentsInChildren<Slider>();
            foreach (Slider healthBarSlider in healthBars)
            {
                GameObject healthBar = healthBarSlider.gameObject;
                if (healthBar.tag == this.tag)
                {
                    m_healthBar = healthBar.GetComponentInChildren<Slider>();
                    m_healthText = healthBar.GetComponentInChildren<Text>();
                    m_fill = healthBar.transform.Find("Fill Area").gameObject.GetComponentInChildren<Image>();
                }
            }
        }
        UpdateHealth();
    }
    /*void SetUIHealthBars();*/

    /**/
    /*
     * DealDamage()
     * NAME
     *  DealDamage - this entitiy takes a specified amount of damage.
     * SYNOPSIS
     *  void DealDamage(int a_damage)
     *      a_damage --> the  amount of damage that  the current entity will recieve.
     * DESCRIPTION
     *  The current entity will take the amount of damage specified by the a_damage value. If this causes
     *  m_currentHealth to drop below zero, the currenty entity will be removed from play. After damage is 
     *  done, UpdateHealth is called. 
     * RETURNS
     *  None
     */
    /**/
    public void DealDamage(int a_damage)
    {
        m_currentHealth -= a_damage;
        if (m_currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        UpdateHealth();
    }
    /*void DealDamage(int a_damage);*/

    /**/
    /*
     * Heal()
     * NAME
     *  Heal - this entity is healed for a set amount.
     * SYNOPSIS
     *  void Heal(int a_amountToHeal)
     *      a_amountToHeal --> the amount of damage the current entity will heal.
     * DESCRIPTION
     *  The currenty entity is healed for the amount specified by a_amountToHeal. If the amount healed were
     *  to cause the current health to be greater than the max health, then the current health is set to be
     *  equal to the max health. If this funciton were to cause the current entity to be healed above 0 health,
     *  the entity is set to be active. UpdateHealth() is called at the end.  
     * RETURNS
     *  None
     */
    /**/
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
    /*void Heal(int a_amountToHeal);*/

    /**/
    /*
     * FullyHeal()
     * NAME
     *  FullyHeal - this entity is fully healed.
     * SYNOPSIS
     *  void FullyHeal()
     * DESCRIPTION
     *  The currenty entity's current health is set to the max health. UpdateHealth() is called at the end. 
     * RETURNS
     *  None
     */
    /**/
    public void FullyHeal()
    {
        m_currentHealth = m_maxHealth;
        gameObject.SetActive(true);
        UpdateHealth();
    }
    /*void FullyHeal();*/

    /**/
    /*
     * GetHealthPercentage()
     * NAME
     *  GetHealthPercentage - calculates the precentage of health this entity has left. 
     * SYNOPSIS
     *  float GetHealthPercentage()
     * DESCRIPTION 
     *  Retuns the amount of health left in the form of a percent in decimal form. 
     * RETURNS
     *  The percentage of health this entity has left.
     */
    /**/
    public float GetHealthPercentage()
    {
        return (float)m_currentHealth / m_maxHealth;
    }
    /*float GetHealthPercentage()*/

    /**/
    /* 
     * SetMaxHealth()
     * NAME
     *  SetMaxHealth - setter method for m_maxHealth.
     * SYNOPSIS
     *  void SetMaxHealth(int a_hp)
     *      a_hp --> the value which m_maxHealth will be assigned to.
     * DESCRIPTION
     *  This allows the value of m_maxHealth to be changed to the value set by a_hp. 
     * RETURNS
     *  None
     */
    /**/
    public void SetMaxHealth(int a_hp)
    {
        m_maxHealth = a_hp; 
    }
    /*void SetMaxHealth(int a_hp);*/

    /**/
    /*
     * SetCurrentHealth()
     * NAME
     *  SetCurrentHealth - setter for m_currentHealth.
     * SYNOPSIS
     *  void SetCurrentHealth(int a_hp)
     *      a_hp --> the value that will be assigned to m_currentHealth.
     * DESCRIPTION
     *  This allows the private memeber value m_currentHealth to be changed to the value of a_hp. 
     * RETURNS
     *  None
     */
    /**/
    public void SetCurrentHealth(int a_hp)
    {
        m_currentHealth = a_hp; 
    }
    /*void SetCurrentHealth(int a_hp);*/

    /**/
    /*
     * GetMaxHealth()
     * NAME
     *  GetMaxHealth - accessor for the member value m_maxHealth.
     * SYNOPSIS
     *  int GetMaxHealth()
     * DESCRIPTION
     *  Allows other classes to access the private member value m_maxHealth. 
     * RETURNS
     *  m_maxHealth
     */
    /**/
    public int GetMaxHealth()
    {
        return m_maxHealth; 
    }
    /*int GetMaxHealth();*/

    /**/
    /*
     * GetCurrentHealth()
     * NAME
     *  GetCurrentHealth - accessor for the private member variable. 
     * SYNOPSIS
     *  int GetCurrentHealth()
     * DESCRIPTION
     *  Allows other classes to access the value of the private member vairable m_currentHealth. 
     * RETURNS
     *  m_currentHealth
     */
    /**/
    public int GetCurrentHealth()
    {
        return m_currentHealth; 
    }
    /*int GetCurrentHealth();*/ 
}
