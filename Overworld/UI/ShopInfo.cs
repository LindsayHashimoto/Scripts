using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ShopInfo : MonoBehaviour {

    private GameObject m_shopMenuBtns; 
    private Button m_buyBtn;
    private Button m_sellBtn;
    private Button m_exitBtn;

    private GameObject m_itemsToBuy;
    private Button[] m_itemsInShop; 
    private GameObject m_smsobj;
    private ExplinationText m_explinationText;

    private bool m_canTalk;
    private bool m_greetingIsActive;
    private bool m_goodbyeIsActive; 

	// Use this for initialization
	void Start ()
    {
        m_shopMenuBtns = GameObject.Find("Shop Menu Buttons"); 
        m_buyBtn = GameObject.Find("Buy").GetComponent<Button>();
        m_sellBtn = GameObject.Find("Sell").GetComponent<Button>();
        m_exitBtn = GameObject.Find("Exit").GetComponent<Button>();
        SetListeners(); 

        m_itemsToBuy = GameObject.Find("Items to Buy");
        m_itemsInShop = m_itemsToBuy.GetComponentsInChildren<Button>(); 
        
        m_smsobj = SceneManagerScript.m_sm.gameObject; 
        m_explinationText = m_smsobj.GetComponentInChildren<ExplinationText>(true);

        m_shopMenuBtns.SetActive(false); 
        m_itemsToBuy.SetActive(false);

        m_canTalk = false;
        m_greetingIsActive = false;
        m_goodbyeIsActive = false; 
}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_canTalk)
            {
                m_explinationText.SetMessage("Blacksmith: Hello and welcome to my shop!");
                m_greetingIsActive = true; 
                m_canTalk = false; 
            }
            else if (m_greetingIsActive)
            {
                m_shopMenuBtns.SetActive(true); 
                m_explinationText.gameObject.SetActive(false);
                m_greetingIsActive = false;
                Time.timeScale = 0f;
            }
            else if (m_goodbyeIsActive)
            {
                m_explinationText.gameObject.SetActive(false);
                m_goodbyeIsActive = false;
                m_canTalk = true;
                Time.timeScale = 1f; 
            }
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
    }

    void OnSellClick()
    {

    }

    void OnExitClick()
    {
        m_itemsToBuy.SetActive(false); 
        m_shopMenuBtns.SetActive(false); 
        m_explinationText.SetMessage("Blacksmith: Thank you! Come again!");
        m_goodbyeIsActive = true; 
    }
}
