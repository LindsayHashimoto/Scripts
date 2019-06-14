using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ShopInfo : MonoBehaviour {

    private GameObject m_shopMenuBtns; 
    private Button m_buyBtn;
    private Button m_sellBtn;
    private Button m_exitBtn;
    
    private GameObject m_smsobj;
    private ExplinationText m_explinationText;

    private GameObject m_itemsToBuy;

    private bool m_canTalk;
    private bool m_greetingIsActive;
    private bool m_goodbyeIsActive;
    private bool m_menuActive; 

    private ShopkeeperData m_shopData; 

    private SceneManagerScript m_sms; 
    private Inventory m_playerInventory;
    private InventoryManager m_invMan;
    private GameObject m_inventoryMenu; 
    private GameObject m_inventoryList;

    private Button m_useBtn;
    private Button m_registerBtn;
    private Button m_buySellBtn; 

    // Use this for initialization
    void Start ()
    {
        m_shopMenuBtns = GameObject.Find("Shop Menu Buttons"); 
        m_buyBtn = GameObject.Find("Buy").GetComponent<Button>();
        m_sellBtn = GameObject.Find("Sell").GetComponent<Button>();
        m_exitBtn = GameObject.Find("Exit").GetComponent<Button>();
        SetListeners();

        m_itemsToBuy = GameObject.Find("ShopKeeper Items");

        m_shopData = FindObjectOfType<ShopkeeperData>(); 

        m_smsobj = SceneManagerScript.m_sm.gameObject; 
        m_explinationText = m_smsobj.GetComponentInChildren<ExplinationText>(true);
        m_inventoryMenu = m_smsobj.transform.Find("Canvas").gameObject.transform.Find("Inventory Menu").gameObject;
        m_inventoryList = m_inventoryMenu.transform.Find("Inventory List").gameObject;
        m_useBtn = m_inventoryMenu.transform.Find("Use Item").gameObject.GetComponent<Button>();
        m_registerBtn = m_inventoryMenu.transform.Find("Register Button").gameObject.GetComponent<Button>();
        m_buySellBtn = m_inventoryMenu.transform.Find("Buy and Sell Button").gameObject.GetComponent<Button>();
        m_playerInventory = m_smsobj.transform.Find("Allies").GetComponentInChildren<Inventory>();
        m_invMan = m_inventoryMenu.GetComponentInChildren<InventoryManager>(true); 


        m_shopMenuBtns.SetActive(false); 

        m_canTalk = false;
        m_greetingIsActive = false;
        m_goodbyeIsActive = false;
        m_menuActive = false; 

        m_useBtn.gameObject.SetActive(false);
        m_registerBtn.gameObject.SetActive(false);
        m_buySellBtn.gameObject.SetActive(false); 


}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (m_canTalk)
            {
                m_explinationText.SetMessage("Blacksmith: Hello and welcome to my shop!");
                m_greetingIsActive = true; 
                m_canTalk = false; 
            }
            else if (m_greetingIsActive)
            {
                m_menuActive = true; 
                m_inventoryList.SetActive(false);
                m_inventoryMenu.SetActive(true); 
                m_shopMenuBtns.SetActive(true); 
                m_explinationText.gameObject.SetActive(false);
                m_greetingIsActive = false;
                Time.timeScale = 0f;
            }
            else if (m_goodbyeIsActive)
            {
                m_menuActive = false;
                m_explinationText.gameObject.SetActive(false);
                m_goodbyeIsActive = false;
                m_canTalk = true;
                Time.timeScale = 1f; 
            }
        }
        if (m_menuActive)
        {
            m_useBtn.gameObject.SetActive(false);
            m_registerBtn.gameObject.SetActive(false);
        }
    }

    private void SetListeners()
    {
        m_buyBtn.onClick.AddListener(OnBuyClick);
        m_sellBtn.onClick.AddListener(OnSellClick);
        m_exitBtn.onClick.AddListener(OnExitClick); 
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                m_explinationText.SetMessage("Blacksmith: Hello and welcome to my shop!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            m_canTalk = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            m_canTalk = false;
        }
    }

    void OnBuyClick()
    {
        m_itemsToBuy.SetActive(true);
        m_inventoryList.SetActive(false);
        m_buySellBtn.gameObject.GetComponentInChildren<Text>().text = "Buy Item";
        m_buySellBtn.onClick.RemoveAllListeners();
        m_buySellBtn.onClick.AddListener(OnBuyingClick);
        
    }

    void OnSellClick()
    {
        m_itemsToBuy.SetActive(false);
        m_buySellBtn.gameObject.GetComponentInChildren<Text>().text = "Sell Item";
        m_buySellBtn.onClick.RemoveAllListeners(); 
        m_buySellBtn.onClick.AddListener(OnSellingClick);
        m_buySellBtn.gameObject.SetActive(true); 
        m_inventoryList.SetActive(true);
        m_invMan.UpdateUIInventory(); 
    }

    void OnExitClick()
    {
        m_buySellBtn.gameObject.SetActive(false); 
        m_inventoryMenu.SetActive(false); 
        m_itemsToBuy.SetActive(false); 
        m_shopMenuBtns.SetActive(false); 
        m_explinationText.SetMessage("Blacksmith: Thank you! Come again!");
        m_goodbyeIsActive = true; 
    }

    void OnBuyingClick()
    {
        m_playerInventory.BuyItem(m_shopData.GetActiveItem());
        m_shopData.UpdateShopData(); 
    }

    void OnSellingClick()
    {
        m_playerInventory.SellItem(m_invMan.GetActiveItem());
        m_shopData.UpdateShopData();
    }
}
