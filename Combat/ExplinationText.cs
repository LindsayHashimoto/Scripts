using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplinationText : MonoBehaviour {

    private Text m_messageBox;
    private List<string> m_awaitingMessages = new List<string>(); 

    // Use this for initialization
    void Start()
    {
        m_messageBox = GetComponentInChildren<Text>();
        this.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
                Time.timeScale = 1f;
                
                if (m_awaitingMessages.Count > 0)
                {
                    m_messageBox.text = m_awaitingMessages[0];
                    m_awaitingMessages.RemoveAt(0);
                    this.gameObject.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
        }
    }

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
        }

    }

    public List<string> GetAwaitingMessages()
    {
        return m_awaitingMessages; 
    }
}
