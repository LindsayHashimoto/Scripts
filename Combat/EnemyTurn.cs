using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class EnemyTurn : MonoBehaviour {
    private Inventory m_enemyInventory; 

    private CombatManager m_combatManager;
    private Stats m_lastTurn;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization.
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  This sets the inital values of m_enemyInvneory and m_combatManager and generates the inventory. 
     * RETURNS
     *  None
     */
    /**/
    void Start ()
    {
        m_enemyInventory = GameObject.Find("Enemy Inventory").GetComponent<Inventory>(); 
        m_combatManager = GetComponentInParent<CombatManager>();
        GenerateInventory(); 
    }
    /*void Start();*/

    /**/
    /*
     * Update()
     * NAME 
     *  Update - Update is called once per frame.
     * SYNOPSIS
     *  void Update()
     * DESCRIPTION
     *  If the game is not paused and the current turn is on an enemy, the enemy will perform an aciton. 
     * RETURNS
     *  None
     */
    /**/
    void Update ()
    {        
        if (Time.timeScale > 0f)
        {
            Stats thisTurn = m_combatManager.GetTurnOrder()[m_combatManager.GetCurrentTurn()];
            //Prevents enemies from taking more than one aciton in a turn due to race conditions
            if (thisTurn != m_lastTurn)
            {
                m_lastTurn = thisTurn;
                if (thisTurn.m_isEnemy)
                {
                    PerformAction();
                }
            }
        }
	}
    /*void Update();*/

    /**/
    /*
     * GenerateInventory()
     * NAME
     *  GenerateInventory - sets up the items the enemy will use in combat
     * SYNOPSIS
     *  void GenerateInventory()
     * DESCRIPTION
     *  This adds 3 items to the enemy's inventory that the enemy will use in combat against the player. 
     * RETURNS
     *  None
     */
    /**/
    private void GenerateInventory()
    {
        m_enemyInventory.AddItems(ItemList.m_claw, 1);
        m_enemyInventory.AddItems(ItemList.m_knife, 1);
        m_enemyInventory.AddItems(ItemList.m_minorPotion, 5);
    }
    /*private void GenerateInventory();*/

    /**/
    /*
     * PerformAction()
     * NAME
     *  PerformAction - how the enemy decides what to do. 
     * SYNOPSIS
     *  void PerformAction()
     * DESCRIPTION
     *  This funtion decides which action the enemy will do on their turn. The enemy will first try to take out a 
     *  player with a basic attack. If the player were to be defeated by the basic attack, the attack will execute. 
     *  Otherwise, the enemy's inventory will be separated into weapons and potions. The weapons list will be sorted 
     *  by their base damage in ascending order. Each of the weapons in order will see if they can take out a player 
     *  in one turn. The attack will execute if an attack will take out a friendly player. If none can be defeated in 
     *  one turn, the enemy will take their turn to heal an enemy with less than 50% health. If none are found
     *  with low health, the enemy will try to deal as much damage as possibly by executing an attack with their 
     *  strongest weapon. 
     * RETURNS 
     *  None
     */ 
    /**/
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
        AttackForDamage(attackTargets, CalculateDamageForEachTarget(attackTargets, weapons.Last()), weapons.Last());
    }
    /*private void PerformAction();*/

    /**/
    /*
     * AttackForKill()
     * NAME
     *  AttackForKill - sees if the enemy can defeat a player with one attack. 
     * SYNOPSIS
     *  bool AttackForKill(Stats [] a_targets, int [] a_damageDone, Weapons a_weapon = null)
     *      a_targets --> the possible targets the enemy can hit.
     *      a_damageDone --> the amount of damage an attack will do that was calculated beforehand.
     *      a_weapon --> the weapon that will be used. If no weapon is set, a basic attack will be used instead. 
     * DESCRIPTION
     *  This function searches through each of the potential targets. If the damage calculated were to cause the 
     *  target to lose all of their health, this funciton will execute that attack using the weapon specifeid by 
     *  a_weapon or a basic attack if no weapon is specified. If an attack was executed, the function will 
     *  return true. If not, false is returned.  
     * RETURNS
     *  True if a target was found that will be defeated by the attack. 
     *  False if the enemy cannot defeat a target in one turn. 
     */
    /**/
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
    /*private bool AttackForKill(Stats [] a_targets, int [] a_damageDone, Weapons a_weapon = null);*/

    /**/
    /*
     * AttackForDamage()
     * NAME
     *  AttackForDamage - the enemy will try to attack the target which will deal the most damage.
     * SYNOPSIS
     *  void AttackForDamage(Stats[] a_targets, int[] a_damageDone, Weapons a_weapon = null)
     *      a_targets --> the targets that the enemy can attack.
     *      a_damageDone --> the amount of damage that will be done to each target.
     *      a_weapon --> the weapon that will be used against the player.
     * DESCRIPTION
     *  This finds the target which will recieve the most damage from an attack and executes that attack. 
     * RETURNS
     *  None
     */
    /**/
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
    /*private void AttackForDamage(Stats[] a_targets, int[] a_damageDone, Weapons a_weapon = null);*/

    /**/
    /*
     * GetAttackTargets()
     * NAME
     *  GetAttackTargets - get the targets the enemy can hit. 
     * SYNOPSIS
     *  Stats[] GetAttackTargets()
     * DESCRIPTION
     *  This finds all entities in the turn order that are not enemies. 
     * RETURNS
     *  All targets the enemy can hit.
     */
    /**/
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
    /*private Stats[] GetAttackTargets();*/
 
    /**/
    /*
     * GetFriendlyTargets()
     * NAME
     *  GetFriendlyTargets - get all the targets that are friendly to the enemy.
     * SYNOPSIS
     *  Stats[] GetFriendlyTargets()
     * DESCRIPTION
     *  Finds all the entities in turn order that are enemies and returns an array of the found entities. 
     * RETURNS
     *  All friendly (to the enemy) targets.
     */
    /**/
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
    /*private Stats[] GetFriendlyTargets();*/

    /**/
    /*
     * CalculateDamageForEachTarget()
     * NAME
     *  CalculateDamageForEachTarget - calculates the damage the enemy will do to each potential target
     * SYNOPSIS
     *  int[] CalculateDamageForEachTarget (Stats[] a_targets, Weapons a_weapon = null)
     *      a_targets --> the targets the enemy can hit.
     *      a_weapon --> the weapon that will be used in calculateion. A basic attack is used if no weapon 
     *      is given. 
     * DESCRIPION
     *  This calculates the damage each target will take with the selected weapon. The base damage is set to 5 
     *  if no weapon is specified. 
     * RETURNS 
     *  The amount of damage that will be done to each target with the current entity using the selected weapon.
     */
    /**/
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
    /*private int[] CalculateDamageForEachTarget (Stats[] a_targets, Weapons a_weapon = null);*/
}
