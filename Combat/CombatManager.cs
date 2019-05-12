using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CombatManager : MonoBehaviour {

    public Stats[] m_turnOrder;
    
    public int m_currentTurn = 0; 
    public GameObject[] m_turnOrderUI; 

    private Stats m_currentEntity;

    public GameObject m_turnMenu;
    public GameObject m_backButton;
    public GameObject m_inventory; 

    private bool m_attackClicked = false;
    private bool m_itemClicked = false;
    

    // Use this for initialization
    void Start ()
    {
        GenerateTurnOrder();
        BuildUIOrder();
        if (!m_turnOrder[0].m_isEnemy)
        {
            m_turnMenu.SetActive(true);
        }
        else
        {
            m_turnMenu.SetActive(false);
        }
        m_turnOrder[m_currentTurn].OnCurrentTurn();
    }
	
	// Update is called once per frame
	void Update ()
    {
       
    }
    //Although Quicksort has a faster average runtime, Insertion sort is faster in smaller sizes. n should be <=8. 
    //https://www.geeksforgeeks.org/insertion-sort/
    private void InsertionSort(Stats [] a_sortArray)
    {
        int n = a_sortArray.Length; 
        for (int i = 1; i < n; ++i)
        {
            Stats key = a_sortArray[i];
            int j = i - 1;
            while (j >= 0 && a_sortArray[j].m_initiative <= key.m_initiative)
            {
                //Move the elements one spot ahead
                a_sortArray[j + 1] = a_sortArray[j];
                j--; 
            }
            //Swap
            a_sortArray[j + 1] = key;
        }
    }

    private void GenerateTurnOrder()
    {    
        for (int i = 0; i < m_turnOrder.Length; i++)
        {
            m_turnOrder[i].GenerateInitiative(); 
        }
        InsertionSort(m_turnOrder);
    }

    private void BuildUIOrder()
    {
        for(int i = 0; i < m_turnOrder.Length; ++i)
        {
            m_turnOrderUI[i].GetComponent<Text>().text = m_turnOrder[i].m_entityName + ": " + m_turnOrder[i].m_initiative;
            m_turnOrderUI[i].SetActive(true);     
        }
        m_turnOrderUI[m_currentTurn].GetComponent<Text>().text = m_turnOrder[m_currentTurn].m_entityName + ": " + m_turnOrder[m_currentTurn].m_initiative + "<";
    }

    //returns true if all enemies have been defeated.
    private bool PlayerWins()
    {
        for (int i = 0; i < m_turnOrder.Length; i++)
        {
            if (m_turnOrder[i].m_isEnemy && m_turnOrder[i].GetHealthManager().m_currentHealth > 0) 
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
            if (!m_turnOrder[i].m_isEnemy && m_turnOrder[i].GetHealthManager().m_currentHealth > 0)
            {
                return false;
            }
        }
        return true; 
    }

    //Moves onto the next person in combat
    public void NextTurn()
    {
        m_turnOrder[m_currentTurn].NoLongerTurn(); 
        m_turnOrderUI[m_currentTurn].GetComponent<Text>().text = m_turnOrder[m_currentTurn].m_entityName + ": " + m_turnOrder[m_currentTurn].m_initiative;
        do
        {
            if (m_currentTurn >= (m_turnOrder.Length - 1))
            {
                m_currentTurn = 0;
            }
            else
            {
                m_currentTurn++;
            }
        //move onto next enitity if current is dead. 
        } while (m_turnOrder[m_currentTurn].GetHealthManager().m_currentHealth <= 0);
        m_turnOrderUI[m_currentTurn].GetComponent<Text>().text = m_turnOrder[m_currentTurn].m_entityName + ": " + m_turnOrder[m_currentTurn].m_initiative + "<";
        if (!m_turnOrder[m_currentTurn].m_isEnemy)
        {
            m_turnMenu.SetActive(true);
        }
        else
        {
            m_turnMenu.SetActive(false);
        }
        m_backButton.SetActive(false);
        m_turnOrder[m_currentTurn].OnCurrentTurn(); 
    }

    

    public void BasicAttack(Stats a_target)
    {
        Stats user = m_turnOrder[m_currentTurn]; 
        // Basic Attack should be realatively weak 
        int damageToDo = CalculateDamage(user, a_target, 5);
        a_target.GetHealthManager().DealDamage(damageToDo); 
        // Performing this aciton ends the turn. 
        NextTurn(); 
    }

    public void UseWeapon(Stats a_target, Items a_items)
    {

    }
    
    public int CalculateDamage(Stats a_user, Stats a_target, int a_baseDamage)
    {
        int damage = a_baseDamage + a_user.m_atk - a_target.m_def;
        // Prevents the damage value from being negative
        if (damage < 1)
            damage = 1;
        return damage; 
    }

    
    public void GetTargetFromUser(Stats a_target)
    {
        if (m_attackClicked)
        {
            BasicAttack(a_target); 
        }
    }


    public void OnAttackClick()
    {
        m_attackClicked = true;
        m_turnMenu.SetActive(false);
        m_backButton.SetActive(true);
    }

    public void OnInventoryClick()
    {
        m_turnMenu.SetActive(false);
        m_backButton.SetActive(true);
        m_inventory.SetActive(true); 
    }

    public void OnBackClick()
    {
        m_attackClicked = false;
        m_itemClicked = false; 
        m_turnMenu.SetActive(true);
        m_inventory.SetActive(false);
        m_backButton.SetActive(false);
    }

}
