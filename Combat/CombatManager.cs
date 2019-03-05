using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public int m_totalEntities;

    //public Stats[] m_entities;
    //private CircularLinkedList m_turnOrder;
    private Stats[] m_turnOrder;
    private Stats m_currentTurn; 
    public GameObject[] m_turnOrderUI; 


    private Stats currentEntity; 

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InsertionSort(Stats [] a_sortArray)
    {

    }
    private void GenerateTurnOrder()
    {    
        for (int i = 0; i < m_totalEntities; i++)
        {
            m_turnOrder[i].generateInitiative(); 
        }
        InsertionSort(m_turnOrder); 

        
        /*
        for (int i = 0; i < m_totalEntities; i++)
        {
            m_turnOrder.addNodeToEnd(m_entities[i]); 
        }
        */
    }



    void NextTurn()
    {
        //turnOrder.current = turnOrder.current.next;  
    }


}
