using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UnityCombatComponents : MonoBehaviour {

    

    private GameObject m_UIinventory;
    private GameObject m_turnMenu;

    private Text m_itemDescription; 
    private Text[] m_turnOrderUI;

    private Button[] m_turnMenuBtns = new Button[3];
    private Button m_attackBtn;
    private Button m_inventoryBtn;
    private Button m_passBtn;

    private Button m_backBtn;
    private Button m_useBtn;
    private Button m_cancelBtn;

    private Button[] m_UIbuttons;

    private Stats[] m_allEntities;

    private CombatManager m_combatManager;




    // Use this for initialization
    void Start () {
        
        m_UIinventory = GameObject.FindGameObjectWithTag("InventoryMenu");
        m_turnMenu = GameObject.FindGameObjectWithTag("PlayerTurnMenu");

        m_itemDescription = GameObject.FindGameObjectWithTag("ItemTxt").GetComponent<Text>(); 
        Text[] tmpTurnOrder = GameObject.FindGameObjectWithTag("TurnOrder").GetComponentsInChildren<Text>();
        m_turnOrderUI = new Text[tmpTurnOrder.Length-1]; 
        for(int i = 0; i<m_turnOrderUI.Length; i++)
        {
            m_turnOrderUI[i] = tmpTurnOrder[i + 1];
        }

        m_turnMenuBtns = GameObject.FindGameObjectWithTag("PlayerTurnMenu").GetComponentsInChildren<Button>();
        m_attackBtn = m_turnMenuBtns[0];
        m_inventoryBtn = m_turnMenuBtns[1];
        m_passBtn = m_turnMenuBtns[2];
        m_backBtn = GameObject.FindGameObjectWithTag("BackBtn").GetComponent<Button>();
        m_useBtn = GameObject.FindGameObjectWithTag("UseBtn").GetComponent<Button>();
        m_cancelBtn = GameObject.FindGameObjectWithTag("CancelBtn").GetComponent<Button>();

        Button [] btns = GameObject.FindGameObjectWithTag("InventoryUI").GetComponentsInChildren<Button>();
        m_UIbuttons = new Button[btns.Length - 1];
        for (int i = 0; i < btns.Length - 1; i++)
        {
            m_UIbuttons[i] = btns[i];
        }

        Stats[] players = GameObject.FindGameObjectWithTag("Allies").GetComponentsInChildren<Stats>(); 
        Stats[] enemies = GameObject.FindGameObjectWithTag("Enemies").GetComponentsInChildren<Stats>();
        m_allEntities = new Stats[players.Length + enemies.Length];

        int index = 0; 
        foreach (Stats player in players)
        {
            m_allEntities[index] = player;
            index++; 
        }
        foreach (Stats enemy in enemies)
        {
            m_allEntities[index] = enemy;
            index++;
        }

        m_UIinventory.SetActive(false);
        m_backBtn.gameObject.SetActive(false);
        m_useBtn.gameObject.SetActive(false);
        //m_cancelBtn.gameObject.SetActive(false);

        foreach (Button button in m_UIbuttons)
        {
            button.gameObject.SetActive(false); 
        }

        m_combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public CombatManager GetCombatManager()
    {
        return m_combatManager; 
    }

    public GameObject GetInventoryUI()
    {
        return m_UIinventory; 
    }

    public GameObject GetTurnMenu()
    {
        return m_turnMenu; 
    }

    public Text GetItemDescription()
    {
        return m_itemDescription; 
    }
    public Text[] GetUITurnOrder()
    {
        return m_turnOrderUI;
    }

    public Button GetAttackBtn()
    {
        return m_attackBtn;
    }
    
    public Button GetInventoryBtn()
    {
        return m_inventoryBtn;
    }

    public Button GetPassBtn()
    {
        return m_passBtn;
    }

    public Button GetBackBtn()
    {
        return m_backBtn;
    }

    public Button GetUseBtn()
    {
        return m_useBtn;
    }

    public Button GetCancelBtn()
    {
        return m_cancelBtn;
    }

    public Button[] GetUIBtns()
    {
        return m_UIbuttons; 
    }

    public Stats[] GetAllEntities()
    {
        return m_allEntities; 
    }
}
