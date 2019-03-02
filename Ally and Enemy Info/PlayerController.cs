using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private float currentSpeed;

    public float diagonalMoveModifier;
    public float sprintModifier; 

    private Animator anim;
    private Rigidbody2D myRigidBody; 

    private bool playerMoving;
    public Vector2 lastMove;

    private static bool playerExists;

    public bool canMove;

	// Use this for initialization
	void Start () {
        canMove = true; 
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();

        if (!playerExists)
        {
            playerExists = true; 
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy (gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!canMove)
        {
            myRigidBody.velocity = Vector2.zero; 
        }
        playerMoving = false;
        
        //Player is moving left or right
        
		if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentSpeed, myRigidBody.velocity.y); 
            playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 
        }
        //Player is moving up or down
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Input.GetAxisRaw("Vertical") * currentSpeed);
            playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 
        }
        //Player stopped moving in the x direction
        if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
        {
            myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y); 
        }
        //Player stopped moving in the y direction
        if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0f);
        }
        //Player is moving diagonally
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
        {
            currentSpeed = moveSpeed * diagonalMoveModifier; 
        }
        else
        {
            currentSpeed = moveSpeed; 
        }
        //Player is sprinting
       if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = moveSpeed * sprintModifier; 
        }
       //Player stopped sprinting
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = moveSpeed; 
        }
        // Game is not paused
        if(Time.timeScale > 0f)
        {
            anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
            anim.SetBool("PlayerMoving", playerMoving);
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
        }
    }
}
