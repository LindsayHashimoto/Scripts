using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollisions : MonoBehaviour
{
   
    private ThrowWeapon m_throwWeapon;
    private Stats m_player;
    private Stats m_enemy;
    private Weapons m_registeredWeapon; 

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  Sets the inital values of the member variables.
     * RETURNS
     *  None
     */
    /**/
    void Start()
    {
        m_throwWeapon = GetComponentInParent<ThrowWeapon>();
        m_player = GetComponentInParent<Stats>(); 
    }
    /*void Start();*/

    /**/
    /*
     * OnTriggerEnter2D()
     * NAME
     *  OnTriggerEnter2D - is called when an entity enters the trigger.
     * SYNOPSIS
     *  void OnTriggerEnter2D(Collider2D a_other)
     *      a_other --> the object that enters this trigger. 
     * DESCRIPTION
     *  If this item hits the enemy, the enemy takes damage and the weapon stops and is moved back to its original position where 
     *  it is no longer active. If the weapon hits a collision, the weapon stops and a dropped weapon object is placed at the location
     *  where the weapon stopped where it can be picked up by the player. The weapon's position is also reset and is set to be not 
     *  active. 
     * RETURNS
     *  None
     */
    /**/
    void OnTriggerEnter2D(Collider2D a_other)
    {
        if (!a_other.isTrigger && this.gameObject.activeSelf)
        {
            if (a_other.gameObject.tag == "Enemy1")
            {
                m_enemy = a_other.gameObject.GetComponent<Stats>(); 
                m_throwWeapon.WeaponStop();
                m_throwWeapon.ResetWeaponPosition();
                this.gameObject.SetActive(false);
                a_other.gameObject.GetComponent<HealthManager>().DealDamage(CalculateThrownDamage());
            }
            if (a_other.gameObject.tag == "Collision")
            {
                m_throwWeapon.WeaponStop();
                m_throwWeapon.DropWeapon();
                m_throwWeapon.ResetWeaponPosition();
                this.gameObject.SetActive(false);
            }
        }
    }
    /*void OnTriggerEnter2D(Collider2D a_other);*/

    /**/
    /*
     * CalculateThrownDamage()
     * NAME
     *  CalculateThrownDamage - calculates the damage the enemy will take from the thrown weapon. 
     * SYNOPSIS
     *  int CalculateThrownDamage()
     * DESCRIPTION
     *  This calculates the damage the weapon will do based on the base damage of the item, the attack of the player and the defense
     *  of the enemy. If the damage ends up being less than 1, the damage is set to 1. 
     * RETURNS
     *  The amount of damage the enemy will take. 
     */
    /**/
    private int CalculateThrownDamage()
    {
        int damage = m_throwWeapon.GetThrownWeapon().GetDamage();
        damage += m_player.m_atk - m_enemy.m_def;
        if (damage < 1)
            damage = 1;
        return damage;
    }
    /*private int CalculateThrownDamage();*/
}

