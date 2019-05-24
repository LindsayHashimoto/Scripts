
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
    private int weaponRange;
    private float m_moveSpeed = 8;

    private float m_diagonalModifier = 0.75f;


    private SpriteRenderer sr;
    private PlayerController m_playerController;
    public ErrorMessage error;

    public GameObject droppedWeapon;

    // Use this for initialization
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
    }

    // Update is called once per frame
    void Update()
    {
        //If game is not paused
        if (Time.timeScale > 0f)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //if no weapon is set, display error message
                if (m_inventoryManager.GetRegisteredWeapon() == null)
                {
                    error.displayMessage("No Weapon Set!");
                }
                //else, decrement the ammo counter and use the weapon
                else
                {
                    //if ammo counter = 0, remove weapon from inventory. Display message that the weapon broke.
                    if (m_inventoryManager.GetRegisteredWeapon().GetDurability() <= 0)
                    {
                        error.displayMessage("The set weapon is out of ammo!");
                    }

                    // throw weapon
                    if (!m_weaponMoving)
                    {
                        m_inventoryManager.GetRegisteredWeapon().RemoveDurability();
                        m_weaponMoving = true;
                        m_weapon.SetActive(true);
                        //weapon should travel in the direction the player is facing
                        SetDirection();
                        //weapon should face the direction it is heaing towards
                        SetRotation();
                        m_weaponBody.velocity = new Vector2(m_movingDirection.x * m_moveSpeed, m_movingDirection.y * m_moveSpeed) * m_diagonalModifier;
                    }
                    else
                    {
                        error.displayMessage("Can't use that yet.");
                    }
                }
            }
        }
    }
    // Spawns on the floor to be picked up by the player
    public void DropWeapon()
    {
        droppedWeapon.transform.position = m_transformWeapon.position;
        droppedWeapon.SetActive(true);
    }

    public void WeaponStop()
    {
        m_weaponMoving = false;
        m_weapon.SetActive(false);
    }

    public void ResetWeaponPosition()
    {
        m_transformWeapon.position = this.transform.position;
    }

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

    private void SetRotation()
    {

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

    public InventoryManager GetInventoryManager()
    {
        return m_inventoryManager; 
    }
}
