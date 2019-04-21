using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class ThrowWeapon : MonoBehaviour {
    public ThrownWeaponData currentWeapon = null; //the weapon that is currently set

    public ThrownWeaponData[] weaponEntities;

    private float diagonalModifier;
    

    private SpriteRenderer sr;
    public PlayerController pc;
    public ErrorMessage error;

    public GameObject droppedWeapon;

    private int cooldown; 

    // Use this for initialization
    void Start() { 
        for(int i = 0; i<3; i++)
        {
            weaponEntities[i].weaponMoving = false;
            weaponEntities[i].weaponBody = weaponEntities[i].weapon.GetComponent<Rigidbody2D>();
            weaponEntities[i].transformWeapon = weaponEntities[i].weapon.GetComponent<Transform>();
            weaponEntities[i].moveX = -1;
            weaponEntities[i].moveY = 0;
        }
        diagonalModifier = 1; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f)
        {
            if (Input.GetAxis("Fire") >= 0.5f)
            {
                /*  //if no weapon is set, display error message
                      if (currentWeapon == null)
                      {
                          error.displayMessage("No Weapon Set!"); 
                      }
                      //else, decrement the ammo counter and use the weapon
                      else
                      {
                          currentWeapon.numberOf--; 
                          //if ammo counter = 0, remove weapon from inventory. Display message that the weapon broke.
                          if(currentWeapon.numberOf <= 0)
                          {
                              error.displayMessage("The set weapon is out of ammo!"); 
                          }*/
                // throw first weapon
                if (!weaponEntities[0].weaponMoving)
                {
                    weaponEntities[0].weaponMoving = true;
                    weaponEntities[0].weapon.SetActive(true);
                    //weapon should travel in the direction the player is facing
                    setDirection(weaponEntities[0]);
                    //weapon should face the direction it is heaing towards
                    setRotation(weaponEntities[0]);    
                }
                //throw second weapon
                else if (!weaponEntities[1].weaponMoving)
                {
                    weaponEntities[1].weaponMoving = true;
                    weaponEntities[1].weapon.SetActive(true);
                    //weapon should travel in the direction the player is facing
                    setDirection(weaponEntities[1]);
                    //weapon should face the direction it is heaing towards
                    setRotation(weaponEntities[1]);
                }
                // throw third weapon
                else if (!weaponEntities[2].weaponMoving)
                {
                    weaponEntities[2].weaponMoving = true;
                    weaponEntities[2].weapon.SetActive(true);
                    //weapon should travel in the direction the player is facing
                    setDirection(weaponEntities[2]);
                    //weapon should face the direction it is heaing towards
                    setRotation(weaponEntities[2]);
                }
                // }
            }
            for (int i = 0; i < 3; i++) {
                if (weaponEntities[i].weaponMoving)
                {
                    weaponEntities[i].weaponBody.velocity = new Vector2(weaponEntities[i].moveX * weaponEntities[i].moveSpeed, 
                        weaponEntities[i].moveY * weaponEntities[i].moveSpeed) * diagonalModifier;
                    //set weapon to be active and have it travel in one direction for a certain range before stopping
                    /* weaponRange--;
                     if (weaponRange <= 0)
                     {
                         weaponStop();
                         dropWeapon();
                         weaponRange = 60; 
                     }
                     */
                }
            }
        }
    }

    //should have a small change(depending on luck stat) to spawn on the floor to be picked up by the player
    public void dropWeapon(ThrownWeaponData thisWeapon)
    {
       // int randomValue = Random.Range(0, 101); 
       // if(randomValue >= (50))
       // {
            droppedWeapon.transform.position = thisWeapon.transformWeapon.position;
            droppedWeapon.SetActive(true); 
       // }
    }

    public void weaponStop(ThrownWeaponData thisWeapon)
    {
        thisWeapon.weaponMoving = false; 
        thisWeapon.weapon.SetActive(false); 
    }

    public void resetWeaponPosition(ThrownWeaponData thisWeapon)
    {
        thisWeapon.transform.position = transform.position; 
    }

    void setDirection(ThrownWeaponData thisWeapon)
    {
        if (pc.lastMove.x > 0.5f || pc.lastMove.x < -0.5f)
        {
            //facing in a diagonal direction
            if (pc.lastMove.y > 0.5f || pc.lastMove.y < -0.5f)
            {
                thisWeapon.moveX = pc.lastMove.x;
                thisWeapon.moveY = pc.lastMove.y;
                diagonalModifier = 0.75f;
            }
            //facing left or right
            else
            {
                thisWeapon.moveX = pc.lastMove.x;
                thisWeapon.moveY = 0f;
                diagonalModifier = 1f;
            }
        }
        //facing up or down
        else if (pc.lastMove.y > 0.5f || pc.lastMove.y < -0.5f)
        {
            thisWeapon.moveX = 0f;
            thisWeapon.moveY = pc.lastMove.y;
            diagonalModifier = 1f;
        }
    }

    void setRotation(ThrownWeaponData thisWeapon)
    {
        
        if(thisWeapon.moveX > 0.5f)
        {
            // moving up right
            if (thisWeapon.moveY > 0.5f)
            {
                thisWeapon.transformWeapon.Rotate(0f, 0f, 315f);
            }
            //moving down right
            else if(thisWeapon.moveY < -0.5f)
            {
                thisWeapon.transformWeapon.Rotate(0f, 0f, 225f);
            }
            //moving right
            else
            {
                thisWeapon.transformWeapon.Rotate(0f, 0f, 270f);
            }
        }

        else if (thisWeapon.moveX < -0.5f)
        {
            // moving up left
            if (thisWeapon.moveY > 0.5f)
            {
                thisWeapon.transformWeapon.Rotate(0f, 0f, 45f);
            }
            //moving down left
            else if (thisWeapon.moveY < -0.5f)
            {
                thisWeapon.transformWeapon.Rotate(0f, 0f, 135f);
            }
            //moving left
            else
            {
                thisWeapon.transformWeapon.Rotate(0f, 0f, 90f);
            }
        }
        //moving up
        if (thisWeapon.moveY > 0.5f)
        {
            thisWeapon.transformWeapon.Rotate(0f, 0f, 0f); 
        }
        //moving down
        else if (thisWeapon.moveY < -0.5f)
        {
            thisWeapon.transformWeapon.Rotate(0f, 0f, 180f);
        }
    }

   
}