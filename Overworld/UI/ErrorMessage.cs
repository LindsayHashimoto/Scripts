using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading; 

public class ErrorMessage : MonoBehaviour {

    private Text m_errorMessage;
    private Thread m_tr;

    // Use this for initialization
    void Start () {
        m_errorMessage = this.GetComponent<Text>();
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (!m_tr.IsAlive)
        {
            this.gameObject.SetActive(false);
        }
           
    }
    public void displayMessage(string message)
    {
        this.gameObject.SetActive(true);
        m_errorMessage.text = message;
        m_tr = new Thread(new ThreadStart(WaitThread));
        m_tr.Start();
        
    }

    private void WaitThread()
    {
        Thread.Sleep(1000); 
    }
}
