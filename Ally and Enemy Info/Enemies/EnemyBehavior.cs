using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public Transform m_player;

    public float m_runSpeed;

    private bool m_canMove; 
    private bool m_chasingPlayer;
    private bool m_isMoving; 

    private Animator m_anim;
    private Vector3 m_displacement;
    
    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization 
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  Sets the inital value of m_anim and m_canMove. 
     * RETURNS
     *  None
     */
    /**/
    void Start ()
    {
        m_anim = GetComponent<Animator>();
        m_canMove = true; 
	}
    /*void Start();*/

    /**/
    /*
     * Update()
     * NAME 
     *  Update - Update is called once per frame
     * SYNOPSIS
     *  void Update()
     * DESCRIPTION
     *  If the enemy can move and is chasing the player, the enemy will run towards the player. The enemy will also have walking 
     *  animations when moving towards the player. 
     * RETURNS
     *  None
     */
    /**/
    void Update () {
        m_isMoving = false; 
        if (m_canMove)
        {
            if (m_chasingPlayer)
            {
                m_displacement = m_player.position - transform.position;
                m_displacement = m_displacement.normalized;

                if (Vector2.Distance(m_player.position, transform.position) > 1.0f)
                {
                    transform.position += (m_displacement * m_runSpeed * Time.deltaTime);
                    m_isMoving = true; 
                }

            }
        
            m_anim.SetFloat("MoveX", m_displacement.x);
            m_anim.SetFloat("MoveY", m_displacement.y);
            m_anim.SetBool("PlayerMoving", m_isMoving);
            m_anim.SetFloat("LastMoveX", m_displacement.x);
            m_anim.SetFloat("LastMoveY", m_displacement.y);
        }

    }
    /*void Update();*/

    /**/
    /*
     * OnTriggerEnter2D
     * NAME
     *  OnTriggerEnter2D - when the player's weapon enters this collider, this chases the player. 
     * SYNOPIS
     *  void OnTriggerEnter2D(Collider2D a_other)
     *      a_other --> an object that enters this BoxCollider2D. 
     * DESCRIPTION
     *  When a weapon thrown by the player enters this box collider, this entity should chase the player and initiate combat. 
     * RETURN
     *  None
     */
    /**/
    void OnTriggerEnter2D(Collider2D a_other)
    {
        if(a_other.gameObject.tag == "Player's Weapon")
        {
            m_chasingPlayer = true; 
        }
    }
    /*void OnTriggerEnter2D(Collider2D other);*/

    /**/
    /*
     * void SetCanMove(bool a_canMove)
     * NAME 
     *  SetCanMove - sets the value for the member value: m_canMove 
     * SYNOPSIS 
     *  void SetCanMove(bool a_canMove)
     *      a_canMove --> the value that m_canMove will be set to
     * DESCRIPTION
     *  Sets the value for m_canMove. 
     * RETURNS
     *  None
    */
    public void SetCanMove(bool a_canMove)
    {
        m_canMove = a_canMove; 
    }
    /*public void SetCanMove(bool a_canMove);*/

    /**/
    /*
     * GetAnim()
     * NAME
     *  GetAnim - accessor for m_anim 
     * SYNOPSIS
     *  Animator GetAnim()
     * DESCRIPTION
     *  Returns the animator for the enemy. 
     * RETURNS
     *  m_anim
     */
    /**/
    public Animator GetAnim()
    {
        return m_anim; 
    }
    /*public Animator GetAnim()*/
}
