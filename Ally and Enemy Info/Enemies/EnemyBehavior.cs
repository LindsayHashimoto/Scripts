using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public Transform player;

    public float runSpeed; 

    private bool chasingPlayer;

    private Animator anim;
    private Vector3 displacement; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (chasingPlayer)
        {
            displacement = player.position - transform.position;
            displacement = displacement.normalized;

            if (Vector2.Distance(player.position, transform.position) > 1.0f)
            {
                transform.position += (displacement * runSpeed * Time.deltaTime);
            }
            
        }
        //patrol the area
        else
        {

        }
        anim.SetFloat("MoveX", displacement.x);
        anim.SetFloat("MoveY", displacement.y);
        anim.SetBool("PlayerMoving", chasingPlayer);
        anim.SetFloat("LastMoveX", displacement.x);
        anim.SetFloat("LastMoveY", displacement.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            //start combat
        }
        if(other.gameObject.tag == "Player's Weapon")
        {
            chasingPlayer = true; 
        }
    }
}
