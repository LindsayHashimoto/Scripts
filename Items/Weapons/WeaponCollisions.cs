/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollisions : MonoBehaviour
{
   
    public ThrowWeapon throwWeapon;
    private ThrownWeaponData thrownWeaponData; 

    // Use this for initialization
    void Start()
    {
        thrownWeaponData = FindObjectOfType<ThrownWeaponData>(); 
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<HealthManager>().DealDamage(1);
                throwWeapon.weaponStop(thrownWeaponData);
                throwWeapon.resetWeaponPosition(thrownWeaponData); 
            }
            if (other.gameObject.tag == "Collision")
            {
                throwWeapon.weaponStop(thrownWeaponData);
                throwWeapon.dropWeapon(thrownWeaponData);
                throwWeapon.resetWeaponPosition(thrownWeaponData); 
            }
        }
    }
}
*/
