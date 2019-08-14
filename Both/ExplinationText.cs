using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplinationText : MonoBehaviour {

    private Text m_messageBox;
    private List<string> m_awaitingMessages = new List<string>();

    private GameObject m_sms;
    private PauseMenuManager m_pmm; 

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization.
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  This sets the inital values for m_pmm and m_messageBox and sets the latter to be initally not active. 
     * RETURNS
     *  None
     */
    /**/
    void Start()
    {
        m_sms = SceneManagerScript.m_sm.gameObject;
        m_pmm = m_sms.GetComponentInChildren<PauseMenuManager>();

        m_messageBox = GetComponentInChildren<Text>();
        this.gameObject.SetActive(false); 
    }
    /*void Start();*/

    /**/
    /*
     * Update()
     * NAME 
     *  Update - Update is called once per frame.
     * SYNOPSIS
     *  void Update()
     * DESCRIPTION
     *  When the user presses the interact button and the message box is active, one of two actions are performed 
     *  depending on if there are messages in the m_awaitingMessages list. If there are messages, the message box 
     *  text is changed to the first item in m_awaitingMessages and the first item is removed from the list. 
     *  Otherwise, the message box is set to longer be active and the game is un-paused. 
     * RETURNS
     *  None
     */
    /**/
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (this.gameObject.activeSelf)
            { 
                if (m_awaitingMessages.Count > 0)
                {
                    m_messageBox.text = m_awaitingMessages[0];
                    m_awaitingMessages.RemoveAt(0);
                }
                else
                {
                    this.gameObject.SetActive(false);
                    Time.timeScale = 1f;
                    m_pmm.SetCanPause(true);
                }
            }
        }
    }
    /*void Update();*/

    /**/
    /*
     * SetMessage()
     * NAME
     *  SetMessage - sends a message that will be displayed to the user. 
     * SYNOPSIS
     *  void SetMessage(string a_message)
     *      a_message --> the message that will be shown to the user. 
     * DESCRIPTION
     *  If this gameobject is not active, the message text is set to be the sent message, the message box appears 
     *  and the game is paused and the pause menu is disabled. If the message box is already active, the message 
     *  is added to the awaitingMessages list. 
     * RETURNS
     *  None
     */
    /**/
    public void SetMessage(string a_message)
    {
        if (this.gameObject.activeSelf)
        {
            m_awaitingMessages.Add(a_message); 
        }
        else
        {
            m_messageBox.text = a_message;
            this.gameObject.SetActive(true);
            Time.timeScale = 0f;
            m_pmm.SetCanPause(false);
        }

    }
    /*public void SetMessage(string a_message);*/

    /**/
    /*
     * GetAwaitingMessages()
     * NAME
     *  GetAwaitingMessages - accessor for m_awaitingMessages.
     * SYNOPSIS
     *  List<string> GetAwaitingMessages()
     * DESCRIPTION
     *  Returns the list of messages that have not been displayed yet. 
     * RETURNS
     *  m_awaitingMessages
     */
    /**/
    public List<string> GetAwaitingMessages()
    {
        return m_awaitingMessages; 
    }
    /*public List<string> GetAwaitingMessages();*/
}
