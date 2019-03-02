using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour {
    public float moveSpeed;

    private Animator anim;
    //private Rigidbody2D myRigidBody;

    private float moveX;
    private float moveY; 
    private Vector2 lastMove;
    private bool followerMoving;

    private static bool followerExists;

    public Transform target;

    private float currentSpeed;

    public float sprintModifier;

    // Use this for initialization
    void Start()
    {
        currentSpeed = moveSpeed; 
        anim = GetComponent<Animator>();
        //myRigidBody = GetComponent<Rigidbody2D>();

        if (!followerExists)
        {
            followerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        followerMoving = false; 
        Vector3 displacement = target.position - transform.position;
        displacement = displacement.normalized;

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

            anim.SetFloat("MoveX", displacement.x);
            anim.SetFloat("MoveY", displacement.y);
            anim.SetBool("PlayerMoving", followerMoving);
            anim.SetFloat("LastMoveX", displacement.x);
            anim.SetFloat("LastMoveY", displacement.y);
        }
    }
