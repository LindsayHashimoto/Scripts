using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ErrorMessage : MonoBehaviour {

    public GameObject errorBox;
    private Text errorMessage;

    private bool textActive;

    private int textLenght = 60;

    // Use this for initialization
    void Start () {
        textActive = false;
        errorMessage = errorBox.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (textActive)
        {
            if (textLenght <= 0)
            {
                textActive = false;
                errorBox.SetActive(false);
                textLenght = 60;
            }
            textLenght--;
        }
    }
    public void displayMessage(string message)
    {
        errorBox.SetActive(true);
        textActive = true;
        errorMessage.text = message;
    }
}
