
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowWeapon : MonoBehaviour
{

    private InventoryManager m_inventoryManager;

    private GameObject m_weapon;
    private Rigidbody2D m_weaponBody;
    private Transform m_transformWeapon;
    private bool m_weaponMoving;
    private Vector2 m_movingDirection;
    private float m_moveSpeed;

    private float m_diagonalModifier = 0.75f;


    private PlayerController m_playerController;

    private SceneManagerScript m_sm;
    private ErrorMessage m_error;
    private GameObject m_droppedWeapon;

    private float m_timer;

    private Weapons m_thrownWeapon;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization.
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  This sets the inital values of the above member variables. 
     * RETURNS
     *  None
     */
    /**/
    void Start()
    {
        m_inventoryManager = FindObjectOfType<InventoryManager>();
        m_playerController = FindObjectOfType<PlayerController>();

        m_weapon = GameObject.FindGameObjectWithTag("Player's Weapon");

        m_weaponMoving = false;
        m_weaponBody = m_weapon.GetComponent<Rigidbody2D>();
        m_transformWeapon = m_weapon.GetComponent<Transform>();
        m_movingDirection.x = -1;
        m_movingDirection.y = 0;
        m_diagonalModifier = 1;
        m_weapon.SetActive(false);

        m_moveSpeed = 8;
        m_sm = SceneManagerScript.m_sm;
        GameObject canvas = m_sm.transform.Find("Canvas").gameObject;
        m_error = canvas.transform.Find("Error Message").gameObject.GetComponent<ErrorMessage>();
        m_droppedWeapon = m_sm.transform.Find("Dropped Weapon").gameObject;
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
     *  This performs actions when the player presses the fire button. If there is no registgered weapon, an error 
     *  message will be displayed. If the weapon's durability is 0 or less, an error message is displayed and the 
     *  inventory is updated. If the weapon is not already moving, the weapon will be thrown, the timer is set, 
     *  and one durability is used up. While the game is not paused and a weapon is already moving, the timer will 
     *  decrement by Time.deltaTime until it reaches zero and the weapon will be dropped.
     * RETURNS
     *  None
     */
    /**/
    void Update()
    {
        //If game is not paused
        if (Time.timeScale > 0f)
        {
            if (Input.GetButtonUp("Fire"))
            {
                //if no weapon is set, display error message
                if (m_inventoryManager.GetRegisteredWeapon() == null)
                {
                    m_error.DisplayMessage("No Weapon Set!");
                    return;
                }
                //else, decrement the ammo counter and use the weapon
                else
                {
                    //if ammo counter = 0, remove weapon from inventory. Display message that the weapon broke.
                    if (m_inventoryManager.GetRegisteredWeapon().GetDurability() <= 0)
                    {
                        m_error.DisplayMessage("The set weapon is out of ammo!");
                        m_inventoryManager.GetPlayerInventory().UpdateInventory();
                        m_inventoryManager.ResetRegisteredWeapon(); 
                        return;
                    }

                    // throw weapon
                    if (!m_weaponMoving)
                    {
                        m_thrownWeapon = m_inventoryManager.GetRegisteredWeapon();
                        m_timer = 3;
                        m_inventoryManager.GetRegisteredWeapon().RemoveDurability();
                        m_weaponMoving = true;
                        m_weapon.SetActive(true);
                        //weapon should travel in the direction the player is facing
                        SetDirection();
                        //weapon should face the direction it is heaing towards
                        SetRotation();
                        m_weaponBody.velocity = new Vector2(m_movingDirection.x * m_moveSpeed, m_movingDirection.y * m_moveSpeed) * m_diagonalModifier;
                        m_inventoryManager.GetPlayerInventory().UpdateInventory();
                    }
                    else
                    {
                        m_error.DisplayMessage("Can't use that yet.");
                    }
                }
            }
            if (m_weaponMoving)
            {
                m_timer -= Time.deltaTime;
                if (m_timer <= 0)
                {
                    WeaponStop();
                    DropWeapon();
                    ResetWeaponPosition();
                    m_weapon.SetActive(false);
                }
            }
        }
    }
    /*void Update();*/

    /**/
    /*
     * DropWeapon()
     * NAME
     *  DropWeapon - spawns a weapon on the floor to be picked up by the player.
     * SYNOPSIS
     *  void DropWeapon()
     * DESCRIPTION
     *  This moves the weapon to be picked up to the position where the moving weapon stopped and sets it to be active.
     * RETURNS
     *  None
     */
    /**/
    public void DropWeapon()
    {
        m_droppedWeapon.transform.position = m_transformWeapon.position;
        m_droppedWeapon.SetActive(true);
        m_droppedWeapon.GetComponent<PickUpWeapon>().SetPickedUpWeapon(m_thrownWeapon);
    }
    /*public void DropWeapon();*/

    /**/
    /*
     * WeaponStop()
     * NAME
     *  WeaponStop - stops a weapon that is moving.
     * SYNOPSIS
     *  void WeaponStop()
     * DESCRIPTION
     *  The moving weapon is set to not be able to move and is set to be inactive. 
     * RETURNS
     *  None
     */
    /**/
    public void WeaponStop()
    {
        m_weaponMoving = false;
        m_weapon.SetActive(false);
    }
    /*public void WeaponStop();*/

    /**/
    /*
     * ResetWeaponPosition()
     * NAME
     *  ResetWeaponPosition - moves the throwing weapon back to the player. 
     * SYNOPSIS
     *  void ResetWeaponPosition()
     * DESCRIPTION
     *  The weapon is moved to the position of the player and is set to be not active.
     * RETURNS
     *  None
     */
    /**/
    public void ResetWeaponPosition()
    {
        m_transformWeapon.position = this.transform.position;
        m_weapon.SetActive(false);
    }
    /*public void ResetWeaponPosition();*/

    /**/
    /*
     * SetDirection()
     * NAME
     *  SetDirection - sets which direction the weapon is moving. 
     * SYNOPSIS
     *  void SetDirection()
     * DESCRIPTION
     *  This moves in the direction that the player was facing last. 
     * RETURNS
     *  None
     */
    /**/
    private void SetDirection()
    {
        if (m_playerController.GetLastMove().x > 0.5f || m_playerController.GetLastMove().x < -0.5f)
        {
            //facing in a diagonal direction
            if (m_playerController.GetLastMove().y > 0.5f || m_playerController.GetLastMove().y < -0.5f)
            {
                m_movingDirection.x = m_playerController.GetLastMove().x;
                m_movingDirection.y = m_playerController.GetLastMove().y;
                m_diagonalModifier = 0.75f;
            }
            //facing left or right
            else
            {
                m_movingDirection.x = m_playerController.GetLastMove().x;
                m_movingDirection.y = 0f;
                m_diagonalModifier = 1f;
            }
        }
        //facing up or down
        else if (m_playerController.GetLastMove().y > 0.5f || m_playerController.GetLastMove().y < -0.5f)
        {
            m_movingDirection.x = 0f;
            m_movingDirection.y = m_playerController.GetLastMove().y;
            m_diagonalModifier = 1f;
        }
    }
    /*private void SetDirection();*/

    /**/
    /*
     * SetRotation()
     * NAME
     *  SetRotation - sets which direction the weapon is facing. 
     * SYNOPSIS
     *  void SetRotation()
     * DESCRIPTION
     *  This first resets the rotation to be 0 degrees. Then the weapon is rotatated in the direction that it will be 
     *  moving in. 
     * RETURNS
     *  None
     */
    /**/
    private void SetRotation()
    {
        m_transformWeapon.eulerAngles = new Vector3(0f, 0f, 0f);
        if (m_movingDirection.x > 0.5f)
        {
            // moving up right
            if (m_movingDirection.y > 0.5f)
            {
                m_transformWeapon.Rotate(0f, 0f, 315f);
            }
            //moving down right
            else if (m_movingDirection.y < -0.5f)
            {
                m_transformWeapon.Rotate(0f, 0f, 225f);
            }
            //moving right
            else
            {
                m_transformWeapon.Rotate(0f, 0f, 270f);
            }
        }

        else if (m_movingDirection.x < -0.5f)
        {
            // moving up left
            if (m_movingDirection.y > 0.5f)
            {
                m_transformWeapon.Rotate(0f, 0f, 45f);
            }
            //moving down left
            else if (m_movingDirection.y < -0.5f)
            {
                m_transformWeapon.Rotate(0f, 0f, 135f);
            }
            //moving left
            else
            {
                m_transformWeapon.Rotate(0f, 0f, 90f);
            }
        }
        //moving up
        if (m_movingDirection.y > 0.5f)
        {
            m_transformWeapon.Rotate(0f, 0f, 0f);
        }
        //moving down
        else if (m_movingDirection.y < -0.5f)
        {
            m_transformWeapon.Rotate(0f, 0f, 180f);
        }
    }
    /*private void SetRotation();*/

    /**/
    /*
     * GetInventoryManager()
     * NAME
     *  GetInventoryManager - accessor for inventory manager. 
     * SYNOPSIS
     *  InventoryManager GetInventoryManager()
     * DESCRIPTION
     *  Returns the InventoryManager class. 
     * RETURNS
     *  m_inventoryManager
     */
    /**/
    public InventoryManager GetInventoryManager()
    {
        return m_inventoryManager;
    }
    /*public InventoryManager GetInventoryManager();*/

    /**/
    /*
     * GetThrownWeapon()
     * NAME
     *  GetThrownWeapon - accessor for m_thrownWeapon.
     * SYNOPSIS
     *  Weapons GetThrownWeapon()
     * DESCRIPTION
     *  Returns the weapon that was thrown. 
     * RETURNS
     *  m_thrownWeapon. 
     */
    /**/
    public Weapons GetThrownWeapon()
    {
        return m_thrownWeapon; 
    }
    /*public Weapons GetThrownWeapon();*/
}
