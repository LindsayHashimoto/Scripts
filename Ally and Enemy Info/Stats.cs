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
    public int m_baseExp; 

    private HealthManager m_healthManager;
    private PlayerTurn m_playerTurn;

    private Light m_highlight = new Light(); 

    private bool m_currentTurn = false;

    // Use this for initialization
    void Start()
    {
        m_playerTurn = FindObjectOfType<PlayerTurn>();
        m_healthManager = this.gameObject.GetComponent<HealthManager>();
        m_highlight = this.gameObject.GetComponent<Light>();
        
    }

    private void Update()
    {

    }

    public void LevelUp(ExplinationText a_exTxt = null)
    {
        
        m_level++;
        m_healthManager.SetMaxHealth(m_healthManager.GetMaxHealth() + 1);
        m_healthManager.Heal(1); 
        m_atk++;
        m_def++;
        if (a_exTxt != null)
        {
            a_exTxt.SetMessage(m_entityName + " grew to level " + m_level + "! \n" + "HP increased by 1. \n" + "Attack increased by 1. \n" + "Defense increased by 1.\n");
        } 
    }

    public void GainExp(int a_exp, ExplinationText a_exTxt = null)
    {
        m_exp += a_exp; 
        while (m_exp >= 100)
        {
            LevelUp(a_exTxt);
            m_exp -= 100; 
        }
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
        m_playerTurn = FindObjectOfType<PlayerTurn>();
        m_playerTurn.GetTargetFromUser(this);
    }

    public HealthManager GetHealthManager()
    {
        return m_healthManager;
    }

    public int GetBaseExp()
    {
        return m_baseExp; 
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