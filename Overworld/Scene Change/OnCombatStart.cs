using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class OnCombatStart : MonoBehaviour {

    private PlayerStartPoint m_startPoint;
    private GameObject m_combatData;
    private EnemyBehavior m_enemyBehavior;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization.
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  This sets the values of the above member values and sets m_combatData to be not active. 
     * RETURNS
     *  None
     */
    /**/
    void Start ()
    {
        m_enemyBehavior = FindObjectOfType<EnemyBehavior>(); 
        m_startPoint = FindObjectOfType<PlayerStartPoint>();
        m_combatData = GameObject.FindGameObjectWithTag("EnemyCombat");
        m_combatData.SetActive(false); 
	}
    /*void Start();*/

    /**/
    /*
     * OnTriggerEnter2D()
     * NAME
     *  OnTriggerEnter2D - sets up combat when player enters the enemy's sight box. 
     * SYNOPSIS
     *  void OnTriggerEnter2D(Collider2D a_other)
     *      a_other --> the object that entered the line of sight.
     * DESCRIPTION
     *  When the player enters the enemy's line of sight, the enemy is set to not be able to move. The position 
     *  where the player will re-enter the current scene will be moved to the current position. Then, the 
     *  combat scene starts. 
     * RETURNS 
     *  None
     */
    /**/
    void OnTriggerEnter2D(Collider2D a_other)
    {
        if (a_other.gameObject.name == "Player")
        {
            m_enemyBehavior.SetCanMove(false); 
            m_startPoint = FindObjectOfType<PlayerStartPoint>();
            m_startPoint.transform.position = transform.position;
            m_combatData.SetActive(true); 
            SceneManager.LoadScene("Combat");
        }
    }
    /*void OnTriggerEnter2D(Collider2D other);*/

}
