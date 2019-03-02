using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour {

    private Stats user;
    private Stats target;
    
	public void attack(string attackName, int baseDamage, int accuracy, bool physical)
    {
        displayAttackMessage(attackName);
        int randomNum = Random.Range(0, 101);
        if (randomNum <= accuracy)
        {
            int damageToDo = calculateDamage(baseDamage, physical);
            target.healthManager.dealDamage(damageToDo); 
        }
        else
        {
            //"The attack missed!"
        }
    }
    // an attack that has a change to provide a status condition
    public void attack(string attackName, int baseDamage, int accuracy, bool physical, Stats.status statusToGive, int statusChance)
    {
        displayAttackMessage(attackName);
        int randomNum = Random.Range(0, 101);
        if (randomNum <= accuracy)
        {
            int damageToDo = calculateDamage(baseDamage, physical);
            target.healthManager.dealDamage(damageToDo);
            if(target.currentStatus == Stats.status.Normal)
            {
                randomNum = Random.Range(0, 101);
                if (randomNum <= statusChance)
                {
                    target.currentStatus = statusToGive;
                    displayStatusMessage(statusToGive); 
                }
            }
        }
        else
        {
            //"The attack missed!"
        }
    }

    private void displayAttackMessage(string attackName)
    {
        string infoText = "";
        infoText += user.name + " used " + attackName + " on " + target.name;
        
    }
    private void displayStatusMessage(Stats.status newStatus)
    {
        string statusMessage; 
        switch (newStatus)
        {
            case Stats.status.Asleep:
                statusMessage = target.entityName + " fell asleep."; 
                break;
            case Stats.status.Poisoned:
                statusMessage = target.entityName + " was poisoned.";
                break;
            case Stats.status.Stuned:
                statusMessage = target.entityName + " became stunned.";
                break;
            case Stats.status.Unconcious:
                statusMessage = target.entityName + " fell unconcious.";
                break;
        }
    }
    private int calculateDamage(int baseDamage, bool physical)
    {
        int damageToDo;
        if (physical)
        {
            damageToDo = baseDamage + user.atk - target.def;
        }
        else
        {
            damageToDo = baseDamage + user.matk - target.mdef;
        }
        if (damageToDo < 0)
        {
            damageToDo = 1; 
        }
        return damageToDo; 
    }
    public void heal(string spellName, int amountToHeal)
    {

    }
}
