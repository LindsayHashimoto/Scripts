using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading; 

public class ErrorMessage : MonoBehaviour {

    private Text m_errorMessage;
    private Thread m_tr;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  Sets the inital value of m_errorMessage and sets this game object to be not active. 
     * RETURNS
     *  None
     */
    /**/
    void Start ()
    {
        m_errorMessage = this.GetComponent<Text>();
        this.gameObject.SetActive(false);
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
     *  This makes the error message disappear when the thread terminates. 
     * RETURNS
     *  None
     */
    /**/
    void Update () {
        if (!m_tr.IsAlive)
        {
            this.gameObject.SetActive(false);
        }
           
    }
    /*void Update();*/

    /**/
    /*
     * DisplayMessage()
     * NAME
     *  DisplayMessage -
     * SYNOPSIS
     *  void DisplayMessage(string a_message)
     *      a_message --> the message that will be displayed to the user.
     * DESCRIPTION
     *  The message text is set to be a_message and appears to the user. The message stays for a second and disappears.
     * RETURNS
     *  None
     */
    /**/
    public void DisplayMessage(string a_message)
    {
        this.gameObject.SetActive(true);
        m_errorMessage.text = a_message;
        m_tr = new Thread(new ThreadStart(WaitThread));
        m_tr.Start();
        
    }
    /*public void displayMessage(string message);*/

    /**/
    /*
     * WaitThread()
     * NAME
     *  WaitThread - waits for a second and terminates
     * SYNOPSIS
     *  void WaitThread()
     * DESCRIPTION
     *  This waits for a second and when this terminates, the error message will disappear. 
     * RETURNS
     *  None
     */
    /**/
    private void WaitThread()
    {
        Thread.Sleep(1000); 
    }
    /*private void WaitThread();*/
}
