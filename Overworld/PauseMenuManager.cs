using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PauseMenuManager : MonoBehaviour {

    private GameObject m_pauseMenu; 

    private GameObject m_inventoryObj; 

    private Button m_inventoryBtn;
    private Button m_closeBtn;

    private bool m_canPause; 

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
        m_canPause = true; 
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
        if (m_canPause)
        {
            if (Input.GetButtonDown("Pause"))
            {
                if (!m_pauseMenu.activeSelf)
                {
                    m_pauseMenu.SetActive(true);
                    Time.timeScale = 0f;
                }
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

    /**/
    /*
     * SetCanPause()
     * NAME
     *  SetCanPause - sets the value of m_canPause
     * SYNOPSIS
     *  void SetCanPause(bool a_canPause)
     *      a_canPause --> the value m_canPause will be set to 
     * DESCRIPTION
     *  Sets the value of the member variable m_canPause to a_canPause. 
     * RETURNS
     *  None
     */
    /**/
    public void SetCanPause(bool a_canPause)
    {
        m_canPause = a_canPause; 
    }
    /*public void SetCanPause(bool a_canPause);*/

    /**/
    /*
     * GetCanPause()
     * NAME
     *  GetCanPause - returns the value of m_canPause
     * SYNOPSIS
     *  void GetCanPause()
     * DESCRIPTION
     *  This returns the value of m_canPause. 
     * RETURNS
     *  m_canPause
     */
    /**/
    public bool GetCanPause()
    {
        return m_canPause; 
    }
    /*public bool GetCanPause();*/
}
