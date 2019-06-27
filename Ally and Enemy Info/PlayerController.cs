using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float m_moveSpeed;
    private float currentSpeed;

    private float m_diagonalMoveModifier;

    private Animator m_anim;
    private Rigidbody2D m_rigidBody; 

    private bool m_playerMoving;
    private Vector2 m_lastMove;

    private static bool m_playerExists;

    private bool m_canMove;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  Sets inital values for the above member variables. 
     * RETURNS
     *  None
     */
    /**/
    void Start () {
        m_canMove = true; 
        m_anim = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_diagonalMoveModifier = 0.75f;
        m_moveSpeed = 7;
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
     *  This takes in the movement input from the user and plays the appropriate animation. 
     * RETURNS
     *  None
     */
    /**/
    void Update () {
        this.gameObject.SetActive(true); 
        m_playerMoving = false;
        if (!m_canMove)
        {
            m_rigidBody.velocity = Vector2.zero;

        }
        else
        {
            //Player is moving left or right

            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                m_rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentSpeed, m_rigidBody.velocity.y);
                m_playerMoving = true;
                m_lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
            //Player is moving up or down
            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, Input.GetAxisRaw("Vertical") * currentSpeed);
                m_playerMoving = true;
                m_lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
            //Player stopped moving in the x direction
            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
            {
                m_rigidBody.velocity = new Vector2(0f, m_rigidBody.velocity.y);
            }
            //Player stopped moving in the y direction
            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
            {
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, 0f);
            }
            //Player is moving diagonally
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
            {
                currentSpeed = m_moveSpeed * m_diagonalMoveModifier;
            }
            else
            {
                currentSpeed = m_moveSpeed;
            }

            // Game is not paused
            if (Time.timeScale > 0f)
            {
                m_anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
                m_anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
                m_anim.SetBool("PlayerMoving", m_playerMoving);
                m_anim.SetFloat("LastMoveX", m_lastMove.x);
                m_anim.SetFloat("LastMoveY", m_lastMove.y);
            }
        }
    }
    /*void Update();*/

    /**/
    /*
     * GetLastMove()
     * NAME
     *  GetLastMove - accessor for m_lastMove
     * SYNOPSIS
     *  Vector2 GetLastMove()
     * DESCRIPTION
     *  Retuns which direction the player was moving last. 
     * RETURNS
     *  m_lastMove
     */
    /**/
    public Vector2 GetLastMove()
    {
        return m_lastMove; 
    }
    /*public Vector2 GetLastMove();*/

    /**/
    /*
     * GetCanMove()
     * NAME
     *  GetCanMove - accessor for m_canMove
     * SYNOPSIS
     *  bool GetCanMove()
     * DESCRIPTION
     *  Returns whether or not the player can move. 
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
     * GetAnim()
     * NAME
     *  GetAnim - accessor for m_anim
     * SYNOPSIS
     *  Animator GetAnim()
     * DESCRIPTION
     *  Returns the animator for the player. 
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
     * SetCanMove()
     * NAME
     *  SetCanMove - setter for m_canMove
     * SYNOPSIS
     *  void SetCanMove(bool a_canMove)
     *      a_canMove --> the value m_canMove will be set to.
     * DESCRIPTION
     *  Sets the value of m_canMove.
     * RETURNS
     *  m_canMove
     */
    /**/
    public void SetCanMove(bool a_canMove)
    {
        m_canMove = a_canMove; 
    }
    /*public void SetCanMove(bool a_canMove);*/
}
