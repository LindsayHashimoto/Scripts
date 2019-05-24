using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float m_moveSpeed = 7;
    private float currentSpeed;

    private float m_diagonalMoveModifier = 0.75f;
    private float m_sprintModifier = 2; 

    private Animator m_anim;
    private Rigidbody2D m_rigidBody; 

    private bool m_playerMoving;
    private Vector2 m_lastMove;

    private static bool m_playerExists;

    public bool m_canMove;

	// Use this for initialization
	void Start () {
        m_canMove = true; 
        m_anim = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody2D>();

        if (!m_playerExists)
        {
            m_playerExists = true; 
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy (gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_canMove)
        {
            m_rigidBody.velocity = Vector2.zero; 
        }
        m_playerMoving = false;
        
        //Player is moving left or right
        
		if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
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
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
        {
            currentSpeed = m_moveSpeed * m_diagonalMoveModifier; 
        }
        else
        {
            currentSpeed = m_moveSpeed; 
        }
        //Player is sprinting
       if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = m_moveSpeed * m_sprintModifier; 
        }
       //Player stopped sprinting
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = m_moveSpeed; 
        }
        // Game is not paused
        if(Time.timeScale > 0f)
        {
            m_anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
            m_anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
            m_anim.SetBool("PlayerMoving", m_playerMoving);
            m_anim.SetFloat("LastMoveX", m_lastMove.x);
            m_anim.SetFloat("LastMoveY", m_lastMove.y);
        }
    }

    public Vector2 GetLastMove()
    {
        return m_lastMove; 
    }
}
