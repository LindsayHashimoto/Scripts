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
        m_turnOrder[0] = new Stats("Jerry", 1, 0, 10, 10, 10, false);
        m_turnOrder[1] = new Stats("Wolf", 1, 0, 5, 5, 5, true); 
        GenerateTurnOrder(); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //Although Quicksort has a faster average runtime, Insertion sort is faster in smaller sizes. n should be <10. 
    //https://www.geeksforgeeks.org/insertion-sort/
    private void InsertionSort(Stats [] a_sortArray)
    {
        for(int i = 1; i < a_sortArray.Length; i++)
        {
            Stats key = a_sortArray[i];
            for(int j = i - 1; j >= 0; j--)
            {
                if(a_sortArray[j].m_initiative <= key.m_initiative)
                {
                    //Swap
                    a_sortArray[j + 1] = a_sortArray[j]; 
                    continue; 
                }
                //Move the elements one spot ahead
                a_sortArray[j + 1] = a_sortArray[j];
            }
        }
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
