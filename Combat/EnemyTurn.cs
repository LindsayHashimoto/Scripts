using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class EnemyTurn : MonoBehaviour {
    public Inventory m_enemyInventory; 

    private CombatManager m_combatManager;
    private Stats m_lastTurn; 

	// Use this for initialization
	void Start ()
    {
        m_combatManager = GetComponentInParent<CombatManager>();
        GenerateInventory(); 
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Prevents enemies from taking more than one aciton in a turn due to race conditions
        Stats thisTurn = m_combatManager.GetTurnOrder()[m_combatManager.GetCurrentTurn()]; 
        if (thisTurn != m_lastTurn)
        {
            m_lastTurn = thisTurn;
            if (thisTurn.m_isEnemy)
            {
                PerformAction();
            }
        }
	}

    private void GenerateInventory()
    {
        m_enemyInventory.AddItems(ItemList.m_claw, 1);
        m_enemyInventory.AddItems(ItemList.m_knife, 1);
        m_enemyInventory.AddItems(ItemList.m_minorPotion, 5);
    }

    private void PerformAction()
    {
        Stats[] attackTargets = GetAttackTargets();
        Stats[] friendlyTargets = GetFriendlyTargets(); 
        int[] basicDamageDone = CalculateDamageForEachTarget(attackTargets);
        List<Weapons> weapons = new List<Weapons>();
        List<Potions> potions = new List<Potions>(); 
 
        // What should the enemy do? 
        // Take out a player if possible
        if (AttackForKill(attackTargets, basicDamageDone)) return;

        foreach (Items item in m_enemyInventory.GetInventory())
        {
            if (item.GetIsWeapon())
            {
                weapons.Add((Weapons)item);
            }
            if (item.GetIsPotion())
            {
                potions.Add((Potions)item); 
            }
        }
        //https://stackoverflow.com/questions/3309188/how-to-sort-a-listt-by-a-property-in-the-object
         weapons = weapons.OrderBy(o => o.GetDamage()).ToList<Weapons>(); 

        foreach (Weapons weapon in weapons)
        {
            if (AttackForKill(attackTargets, CalculateDamageForEachTarget(attackTargets, weapon), weapon)) return;
        }

        // Heal himself or an ally with low health if possible 
        foreach (Stats friend in friendlyTargets)
        {
            if(friend.GetHealthManager().GetHealthPercentage() <= 0.5)
            {
                m_combatManager.UsePotion(friend, potions[0]);
                return;  
            }
        }
        // Deal the most damage
        AttackForDamage(attackTargets, CalculateDamageForEachTarget(attackTargets, weapons[0]), weapons[0]);
        //AttackForDamage(attackTargets, basicDamageDone);
    }

    private bool AttackForKill(Stats [] a_targets, int [] a_damageDone, Weapons a_weapon = null)
    {
        for (int i = 0; i < a_targets.Length; i++)
        {
            // If the attack will kill the current target
            if (a_targets[i].GetHealthManager().m_currentHealth <= a_damageDone[i])
            {
                if (a_weapon == null)
                {
                    m_combatManager.BasicAttack(a_targets[i]);
                }
                else
                {
                    m_combatManager.UseWeapon(a_targets[i], a_weapon); 
                }
                return true; 
            }
        }
        return false; 
    }

    private void AttackForDamage(Stats[] a_targets, int[] a_damageDone, Weapons a_weapon = null)
    {
        int highest = 0;
        int highestCount = -1;
        for (int i = 0; i < a_targets.Length; i++)
        {
            if (a_damageDone[i] > highest)
            {
                highest = a_damageDone[i];
                highestCount = i;
            }
        }
        if (a_weapon == null)
        {
            m_combatManager.BasicAttack(a_targets[highestCount]);
        }
        else
        {
            m_combatManager.UseWeapon(a_targets[highestCount], a_weapon); 
        }
        
    }

    // Get the targets the Enemy can hit 
    private Stats[] GetAttackTargets()
    {
        Stats[] maxTargets = new Stats[4];
        int counter = 0; 
        foreach (Stats entity in m_combatManager.GetTurnOrder())
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

    // Get the friendly targets
    private Stats[] GetFriendlyTargets()
    {
        Stats[] maxTargets = new Stats[4];
        int counter = 0;
        foreach (Stats entity in m_combatManager.GetTurnOrder())
        {
            if (entity.m_isEnemy && entity.GetHealthManager().m_currentHealth > 0)
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


    private int[] CalculateDamageForEachTarget (Stats[] a_targets, Weapons a_weapon = null)
    {
        int power; 
        if (a_weapon == null)
        {
            power = 5; 
        }
        else
        {
            power = a_weapon.GetDamage(); 
        }
        int[] damageDone = new int[4];
        for (int i = 0; i < a_targets.Length; i++)
        {
            damageDone[i] = m_combatManager.CalculateDamage(m_combatManager.GetTurnOrder()[m_combatManager.GetCurrentTurn()], a_targets[i], power);
        }
        return damageDone;
    }
}
