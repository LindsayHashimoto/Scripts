using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CombatManager : MonoBehaviour {

    private Stats[] m_turnOrder;

    private int m_currentTurn = 0;

    private Text [] m_turnOrderUI;
    private GameObject m_turnMenu;

    private Button m_attackBtn;
    private Button m_inventoryBtn;
    private Button m_passBtn;
    private Button m_backBtn;
    private Button m_useBtn;
    private Button m_cancelBtn;

    private bool m_attackClicked;
    private bool m_itemClicked;
    private InventoryManager m_inventoryManager;

    private ExplinationText m_explinationText; 

    // Use this for initialization
    void Start()
    {
        m_turnMenu = GameObject.FindGameObjectWithTag("TurnMenu");
        Button[] turnMenuBtns = m_turnMenu.GetComponentsInChildren<Button>();
        m_attackBtn = turnMenuBtns[0];
        m_inventoryBtn = turnMenuBtns[1];
        m_passBtn = turnMenuBtns[2];
        m_backBtn = GameObject.FindGameObjectWithTag("BackBtn").GetComponent<Button>();
        m_useBtn = GameObject.FindGameObjectWithTag("UseBtn").GetComponent<Button>();
        m_cancelBtn = GameObject.FindGameObjectWithTag("CancelBtn").GetComponent<Button>();

        m_inventoryManager = FindObjectOfType<InventoryManager>();

        SetListeners();

        BuildInitialTurnOrder(); 

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

        m_backBtn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetListeners()
    {
        m_attackBtn.onClick.AddListener(OnAttackClick);
        m_inventoryBtn.onClick.AddListener(OnInventoryClick);
        m_passBtn.onClick.AddListener(OnPassClick);
        m_backBtn.onClick.AddListener(OnBackClick);
        m_useBtn.onClick.AddListener(OnUseClick);
        m_cancelBtn.onClick.AddListener(OnBackClick);
    }
    private void BuildInitialTurnOrder()
    {
        Stats[] players = GameObject.FindGameObjectWithTag("Allies").GetComponentsInChildren<Stats>();
        Stats[] enemies = GameObject.FindGameObjectWithTag("Enemies").GetComponentsInChildren<Stats>();
        m_turnOrder = new Stats[players.Length + enemies.Length];

        int index = 0;
        foreach (Stats player in players)
        {
            m_turnOrder[index] = player;
            index++;
        }
        foreach (Stats enemy in enemies)
        {
            m_turnOrder[index] = enemy;
            index++;
        }

        Text[] tmpTurnOrder = GameObject.FindGameObjectWithTag("TurnOrder").GetComponentsInChildren<Text>();
        m_turnOrderUI = new Text[tmpTurnOrder.Length - 1];
        for (int i = 0; i < m_turnOrderUI.Length; i++)
        {
            m_turnOrderUI[i] = tmpTurnOrder[i + 1];
        }
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
            m_turnOrderUI[i].text = m_turnOrder[i].m_entityName + ": " + m_turnOrder[i].m_initiative;
            m_turnOrderUI[i].gameObject.SetActive(true);     
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
        m_backBtn.gameObject.SetActive(false);
        m_turnOrder[m_currentTurn].OnCurrentTurn(); 
    } 

    public void BasicAttack(Stats a_target)
    {
        Stats user = m_turnOrder[m_currentTurn]; 
        // Basic Attack should be realatively weak 
        int damageToDo = CalculateDamage(user, a_target, 5);
        m_explinationText.SetMessage(user.m_entityName + " used a basic attack on " + a_target.m_entityName);
        a_target.GetHealthManager().DealDamage(damageToDo); 
        // Performing this aciton ends the turn. 
        NextTurn(); 
    }

    public void UseWeapon(Stats a_target, Weapons a_item)
    {
        if(a_item.GetDurability() <= 0)
        {
            m_explinationText.SetMessage("Error: This item has no durability left.");
            return;
        }
        // miss
        if(UnityEngine.Random.Range(1,101) >= a_item.GetAccuracy())
        {
            m_explinationText.SetMessage(m_turnOrder[m_currentTurn].m_entityName + "'s attack missed!");
            return; 
        }
        //hit
        else
        {
            m_explinationText.SetMessage(m_turnOrder[m_currentTurn].m_entityName + " attacked " + a_target.m_entityName + " with the " + a_item.GetName());
            int damageToDo = CalculateDamage(m_turnOrder[m_currentTurn], a_target, a_item.GetDamage());
            a_target.GetHealthManager().DealDamage(damageToDo);
            a_item.RemoveDurability();
            NextTurn(); 
        }

    }

    public void UsePotion(Stats a_target, Potions a_item)
    {
        if (a_item.GetDurability() <= 0)
        {
            m_explinationText.SetMessage("Error: This item has no durability left.");
            return;
        }
        m_explinationText.SetMessage(m_turnOrder[m_currentTurn].m_entityName + " used a " + a_item + " on " + a_target.m_entityName);
        a_target.GetHealthManager().Heal(a_item.GetHeal());
        a_item.RemoveDurability();
        NextTurn(); 
    }
    
    public int CalculateDamage(Stats a_user, Stats a_target, int a_baseDamage)
    {
        int damage = a_baseDamage + a_user.m_atk - a_target.m_def;
        // Prevents the damage value from being negative
        if (damage < 1)
            damage = 1;
        return damage; 
    }

    public Stats [] GetTurnOrder()
    {
        return m_turnOrder;
    }

    public int GetCurrentTurn()
    {
        return m_currentTurn;
    }

    public GameObject GetTurnMenu()
    {
        return m_turnMenu;
    }

    public bool GetAttackClicked()
    {
        return m_attackClicked;
    }

    public bool GetItemClicked()
    {
        return m_itemClicked;
    }

    public void SetAttackClicked(bool a_input)
    {
        m_attackClicked = a_input;
    }

    public void SetItemClicked(bool a_input)
    {
        m_itemClicked = a_input;
    }

    private void OnAttackClick()
    {
        m_attackClicked = true;
        m_turnMenu.SetActive(false);
        m_backBtn.gameObject.SetActive(true);
    }

    private void OnInventoryClick()
    {
        m_turnMenu.SetActive(false);
        m_inventoryManager.gameObject.SetActive(true);
    }

    private void OnPassClick()
    {
        NextTurn(); 
    }

    private void OnBackClick()
    {
        m_attackClicked = false;
        m_itemClicked = false;
        m_turnMenu.SetActive(true);
        m_inventoryManager.gameObject.SetActive(false);
        m_backBtn.gameObject.SetActive(false);
    }

    private void OnUseClick()
    {
        m_inventoryManager.gameObject.SetActive(false);
        m_turnMenu.SetActive(false);
        m_backBtn.gameObject.SetActive(true);
    }
}
