using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

    private bool m_dontDestroy; 
	// Use this for initialization
	void Start ()
    {
        m_dontDestroy = false; 
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void SetDontDestroy(bool a_dontDestroy)
    {
        m_dontDestroy = a_dontDestroy; 
    }
    public bool GetDontDestroy()
    {
        return m_dontDestroy; 
    }
}
