using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadNewArea : MonoBehaviour {

    public string m_levelToLoad;

    /**/
    /*
     * OnTriggerEnter2D()
     * NAME
     *  OnTriggerEnter2D - load a new scene when the player enters this area.
     * SYNOPSIS
     *  void OnTriggerEnter2D(Collider2D a_other)
     *      a_other --> the object that entered this area. 
     * DESCRIPTION 
     *  When the player enters the object this script is attached to, the new scene specified by the game object in 
     *  the unity editor is loaded. 
     * RETURNS 
     *  None
     */
    /**/
    void OnTriggerEnter2D(Collider2D a_other)
    {
        if(a_other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(m_levelToLoad); 
        }
    }
    /*void OnTriggerEnter2D(Collider2D other);*/
}
