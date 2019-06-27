using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour {

    /**/
    /*
     * OnTriggerEnter2D()
     * NAME
     *  OnTriggerEnter2D - heals the player and their allies when they enter the box collider.
     * SYNOPSIS
     *  void OnTriggerEnter2D(Collider2D a_other)
     * DESCRIPTION
     *  This funciton finds all allies and heals them fully when they enter the box collider. 
     * RETURNS
     *  None
     */
    /**/
    private void OnTriggerEnter2D(Collider2D a_other)
    {
        if (a_other.tag == "Player")
        {
            SceneManagerScript sm = a_other.GetComponentInParent<SceneManagerScript>();
            GameObject allies = sm.transform.Find("Allies").gameObject;
            HealthManager[] allyHealth = allies.GetComponentsInChildren<HealthManager>();
            
            foreach (HealthManager personToHeal in allyHealth)
            {
                personToHeal.FullyHeal(); 
            }
        }
    }
    /*private void OnTriggerEnter2D(Collider2D a_other);*/
}
