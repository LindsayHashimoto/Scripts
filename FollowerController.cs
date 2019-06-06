using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour {
    public float moveSpeed;

    private Animator m_anim;
    //private Rigidbody2D myRigidBody;

    private float moveX;
    private float moveY; 
    private Vector2 lastMove;
    private bool followerMoving;
    private bool m_canMove; 

    private static bool followerExists;

    public Transform target;

    private float currentSpeed;

    public float sprintModifier;

    // Use this for initialization
    void Start()
    {
        m_canMove = true; 
        currentSpeed = moveSpeed; 
        m_anim = GetComponent<Animator>();
        //myRigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        followerMoving = false;
        Vector3 displacement = target.position - transform.position;
        displacement = displacement.normalized;
        if (m_canMove)
        {
            

            if (Vector2.Distance(target.position, transform.position) > 1.0f)
            {
                transform.position += (displacement * currentSpeed * Time.deltaTime);
                followerMoving = true;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = moveSpeed * sprintModifier;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                currentSpeed = moveSpeed;
            }
        }
        m_anim.SetFloat("MoveX", displacement.x);
        m_anim.SetFloat("MoveY", displacement.y);
        m_anim.SetBool("PlayerMoving", followerMoving);
        m_anim.SetFloat("LastMoveX", displacement.x);
        m_anim.SetFloat("LastMoveY", displacement.y);
        

    }


    public Animator GetAnim()
    {
        return m_anim;
    }

    public bool GetCanMove()
    {
        return m_canMove;
    }

    public void SetCanMove(bool a_canMove)
    {
        m_canMove = a_canMove;
    }
}
