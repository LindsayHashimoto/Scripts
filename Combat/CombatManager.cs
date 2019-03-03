using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public int numAllies;
    public int numEnemies;
    public int totalEntities;

    public Stats[] entities;
    private CircularLinkedList turnOrder; 


    private Stats currentEntity; 
    
    private QuickSort quickSort;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void generateTurnOrder()
    {
        totalEntities = numAllies + numEnemies;
        // generates initiative values
         
        for (int i = 0; i < totalEntities; i++)
        {
            entities[i].generateInitiative(); 
        }
        quickSort.quicksort(entities, 0, totalEntities);
        for (int i = 0; i < totalEntities; i++)
        {
            turnOrder.addNodeToEnd(entities[i]); 
        }
    }

    void nextTurn()
    {
        turnOrder.current = turnOrder.current.next;  
    }
}
