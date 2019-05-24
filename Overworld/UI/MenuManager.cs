using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuManager : MenuMovement {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            switch (selected)
            {
                case 0:
                    SceneManager.LoadScene("New Game");
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
	}
}
