using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollisions : MonoBehaviour
{
   
    private ThrowWeapon m_throwWeapon;
    private Stats m_player;
    private Stats m_enemy; 

    // Use this for initialization
    void Start()
    {
        m_throwWeapon = GetComponentInParent<ThrowWeapon>();
        m_player = GetComponentInParent<Stats>(); 
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && this.gameObject.activeSelf)
        {
            if (other.gameObject.tag == "Enemy1")
            {
                m_enemy = other.gameObject.GetComponent<Stats>(); 
                other.gameObject.GetComponent<HealthManager>().DealDamage(CalculateThrownDamage());
                m_throwWeapon.WeaponStop();
                m_throwWeapon.ResetWeaponPosition();
                this.gameObject.SetActive(false); 
            }
            if (other.gameObject.tag == "Collision")
            {
                m_throwWeapon.WeaponStop();
                m_throwWeapon.DropWeapon();
                m_throwWeapon.ResetWeaponPosition();
                this.gameObject.SetActive(false);
            }
        }
    }

    private int CalculateThrownDamage()
    {
        int damage = m_throwWeapon.GetInventoryManager().GetRegisteredWeapon().GetDamage();
        damage += m_player.m_atk - m_enemy.m_def;
        if (damage < 1)
            damage = 1;
        return damage;
    }
}

