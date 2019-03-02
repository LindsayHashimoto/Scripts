using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public string entityName;
    public int level;
    public int exp;

    public int hp;
    public int atk;
    public int def;
    public int matk;
    public int mdef;
    // Every 4 stats, percentage increases by 1. 
    public int luck;

    public enum status{
        // The default status
        Normal, 
        // Cannot attack, will wake up over time or if attacked
        Asleep, 
        // Takes damage every turn.
        Poisoned, 
        // Attacks have a change to fail. Goes last in the turn order (unless another entity is stunned). 
        Stuned, 
        // Cannot attack, wont wake up until the player leaves the area
        Unconcious
    };
    public status currentStatus; 

    // Determines the turn order
    public int initiative;

    public bool isEnemy;
    public HealthManager healthManager; 

    // Use this for initialization
    void Start () {
        //Test stats:
        level = 1;
        exp = 0;
        hp = 10;
        atk = 10;
        def = 10;
        matk = 10;
        mdef = 10;
        luck = 10;

        healthManager = GetComponent<HealthManager>(); 
        currentStatus = status.Normal;   
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void levelUp()
    {
        level++; 
    }
}
