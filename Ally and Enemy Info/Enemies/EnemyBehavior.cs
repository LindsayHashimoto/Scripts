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
	// Use this for initialization
	void Start ()
    {
        m_anim = GetComponent<Animator>();
        m_canMove = true; 
	}
	
	// Update is called once per frame
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player's Weapon")
        {
            m_chasingPlayer = true; 
        }
    }

    /**/
    /*void EnemyBehavior::SetCanMove(bool a_canMove);
     * NAME: 
     * 
    */
    public void SetCanMove(bool a_canMove)
    {
        m_canMove = a_canMove; 
    }

    public Animator GetAnim()
    {
        return m_anim; 
    }
}
