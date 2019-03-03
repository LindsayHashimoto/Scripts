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
    public int m_initiative;

    public bool m_isEnemy;
    public HealthManager m_healthManager; 

    void Stats(string a_name)
    {

    }

    public void levelUp()
    {
        m_level++; 
    }

    public void generateInitiative()
    {
        m_initiative = Random.Range(1, 100); 
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