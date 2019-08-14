using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour {
    public Transform m_target;

    private float m_moveSpeed;
    private float m_currentSpeed;
    private float m_sprintModifier;

    private Animator m_anim;

    private float m_moveX;
    private float m_moveY; 
    private Vector2 m_lastMove;
    private bool m_followerMoving;
    private bool m_canMove; 

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization.
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  This sets the initial values for m_moveSpeed, m_sprintModifier, m_canMove, m_currentSpeed and m_anim. 
     * RETURNS
     *  None
     */
    /**/
    void Start()
    {
        m_moveSpeed = 7;
        m_sprintModifier = 2; 
        m_canMove = true;
        m_currentSpeed = m_moveSpeed; 
        m_anim = GetComponent<Animator>();
    }
    /*void Start();*/

    /**/
    /*
     * Update()
     * NAME 
     *  Update - Update is called once per frame.
     * SYNOPSIS
     *  void Update()
     * DESCRIPTION
     *  This function makes the follower follow the selected target. If the follower is set to be able to move, 
     *  the distance the follower is from the target is calculated and the follower moves that distance towards 
     *  the target at a speed of m_currentSpeed. The follower also displays walking or standing animations depending 
     *  on which direction the follower is facing and whether or not they are currently moving.
     * SOURCE 
     *  https://answers.unity.com/questions/607100/how-to-make-an-ai-to-follow-the-player-in-2d-c.html
     * RETURNS
     *  None
     */
    /**/
    void Update()
    {
        m_followerMoving = false;
        Vector3 displacement = m_target.position - transform.position;
        displacement = displacement.normalized;
        if (m_canMove)
        {
            if (Vector2.Distance(m_target.position, transform.position) > 1.0f)
            {
                transform.position += (displacement * m_currentSpeed * Time.deltaTime);
                m_followerMoving = true;
            }
        
            m_anim.SetFloat("MoveX", displacement.x);
            m_anim.SetFloat("MoveY", displacement.y);
            m_anim.SetBool("PlayerMoving", m_followerMoving);
            m_anim.SetFloat("LastMoveX", displacement.x);
            m_anim.SetFloat("LastMoveY", displacement.y);
        }

    }
    /*void Update();*/

    /**/
    /*
     * GetAnim()
     * NAME
     *  GetAnim - accessor for the animator object. 
     * SYNOPSIS
     *  Animator GetAnim()
     * DESCRIPTION
     *  This returns the animator value of the follower. 
     * RETURNS
     *  m_anim
     */
    /**/
    public Animator GetAnim()
    {
        return m_anim;
    }
    /*public Animator GetAnim();*/

    /**/
    /*
     * GetCanMove()
     * NAME
     *  GetCanMove - accessor for m_canMove
     * SYNOPSIS
     *  bool GetCanMove()
     * DESCRIPTION
     *  This lets other classes know the value of m_canMove. 
     * RETURNS
     *  m_canMove
     */
    /**/
    public bool GetCanMove()
    {
        return m_canMove;
    }
    /*public bool GetCanMove();*/

    /**/
    /*
     * SetCanMove()
     * NAME
     *  SetCanMove - sets the value of m_canMove.
     * SYNOPSIS
     *  void SetCanMove(bool a_canMove)
     *      a_canMove --> the value that m_canMove will be set to. 
     * DESCRIPTION
     *  This allows the value of m_canMove to be changed if another class wants the follower to be able to or 
     *  not be able to move.  
     * RETURNS
     *  None
     */
    /**/
    public void SetCanMove(bool a_canMove)
    {
        m_canMove = a_canMove;
    }
    /*public void SetCanMove(bool a_canMove);*/
}
