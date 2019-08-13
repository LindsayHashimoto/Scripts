using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

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

    private Light m_highlight; 

    private bool m_currentTurn;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  This sets the inital values for member variables and sets up the interface for the health bars. 
     * RETURNS
     *  None
     */
    /**/
    void Start()
    {
        m_playerTurn = FindObjectOfType<PlayerTurn>();
        m_healthManager = this.gameObject.GetComponent<HealthManager>();
        m_highlight = new Light();
        m_highlight = this.gameObject.GetComponent<Light>();
        m_currentTurn = false;
        if (!m_isEnemy)
        {
            m_healthManager.SetUIHealthBars();
        }

    }
    /*void Start();*/

    /**/
    /*
     * LevelUp()
     * NAME
     *  LevelUp - makes the player stronger
     * SYNOPSIS
     *  void LevelUp(ExplinationText a_exTxt = null)
     *      a_exTxt --> the class that allows the program to send messages to the user
     * DESCIPTION
     *  When the player gets 100 experience points, they will level up and this will be called. This function 
     *  makes the player slightly more powerful by increasing thier level, max health, current health, attack 
     *  and defense by 1. If a_exTxt is provided by the caller, this function will tell the user that the character
     *  leveled up and their stats increased. 
     * RETURNS 
     *  None
     */
    /**/
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
    /*public void LevelUp(ExplinationText a_exTxt = null);*/

    /**/
    /*
     * GainExp()
     * NAME 
     *  GainExp - player gains experience by a specified amount
     * SYNOPSIS
     *  void GainExp(int a_exp, ExplinationText a_exTxt = null)
     *      a_exp --> the amount of experience that will be added
     *      a_exTxt --> the class that sends messages to the user
     * DESCRIPTION
     *  The player gains experience specified by a_exp. If this were to cause the player to gain
     *  more than 100 experience, the player will level up and the experience number is reduced 
     *  by 100. a_exTxt is passed on to the LevelUp funciton to send messages to the user to tell
     *  them that they leveled up. 
     * RETURNS
     *  None
     */
    /**/
    public void GainExp(int a_exp, ExplinationText a_exTxt = null)
    {
        m_exp += a_exp; 
        while (m_exp >= 100)
        {
            LevelUp(a_exTxt);
            m_exp -= 100; 
        }
    }
    /*public void GainExp(int a_exp, ExplinationText a_exTxt = null);*/

    /**/
    /*
     * GenerateInitiative()
     * NAME
     *  GenerateInititative - generate a number that will determine the turn order in combat.
     * SYNOPSIS
     *  void GenerateInitiative()
     * DESCRIPTION
     *  m_initiative is assigned a value between 1 and 100. Entities with higher numbers go before those
     *  with lower numbers in the turn order. 
     * RETURNS
     *  None. 
     */
    /**/
    public void GenerateInitiative()
    {
        m_initiative = Random.Range(1, 101); 
    }
    /*public void GenerateInitiative();*/

    /**/
    /*
     * OnCurrentTurn()
     * NAME
     *  OnCurrentTurn - makes the entity glow green if it is currently their turn
     * SYNOPSIS
     *  void OnCurrentTurn()
     * DESCRIPTION
     *  This is called when the current entity's turn starts. This fuction makes the current
     *  entity glow green and sets m_currentTurn to be true. 
     * RETURNS 
     *  None
     */
    /**/
    public void OnCurrentTurn()
    {
        m_highlight.color = new Color(0, 255, 0);
        m_highlight.enabled = true;
        m_currentTurn = true; 
    }
    /*public void OnCurrentTurn();*/

    /**/
    /*
     * NoLongerTurn()
     * NAME
     *  NoLongerTurn - performs action when it is no longer the current entity's turn
     * SYNOPSIS
     *  void NoLongerTurn()
     * DESCRIPTION
     *  This funciton is called when the current entity's turn ends. This makes the entity no longer light up
     *  constantly and have its light color set to yellow instead of green. m_currentTurn is also set to be false. 
     * RETURNS 
     *  None. 
     */
    /**/
    public void NoLongerTurn()
    {
        m_highlight.color = new Color(255, 255, 0);
        m_highlight.enabled = false;
        m_currentTurn = false; 
    }
    /*public void NoLongerTurn();*/

    /**/
    /*
     * OnMouseOver()
     * NAME
     *  OnMouseOver - makes the current entity light up when the user hovers over it
     * SYNOPSIS
     *  void OnMouseOver()
     * DESCRIPTION
     *  This is called whenever the user hovers thier mouse over the current entity. When they do this, the 
     *  current entity's yellow highlight becomes visible.  
     * RETURNS
     *  None
     */
    /**/
    private void OnMouseOver()
    {
        if (!m_currentTurn)
        {
            m_highlight.enabled = true;
        }  
    }
    /*private void OnMouseOver();*/

    /**/
    /*
     * OnMouseExit()
     * NAME
     *  OnMouseExit - performs action when the user's mouse exits an entity
     * SYNOPSIS
     *  void OnMouseExit()
     * DESCRIPTION
     *  This funtion is called whenever the user's mouse moves away from the current entity. This function makes
     *  the highlight around the entity dissappear. 
     * RETURNS
     *  None
     */
    /**/
    private void OnMouseExit()
    {
        if (!m_currentTurn)
        {
            m_highlight.enabled = false;
        }
    }
    /*private void OnMouseExit();*/

    /**/
    /*
     * OnMouseDown()
     * NAME
     *  OnMouseDown - performs action when the user clicks on the entity
     * SYNOPSIS
     *  void OnMouseDown()
     * DESCRIPTION
     *  This function is called each time the user clicks on an entity. This function sends the target 
     *  the user clicked to the GetTargetFromUser funtion in the PlayerTurn class. This data will be used
     *  to have the player perform a specified action on the selected target. 
     * RETURNS 
     *  None
     */
    /**/
    private void OnMouseDown()
    {
        m_playerTurn = FindObjectOfType<PlayerTurn>();
        m_playerTurn.GetTargetFromUser(this);
    }
    /*private void OnMouseDown();*/

    /**/
    /*
     * GetHealthManager()
     * NAME
     *  GetHealthManager - accessor for m_healthManager
     * SYNOPSIS
     *  HealthManager GetHealthManager()
     * DESCRIPTION
     *  Returns the health manager class for this entity. 
     * RETURNS
     *  m_healthManager
     */
    /**/
    public HealthManager GetHealthManager()
    {
        return m_healthManager;
    }
    /*public HealthManager GetHealthManager();*/

    /**/
    /*
     * GetBaseExp()
     * NAME
     *  GetBaseExp - accessor for m_baseExp. 
     * SYNOPSIS
     *  int GetBaseExp()
     * DESCRIPTION
     *  Returns the base experience this entity will give. 
     * RETURNS
     *  m_baseExp
     */
    /**/
    public int GetBaseExp()
    {
        return m_baseExp; 
    }
    /* public int GetBaseExp();*/
}