using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class OnCombatStart : MonoBehaviour {

    Scene m_thisScene;
    PlayerStartPoint m_startPoint;
    GameObject m_combatData; 

    // Use this for initialization
    void Start ()
    {
        
        m_startPoint = FindObjectOfType<PlayerStartPoint>();
        m_combatData = GameObject.FindGameObjectWithTag("EnemyCombat");
        m_combatData.SetActive(false); 
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            m_startPoint = FindObjectOfType<PlayerStartPoint>();
            m_thisScene = SceneManager.GetActiveScene();
            m_startPoint.transform.position = transform.position;
            m_combatData.SetActive(true); 
            SceneManager.LoadScene("Combat");
        }
    }

    public Scene GetLastScene()
    {
        return m_thisScene; 
    }
}
