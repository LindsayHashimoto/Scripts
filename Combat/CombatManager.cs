using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    private Stats[] m_turnOrder;
    private int m_currentTurn = 0; 
    public GameObject[] m_turnOrderUI; 


    private Stats currentEntity; 

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //Although Quicksort has a faster average runtime, Insertion sort is faster in smaller sizes. n should be <10. 
    //https://en.wikipedia.org/wiki/Insertion_sort 
    private void InsertionSort(Stats [] a_sortArray)
    {

    }
    private void GenerateTurnOrder()
    {    
        for (int i = 0; i < m_turnOrder.Length; i++)
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

    //returns true if all enemies have been defeated.
    private bool PlayerWins()
    {
        for (int i = 0; i < m_turnOrder.Length; i++)
        {
            if (m_turnOrder[i].m_isEnemy && m_turnOrder[i].m_hp > 0) 
            {
                return false; 
            }
        }
        return true; 
    }

    //return true if all allies are defeated
    private bool EnemyWins()
    {
        for (int i = 0; i < m_turnOrder.Length; i++)
        {
            if (!m_turnOrder[i].m_isEnemy && m_turnOrder[i].m_hp > 0)
            {
                return false;
            }
        }
        return true; 
    }

    //Moves onto the next person in combat
    private void NextTurn()
    {
        do
        {
            if (m_currentTurn >= m_turnOrder.Length)
            {
                m_currentTurn = 0;
            }
            else
            {
                m_currentTurn++;
            }
        //move onto next enitity if current is dead. 
        } while (m_turnOrder[m_currentTurn].m_hp <= 0);
    }


}
