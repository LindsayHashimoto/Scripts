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
    private PauseMenuManager m_pmm; 

    private Button m_useBtn;
    private Button m_registerBtn;
    private Button m_buySellBtn;
    private Button m_cancelBtn; 

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  Sets the inital values of various member variables and sets the shop interface to be not active. 
     * RETURNS
     *  None
     */
    /**/
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
        m_cancelBtn = m_inventoryMenu.transform.Find("Cancel").gameObject.GetComponent<Button>();
        m_pmm = m_smsobj.GetComponentInChildren<PauseMenuManager>(); 


        m_shopMenuBtns.SetActive(false); 

        m_canTalk = false;
        m_greetingIsActive = false;
        m_goodbyeIsActive = false;
        m_menuActive = false; 

        m_useBtn.gameObject.SetActive(false);
        m_registerBtn.gameObject.SetActive(false);
        m_buySellBtn.gameObject.SetActive(false); 


    }
    /*void Start();*/

    /**/
    /*
     * Update()
     * NAME 
     *  Update - Update is called once per frame
     * SYNOPSIS
     *  void Update()
     * DESCRIPTION
     *  This handles what happens when the user presses the space bar. If the player is able to talk to the shopowner, the shopowner
     *  will greet the player. When the greeting is active and the player pushes the space bar again, the game pauses and makes the 
     *  shop interface appear. When the player exits the shop, the shopkeeper says a goodbye statement. When this is active and the 
     *  player presses the space bar agaian, the game is no longer paused. If the shop menu is open, the use and register buttons 
     *  should not be interactable. 
     * RETURNS
     *  None
     */
    /**/
    void Update ()
    {
        if (Input.GetButtonUp("Interact"))
        {
            if (m_canTalk)
            {
                m_explinationText.SetMessage("Shopkeeper: Hello and welcome to my shop!");
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
                m_cancelBtn.gameObject.SetActive(false);
                m_greetingIsActive = false;
                m_pmm.SetCanPause(false);
                Time.timeScale = 0f;
            }
            else if (m_goodbyeIsActive)
            {
                m_menuActive = false;
                m_explinationText.gameObject.SetActive(false);
                m_goodbyeIsActive = false;
                m_canTalk = true;
                m_cancelBtn.gameObject.SetActive(true);
                m_pmm.SetCanPause(true);
                Time.timeScale = 1f; 
            }
        }
        if (m_menuActive)
        {
            m_useBtn.gameObject.SetActive(false);
            m_registerBtn.gameObject.SetActive(false);
            m_pmm.SetCanPause(false); 
        }
    }
    /*void Update();*/

    /**/
    /*
     * SetListeners()
     * NAME
     *  SetListeners - sets the on click listeners for the buttons used to interact with the shop.
     * SYNOPSIS
     *  void SetListeners()
     * DESCRIPTION
     *  This funciton sets up the on click listeners for the shopkeeper menu. 
     * RETURNS
     *  None
     */
    /**/
    private void SetListeners()
    {
        m_buyBtn.onClick.AddListener(OnBuyClick);
        m_sellBtn.onClick.AddListener(OnSellClick);
        m_exitBtn.onClick.AddListener(OnExitClick); 
    }
    /*private void SetListeners();*/

    /*private void OnTriggerStay2D(Collider2D other);*/

    /**/
    /*
     * OnTriggerEnter2D()
     * NAME
     *  OnTriggerEnter2D - perform action when an object enters this trigger. 
     * SYNOPSIS
     *  void OnTriggerEnter2D(Collider2D a_other)
     *      a_other --> the objected that entered this trigger. 
     * DESCRIPTION
     *  When the player enters this trigger, the player should be able to talk to the shopkeeper. 
     * RETURNS
     *  None
     */
    /**/
    private void OnTriggerEnter2D(Collider2D a_other)
    {
        if (a_other.gameObject.name == "Player")
        {
            m_canTalk = true; 
        }
    }
    /*private void OnTriggerEnter2D(Collider2D other);*/

    /**/
    /*
     * OnTriggerExit2D()
     * NAME
     *  OnTriggerExit2D - perform action when an object exits the trigger. 
     * SYNOPSIS
     *  void OnTriggerExit2D(Collider2D a_other)
     *      a_other --> the object leaving this trigger. 
     * DESCRIPTION
     *  When the player leaves this trigger, they should no longer be able to interact with the shopkeeper. 
     * RETURNS
     *  None
     */
    /**/
    private void OnTriggerExit2D(Collider2D a_other)
    {
        if (a_other.gameObject.name == "Player")
        {
            m_canTalk = false;
        }
    }
    /*private void OnTriggerExit2D(Collider2D other);*/

    /**/
    /*
     * OnBuyClick()
     * NAME
     *  OnBuyClick - is called when the player clicks on the buy button.
     * SYNOPSIS
     *  void OnBuyClick()
     * DESCRIPTION
     *  When the player clicks on this button, the list of purchasable items appears to the user. The buying and selling 
     *  button's text changes to "Buy Item" and when the user clicks this button, they will purchase the selected item. 
     * RETURNS 
     *  None
     */
    /**/
    void OnBuyClick()
    {
        m_itemsToBuy.SetActive(true);
        m_inventoryList.SetActive(false);
        m_shopData.UpdateShopData(); 

        m_buySellBtn.gameObject.GetComponentInChildren<Text>().text = "Buy Item";
        m_buySellBtn.onClick.RemoveAllListeners();
        m_buySellBtn.onClick.AddListener(OnBuyingClick);
        m_buySellBtn.gameObject.SetActive(true);
    }
    /*void OnBuyClick();*/

    /**/
    /*
     * OnSellClick()
     * NAME
     *  OnSellClick - is called when the player clicks on the sell button. 
     * SYNOPSIS
     *  void OnSellClick()
     * DESCRIPTION
     *  When this button is clicked, the player's inventory appears. The buy and sell button's text changes to "Sell Item" and 
     *  when the user clicks on that button, the player will sell the selected item. 
     * RETURNS
     *  None
     */
    /**/
    void OnSellClick()
    {
        m_itemsToBuy.SetActive(false);
        m_inventoryList.SetActive(true);
        m_invMan.UpdateUIInventory();

        m_buySellBtn.gameObject.GetComponentInChildren<Text>().text = "Sell Item";
        m_buySellBtn.onClick.RemoveAllListeners(); 
        m_buySellBtn.onClick.AddListener(OnSellingClick);
        m_buySellBtn.gameObject.SetActive(true); 
    }
    /*void OnSellClick();*/

    /**/
    /*
     * OnExitClick()
     * NAME
     *  OnExitClick - is called when the user clicks on the exit button. 
     * SYNOPSIS
     *  void OnExitClick()
     * DESCRIPTION
     *  When the user clicks this button, the shopping interface becomes inactive and the shopkeeper says a "goodbye" message. 
     * RETURNS
     *  None
     */
    /**/
    void OnExitClick()
    {
        m_buySellBtn.gameObject.SetActive(false); 
        m_inventoryMenu.SetActive(false); 
        m_itemsToBuy.SetActive(false); 
        m_shopMenuBtns.SetActive(false); 
        m_explinationText.SetMessage("Shopkeeper: Thank you! Come again!");
        m_goodbyeIsActive = true; 
    }
    /*void OnExitClick();*/

    /**/
    /*
     * OnBuyingClick()
     * NAME
     *  OnBuyingClick - is called when the user clicks on the "Buy Item" button. 
     * SYNOPSIS
     *  void OnBuyingClick()
     * DESCRIPTION
     *  When the user clicks on this button, the player buys the selected item and adds it to their inventory. 
     * RETURNS
     *  None
     */
    /**/
    void OnBuyingClick()
    {
        m_playerInventory.BuyItem(m_shopData.GetActiveItem());
        m_shopData.UpdateShopData(); 
    }
    /*void OnBuyingClick();*/

    /**/
    /*
     * OnSellingClick()
     * NAME
     *  OnSellingClick - is called when the player clicks on the "Sell Item" button. 
     * SYNOPSIS 
     *  void OnSellingClick()
     * DESCRIPTION
     *  When this is clicked, the selected item is sold and removed from the player's inventory. 
     * RETURNS
     *  None
     */
    /**/
    void OnSellingClick()
    {
        m_playerInventory.SellItem(m_invMan.GetActiveItem());
        m_shopData.UpdateShopData();
    }
    /*void OnSellingClick();*/
}
