using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**/

public class Stats : MonoBehaviour {

    public string m_entityName;
    public int m_level;
    public int m_exp;

    public int m_hp;
    public int m_atk;
    public int m_def;

    // Determines the turn order
    public int initiative;

    public bool isEnemy;
    public HealthManager healthManager; 

    void Stats(string a_name)
    {

    }

    // Use this for initialization
    void Start ()
    { 

        //healthManager = GetComponent<HealthManager>();
	}

    public void levelUp()
    {
        level++; 
    }

    public void generateInitiative()
    {
        initiative = Random.Range(1, 100); 
    }
}

/*
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
    */
// public status currentStatus; 