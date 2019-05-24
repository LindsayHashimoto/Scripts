using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEditor;

public class ExplinationText : MonoBehaviour {

    private Text m_messageBox;
    private Thread m_tr; 

    // Use this for initialization
    void Start()
    {
        m_messageBox = GetComponentInChildren<Text>();
        this.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            m_tr = new Thread(new ThreadStart(WaitThread));
            m_tr.Start();
            m_tr.Join();
            this.gameObject.SetActive(false);
        }
    }

    public void SetMessage(string a_message)
    {
        m_messageBox.text = a_message;
        this.gameObject.SetActive(true);
    }

    private void WaitThread()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                return; 
            }
        }
    }
}
