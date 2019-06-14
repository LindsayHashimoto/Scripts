using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
}
