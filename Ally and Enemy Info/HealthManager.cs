using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthManager : MonoBehaviour {

    public int myMaxHealth;
    public int myCurrentHealth;
    public Slider healthBar;
    public Text healthText;

    public Image fill;  

    private float healthPercentage;

	// Use this for initialization
	void Start () { 
        myCurrentHealth = myMaxHealth;
        updateHealth();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (myCurrentHealth <= 0)
        {
            gameObject.SetActive(false); 
        }
	}
    public void dealDamage(int damage)
    {
        myCurrentHealth -= damage;
        updateHealth();
    }
    public void heal(int amountToHeal)
    {
        myCurrentHealth += amountToHeal;
        if (myCurrentHealth > myMaxHealth)
        {
            myCurrentHealth = myMaxHealth; 
        }
        updateHealth(); 
    }
    public void updateHealth()
    {
        healthBar.maxValue = myMaxHealth;
        healthBar.value = myCurrentHealth;
        healthText.text = "HP: " + myCurrentHealth + " / " + myMaxHealth;
        healthPercentage = (float) myCurrentHealth / myMaxHealth; 
        if (healthPercentage > 0.50)
        {
            //set color to green
            fill.color = new Color (0, 255, 0);
        }
        else if (healthPercentage <= 0.25)
        {
            //set color to red
            fill.color = new Color(255, 0, 0);
        }
        else
        {
            //set color to yellow
            fill.color = new Color(250, 255, 0);
        }
    }
}
