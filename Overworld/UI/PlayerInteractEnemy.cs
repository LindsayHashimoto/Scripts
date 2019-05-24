using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractEnemy : MonoBehaviour {

    public GameObject playerChoiceBox;
    private bool boxActive;

    public Button[] buttons;

    public GameObject thisPerson; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                setActive(); 
            }
        }
    }

    void setActive()
    {
        playerChoiceBox.SetActive(true);
        boxActive = true;
        Time.timeScale = 0f;
    }

    void KnockOut()
    {
        thisPerson.SetActive(false); 
    }

    void pickpocket()
    {

    }

    void backstab()
    {
 
    }

    void closeBox()
    {
        playerChoiceBox.SetActive(false);
        boxActive = false;
        Time.timeScale = 1f; 
    }
}
