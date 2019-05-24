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
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            Thread.Sleep(2000);
            this.gameObject.SetActive(false);
        }
    }

    public void SetMessage(string a_message)
    {
        //m_messageBox.text = a_message;
        //this.gameObject.SetActive(true);
    }
}
