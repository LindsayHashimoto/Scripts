using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PauseMenuManager : MonoBehaviour {

    private GameObject m_pauseMenu; 

    private GameObject m_inventoryObj; 
    private Button m_inventoryBtn;

    private Button m_closeBtn; 
    // Use this for initialization
    void Start () {
        m_pauseMenu = this.gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject;
        m_inventoryObj = GameObject.FindGameObjectWithTag("InventoryMenu");
        m_inventoryBtn = this.gameObject.GetComponentsInChildren<Button>()[0];
        m_closeBtn = this.gameObject.GetComponentsInChildren<Button>()[1];
        m_inventoryBtn.onClick.AddListener(OpenInventory);
        m_closeBtn.onClick.AddListener(ClosePauseMenu);
        m_pauseMenu.SetActive(false);
        m_inventoryObj.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //Pause Menu is opend by pressing Tab
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (!m_pauseMenu.activeSelf)
            {
                m_pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                ClosePauseMenu(); 
            }
        }
    }
    private void OpenInventory()
    {
        m_inventoryObj.SetActive(true); 
    }
    private void ClosePauseMenu()
    {
        m_pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
