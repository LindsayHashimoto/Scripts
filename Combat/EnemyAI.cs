using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public CombatManager m_combatManager; 
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_combatManager.m_turnOrder[m_combatManager.m_currentTurn].m_isEnemy)
        {
            PerformAction(); 
        }
	}

    private void PerformAction()
    {
        Stats[] attackTargets = GetAttackTargets();
        int[] damageDone = CalculateBasicDamage(attackTargets);
        // What should the enemy do? 

        // Take out a player if possible
        if (AttackForKill(attackTargets, damageDone, 100)) return;
        // Heal himself or an ally with low health if possible 
        // Deal the most damage
        AttackForDamage(attackTargets, damageDone, 100);
    }

    private bool AttackForKill(Stats [] a_targets, int [] a_damageDone, int a_accuracy)
    {
        for (int i = 0; i < a_targets.Length; i++)
        {
            // If the attack will kill the current target
            if (a_targets[i].GetHealthManager().m_currentHealth <= a_damageDone[i])
            {
                ExecuteAttack(a_targets[i], a_damageDone[i], a_accuracy);
                return true; 
            }
        }
        return false; 
    }

    private void AttackForDamage(Stats[] a_targets, int[] a_damageDone, int a_accuracy)
    {
        int highest = 0;
        int highestCount = -1; 
        for (int i = 0; i < a_damageDone.Length; i++)
        {
            if (a_damageDone[i] > highest)
            {
                highest = a_damageDone[i];
                highestCount = i; 
            }
        }
        ExecuteAttack(a_targets[highestCount], a_damageDone[highestCount], a_accuracy); 
    }

    private void ExecuteAttack(Stats a_target, int a_damageToDo, int a_accuracy)
    {
        if (Random.Range(1, 101) <= a_accuracy)
        {
            a_target.GetHealthManager().DealDamage(a_damageToDo); 
        }
        m_combatManager.NextTurn(); 
    }

    // Get the targets the Enemy can hit 
    private Stats[] GetAttackTargets()
    {
        Stats[] maxTargets = new Stats[4];
        int counter = 0; 
        foreach (Stats entity in m_combatManager.m_turnOrder)
        {
            if (!entity.m_isEnemy && entity.GetHealthManager().m_currentHealth > 0)
            {
                maxTargets[counter] = entity;
                counter++; 
            }
        }
        Stats[] targets = new Stats[counter]; 
        for (int i = 0; i < counter; i++)
        {
            targets[i] = maxTargets[i]; 
        }
        return targets;  
    }

    private int[] CalculateBasicDamage(Stats[] a_targets)
    {
        int[] damageDone = new int[4];
        for (int i = 0; i < a_targets.Length; i++)
        {
            damageDone[i] = m_combatManager.CalculateDamage(m_combatManager.m_turnOrder[m_combatManager.m_currentTurn], a_targets[i], 5);
        }
        return damageDone;
    }
}
