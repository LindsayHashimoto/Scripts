using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PauseMenuManager : MonoBehaviour {

    public GameObject m_pauseMenu; 

    public GameObject m_inventoryObj; 
    public Button m_inventoryBtn;

    public Button m_closeBtn; 
    // Use this for initialization
    void Start () {
        m_inventoryBtn.onClick.AddListener(OpenInventory);
        m_closeBtn.onClick.AddListener(ClosePauseMenu); 
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
