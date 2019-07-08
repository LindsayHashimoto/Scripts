using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PauseMenuManager : MonoBehaviour {

    private GameObject m_pauseMenu; 

    private GameObject m_inventoryObj; 
    private Button m_inventoryBtn;

    private Button m_closeBtn;

    private GameObject m_inventoryList;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  Sets the values of the above member values and sets on click listeners for the buttons. The pause menu and inventory menu
     *  are also set to be initally not active. 
     * RETURNS
     *  None
     */
    /**/
    void Start () {
        m_pauseMenu = this.gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject;
        m_inventoryObj = GameObject.FindGameObjectWithTag("InventoryMenu");
        m_inventoryBtn = this.gameObject.GetComponentsInChildren<Button>()[0];
        m_closeBtn = this.gameObject.GetComponentsInChildren<Button>()[1];
        m_inventoryBtn.onClick.AddListener(OpenInventory);
        m_closeBtn.onClick.AddListener(ClosePauseMenu);

        m_inventoryList = m_inventoryObj.transform.Find("Inventory List").gameObject; 
        m_pauseMenu.SetActive(false);
        m_inventoryObj.SetActive(false);
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
     *  When the player presses the tab button, the pause menu opens up and the game is paused. 
     * RETURNS
     *  None
     */
    /**/
    void Update () {
        //Pause Menu is opend by pressing Tab
        if (Input.GetAxisRaw("Pause") > 0.5f)
        {
            if (!m_pauseMenu.activeSelf)
            {
                m_pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    /*void Update();*/

    /**/
    /*
     * OpenInventory()
     * NAME
     *  OpenInventory - opens the inventory menu
     * SYNOPSIS
     *  void OpenInventory()
     * DESCRIPTION
     *  The inventory menu is opened. 
     * RETURNS
     *  None
     */
    /**/
    private void OpenInventory()
    {
        m_inventoryList.SetActive(true);
        m_inventoryObj.SetActive(true); 
    }
    /*private void OpenInventory();*/

    /**/
    /*
     * ClosePauseMenu()
     * NAME
     *  ClosePauseMenu - closes the pause menu
     * SYNOPSIS
     *  void ClosePauseMenu()
     * DESCRIPTION
     *  The pause menu is closed and the game is no longer paused.
     * RETURNS
     *  None
     */
    /**/
    private void ClosePauseMenu()
    {
        m_pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    /*private void ClosePauseMenu();*/
}
