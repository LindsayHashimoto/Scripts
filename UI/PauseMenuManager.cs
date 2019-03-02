using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PauseMenuManager : MonoBehaviour {

    public GameObject menuBox; 
    public bool menuActive;

    public GameObject invBox;
    public bool invActive; 
    public Button invButt;

    public Button closeButt; 
    // Use this for initialization
    void Start () {
        invButt.onClick.AddListener(openInventory);
        closeButt.onClick.AddListener(closeMenu); 
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (!menuActive)
            {
                menuBox.SetActive(true);
                menuActive = true;
                Time.timeScale = 0f;
            }
            else
            {
                closeMenu(); 
            }
        }
    }
    void openInventory()
    {
        if (!invActive)
        {
            invBox.SetActive(true);
            invActive = true;
        }
        else
        {
            invBox.SetActive(false);
            invActive = false;
        }
    }
    void closeMenu()
    {
        if (invActive)
        {
            invBox.SetActive(false);
            invActive = false;
        }
        menuBox.SetActive(false);
        menuActive = false;
        Time.timeScale = 1f;
    }
}
