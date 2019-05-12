using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

/**/

public class Stats : MonoBehaviour {

    public string m_entityName;
    public int m_level;
    public int m_exp;
    public int m_atk;
    public int m_def;
    // Determines the turn order
    public int m_initiative;
    public bool m_isEnemy;

    private HealthManager m_healthManager;
    private CombatManager m_combatManager;

    private Light m_highlight; 

    private bool m_currentTurn = false;

    // Use this for initialization
    void Start()
    {
        m_combatManager = FindObjectOfType<CombatManager>();
        m_healthManager = GetComponent<HealthManager>();
        m_highlight = GetComponent<Light>();
        
    }

    private void Update()
    {

    }

    public void levelUp()
    {
        m_level++; 
    }

    public void GenerateInitiative()
    {
        m_initiative = Random.Range(1, 101); 
    }

    public void OnCurrentTurn()
    {
        m_highlight.color = new Color(0, 255, 0);
        m_highlight.enabled = true;
        m_currentTurn = true; 
    }

    public void NoLongerTurn()
    {
        m_highlight.color = new Color(255, 255, 0);
        m_highlight.enabled = false;
        m_currentTurn = false; 
    }
    private void OnMouseOver()
    {
        if (!m_currentTurn)
        {
            m_highlight.enabled = true;
        }  
    }

    private void OnMouseExit()
    {
        if (!m_currentTurn)
        {
            m_highlight.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        m_combatManager.GetTargetFromUser(this);
    }

    public HealthManager GetHealthManager()
    {
        return m_healthManager;
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