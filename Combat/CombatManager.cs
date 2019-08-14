using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class CombatManager : MonoBehaviour {

    private Stats[] m_turnOrder;

    private int m_currentTurn = 0;

    private Stats[] m_players;
    private Stats[] m_enemies; 
    private GameObject m_turnOrderUIObj; 
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
    private OnCombatStart m_onCombatStart;

    private GameObject m_smsobj;
    private SceneManagerScript m_sms;
    private PauseMenuManager m_pmm; 

    private Thread m_thread;
    private int m_sceneID;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization.
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  This sets the inital values for the above member variables and generates the turn order.
     * RETURNS
     *  None
     */
    /**/
    void Start()
    {
        m_turnMenu = GameObject.FindGameObjectWithTag("TurnMenu");
        Button[] turnMenuBtns = m_turnMenu.GetComponentsInChildren<Button>(true);
        m_attackBtn = turnMenuBtns[0];
        m_inventoryBtn = turnMenuBtns[1];
        m_passBtn = turnMenuBtns[2];
        m_backBtn = GameObject.FindGameObjectWithTag("BackBtn").GetComponent<Button>();

        m_smsobj = SceneManagerScript.m_sm.gameObject;
        m_sms = m_smsobj.GetComponent<SceneManagerScript>();
        m_pmm = m_smsobj.GetComponentInChildren<PauseMenuManager>();

        m_inventoryManager = m_smsobj.GetComponentInChildren<InventoryManager>(true);
        Button[] btns = m_inventoryManager.gameObject.GetComponentsInChildren<Button>(true);
        m_useBtn = btns[0];
        m_cancelBtn = btns[btns.Length - 1];

        m_explinationText = m_smsobj.GetComponentInChildren<ExplinationText>(true);

        m_onCombatStart = FindObjectOfType<OnCombatStart>();

        m_turnOrderUIObj = GameObject.FindGameObjectWithTag("TurnOrder"); 
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

        foreach (Stats entity in m_turnOrder)
        {
            entity.GetHealthManager().SetUIHealthBars(); 
        }
    }
    /*void Start();*/

    /**/
    /*
     * Update()
     * NAME 
     *  Update - Update is called once per frame.
     * SYNOPSIS
     *  void Update()
     * DESCRIPTION
     *  This waits for the thread, which will run ChangeScenesWhenDialogEnds(), to end. When it does end, 
     *  the new scene is loaded and the user can open the pause menu again. Each frame, the player is set 
     *  to be unable to pause. 
     * RETURNS
     *  None
     */
    /**/
    void Update()
    {
        if (m_thread != null)
        {
            if (m_thread.ThreadState == ThreadState.Stopped)
            {
                m_pmm.SetCanPause(true);
                SceneManager.LoadScene(m_sceneID);
            }
        }
        m_pmm.SetCanPause(false);
    }
    /*void Update();*/

    /**/
    /*
     * SetListeners()
     * NAME
     *  SetListeners - sets the on click listeners for the buttons in combat.
     * SYNOPSIS
     *  void SetListeners()
     * DESCRIPTION 
     *  This funciton is called to initalize the buttons the user will need to click in order to participate
     *  in combat. 
     * RETURNS
     *  None
     */
    /**/
    public void SetListeners()
    {
        m_attackBtn.onClick.AddListener(OnAttackClick);
        m_inventoryBtn.onClick.AddListener(OnInventoryClick);
        m_passBtn.onClick.AddListener(OnPassClick);
        m_backBtn.onClick.AddListener(OnBackClick);
        m_useBtn.onClick.AddListener(OnUseClick);
        m_cancelBtn.onClick.AddListener(OnBackClick);
    }
    /*public void SetListeners();*/

    /**/
    /*
     * BuildInitialTurnOrder()
     * NAME 
     *  BuildInitialTurnOrder - inserts the initial values for m_turnOrder and m_turnOrderUI. 
     * SYNOPSIS
     *  void BuildInitialTurnOrder()
     * DESCRIPTION
     *  This funciton initally finds all entities that are allies and enemies separately and puts them in the 
     *  member arrays: m_players and m_enemies. These two arrays are combined into the m_turnOrder array. The text
     *  data from m_turnOrder is then placed in the m_turnOrderUI array that will later be used to show the user
     *  the turn order and which entity is currently able to act. The sight distance for the first enemy is also 
     *  set to be inactive to allow the user to be able to select him for a target. 
     * RETURNS 
     *  None
     */
    /**/
    private void BuildInitialTurnOrder()
    {
        m_players = SceneManagerScript.m_sm.transform.Find("Allies").GetComponentsInChildren<Stats>(true);
        m_enemies = SceneManagerScript.m_sm.transform.Find("Enemies").GetComponentsInChildren<Stats>(true);
        m_turnOrder = new Stats[m_players.Length + m_enemies.Length];

        m_enemies[0].GetComponentInChildren<CircleCollider2D>().gameObject.SetActive(false); 
        int index = 0;
        foreach (Stats player in m_players)
        {
            m_turnOrder[index] = player;
            index++;
        }
        foreach (Stats enemy in m_enemies)
        {
            m_turnOrder[index] = enemy;
            index++;
        }

        Text[] tmpTurnOrder = m_turnOrderUIObj.GetComponentsInChildren<Text>();
        m_turnOrderUI = new Text[tmpTurnOrder.Length - 1];
        for (int i = 0; i < m_turnOrderUI.Length; i++)
        {
            m_turnOrderUI[i] = tmpTurnOrder[i + 1];
        }
    }
    /*private void BuildInitialTurnOrder();*/

    /**/
    /*
     * InsertionSort()
     * NAME
     *  InsertionSort - performs insertion sort on an array of Stats.
     * SYNOPSIS
     *  void InsertionSort(Stats [] a_sortArray)
     *      a_sortArray --> the array to be sorted
     * DESCRIPTION
     *  This funciton sorts the elements in decending order by m_inititative using the insertion sort algorithm. 
     *  Although Quicksort has a faster average runtime, Insertion sort is faster in smaller sizes. The max size of
     *  the array should be 8. 
     * SOURCE
     *   https://www.geeksforgeeks.org/insertion-sort/
     * RETURNS 
     *  None
     */
    /**/
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
    /*private void InsertionSort(Stats [] a_sortArray);*/

    /**/
    /*
     * GenerateTurnOrder()
     * NAME
     *  GenerateTurnOrder - Generates the turn order at the start of combat.
     * SYNOPSIS
     *  void GenerateTurnOrder()
     * DESCRIPTION
     *  Each element in m_turnOrder generates a random value between 1 and 100. The elements then get sorted 
     *  based on the number generated in decending order. This is the order in which each entity will act. 
     * RETURNS
     *  None
     */
    /**/
    private void GenerateTurnOrder()
    {    
        for (int i = 0; i < m_turnOrder.Length; i++)
        {
            m_turnOrder[i].GenerateInitiative(); 
        }
        InsertionSort(m_turnOrder);
    }
    /*private void GenerateTurnOrder();*/

    /**/
    /*
     * BuildUIOrder()
     * NAME
     *  BuildUIOrder - builds the UI component that shows the turn order
     * SYNOPSIS
     *  void BuildUIOrder()
     * DESCRIPTION
     *  This funciton sets up the UI element that shows the user the turn order and who's turn it is. This is 
     *  symbolized by the entity's name and the number they rolled when determining the turn order. The entity
     *  whose turn it is gets a "<" to the right of the string. There is a maximum of 8 entities that can participate 
     *  in combat. This funciton makes sure that there are the same number of names in the UI element than there 
     *  are entities. 
     * RETURNS
     *  None
     */
    /**/
    private void BuildUIOrder()
    {
        for(int i = 0; i < m_turnOrder.Length; ++i)
        {
            m_turnOrderUI[i].text = m_turnOrder[i].m_entityName + ": " + m_turnOrder[i].m_initiative;
            m_turnOrderUI[i].gameObject.SetActive(true);     
        }
        m_turnOrderUI[m_currentTurn].GetComponent<Text>().text = m_turnOrder[m_currentTurn].m_entityName + ": " + m_turnOrder[m_currentTurn].m_initiative + "<";
    }
    /*private void BuildUIOrder();*/

    /**/
    /*
     * PlayerWins()
     * NAME
     *  PlayerWins - returns true if all enemies have been defeated.
     * SYNOPSIS
     *  bool PlayerWins()
     * DESCRIPTION
     *  This function searches through all of the entities in combat. If the entity is an enemy and is not defeated,
     *  the player has not won yet. If all of the enemies have been defeated, this returns true and the player has
     *  won the battle. 
     * RETURNS
     *  True if all enemies have been defeated.
     *  False if at least one enemy is still alive. 
     */
    /**/
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
    /*private bool PlayerWins();*/

    /**/
    /*
     * EnemyWins()
     * NAME 
     *  EnemyWins - returns true if all friendly entities are defeated.
     * SYNOPSIS
     *  bool EnemyWins()
     * DESCRIPTION
     *  This function searches through each entity in combat. If an entity that is not an enemy is still alive,
     *  the enemy has not won. If all entities that are not enemies have been defeated, this returns true. 
     * RETURNS
     *  True if all friendly players have been defeated.
     *  False if there is at least one friendly player still alive. 
     */
    /**/
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
    /*private bool EnemyWins();*/

    /**/
    /*
     * NextTurn()
     * NAME
     *  NextTurn - performs the actions needed to move on to the next turn of combat.
     * SYNOPIS
     *  void NextTurn()
     * DESCRIPTION
     *  This function first checks if any of the teams have reached victory conditions. If so, either OnPlayerWin or
     *  OnEnemyWin will be called. Otherwise, the turn will move on to the next entity in m_turnOrder that is still 
     *  alive and the turn order interface will also be updated. This also makes the menu that allows the player to 
     *  participate in combat disappear if it is now the enemy's turn or reappear if it is an ally's turn. 
     * RETURNS
     *  None
     */
    /**/
    public void NextTurn()
    {
        //m_inventoryManager.UpdateUIInventory(); 
        if (PlayerWins())
        {
            OnPlayerWin();
            return; 
        }
        if (EnemyWins())
        {
            OnEnemyWin();
            return; 
        }
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
    /*public void NextTurn();*/

    /**/
    /*
     * BasicAttack()
     * NAME
     *  BasicAttack - perform an attack without an item.
     * SYNOPSIS
     *  void BasicAttack(Stats a_target)
     *      a_target --> the entity that will be hurt. 
     * DESCRIPTION
     *  The entity whose turn it is attacks the target for 5 base damage. After damage is done, the turn ends.
     * RETURNS
     *  None
     */
    /**/
    public void BasicAttack(Stats a_target)
    {
        Stats user = m_turnOrder[m_currentTurn]; 
        // Basic Attack should be realatively weak 
        int damageToDo = CalculateDamage(user, a_target, 5);
        m_explinationText.SetMessage(user.m_entityName + " used a basic attack on " + a_target.m_entityName);
        a_target.GetHealthManager().DealDamage(damageToDo);
        OnDefeat(a_target);
        // Performing this aciton ends the turn. 
        NextTurn(); 
    }
    /*public void BasicAttack(Stats a_target);*/

    /**/
    /*
     * UseWeapon()
     * NAME
     *  UseWeapon - the target is attacked with a weapon.
     * SYNOPSIS
     *  void UseWeapon(Stats a_target, Weapons a_item)
     *      a_target --> the entity that will be attacked.
     *      a_item --> the weapon that will be used against the target.
     * DESCRIPTION
     *  The current entity uses a weapon on the selected target. If the weapon has no durability left, an error 
     *  message is displayed because this should not happen under normal circumstances. If the weapon passes the 
     *  error check, a random number is generated between 1 and 100. If the randomly generated number is greater than 
     *  the item's accuracy, the attack misses and combat moves on to the next turn. Otherwise, the attack hits, damage 
     *  is calculated and deducted from the target, the item loses one durability and the current entity's turn ends. 
     * RETURNS
     *  None
     */
    /**/
    public void UseWeapon(Stats a_target, Weapons a_item)
    {
        if(a_item.GetDurability() <= 0)
        {
            m_explinationText.SetMessage("Error: This item has no durability left.");
            return;
        }
        // miss
        if(UnityEngine.Random.Range(1,101) > a_item.GetAccuracy())
        {
            m_explinationText.SetMessage(m_turnOrder[m_currentTurn].m_entityName + "'s attack missed!");
            NextTurn();
            return; 
        }
        //hit
        else
        {
            m_explinationText.SetMessage(m_turnOrder[m_currentTurn].m_entityName + " attacked " + a_target.m_entityName + " with the " + a_item.GetName());
            int damageToDo = CalculateDamage(m_turnOrder[m_currentTurn], a_target, a_item.GetDamage());
            a_target.GetHealthManager().DealDamage(damageToDo);
            a_item.RemoveDurability();
            OnDefeat(a_target); 
            NextTurn();
            return; 
        }

    }
    /*public void UseWeapon(Stats a_target, Weapons a_item);*/

    /**/
    /*
     * UsePotion()
     * NAME 
     *  UsePotion - the current entiy uses a healing potion on a target. 
     * SYNOPSIS
     *  void UsePotion(Stats a_target, Potions a_item)
     *      a_target --> the person that will be healed.
     *      a_item --> the item that will be used.
     * DESCRIPTION
     *  If the entity is somehow able to use an item with no durability left, an error message is displayed since 
     *  this should not happen under normal circumstances. The target is healed by an amount given by the item data. 
     *  The item loses one durabilty and combat moves on to the next turn. 
     * RETURNS 
     *  None
     */
    /**/
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
    /*public void UsePotion(Stats a_target, Potions a_item);*/

    /**/
    /*
     * CalculateDamage()
     * NAME
     *  CalculateDamage - the amount of damage the target will take is calculated.
     * SYNOPSIS
     *  int CalculateDamage(Stats a_user, Stats a_target, int a_baseDamage)
     *      a_user --> the entity using the attack.
     *      a_target --> the entity that will be attacked.
     *      a_baseDamage --> how powerful the attack is without accounting for attack and defense.
     * DESCRIPTION
     *  The damage is calculated by taking the base damage, adding the attack stat of the user and subtracting 
     *  the defense stat of the target. Since a target with high defense can cause this number to become negative, 
     *  the damage number is set to 1 if the damage calculation becomes 0 or less. 
     * RETURNS
     *  The amount of damage the target will take.
     */
    /**/
    public int CalculateDamage(Stats a_user, Stats a_target, int a_baseDamage)
    {
        int damage = a_baseDamage + a_user.m_atk - a_target.m_def;
        // Prevents the damage value from being negative
        if (damage < 1)
            damage = 1;
        return damage; 
    }
    /*public int CalculateDamage(Stats a_user, Stats a_target, int a_baseDamage);*/

    // Perform action when someone loses all HP. 
    /**/
    /*
     * OnDefeat()
     * NAME
     *  OnDefeat - is called when an entity takes damage. 
     * SYNOPSIS
     *  void OnDefeat(Stats a_target)
     *      a_target --> the entity that has taken damage.
     * DESCRIPTION
     *  If the target is still alive, this returns. Otherwise, a message is sent to the player that the target was 
     *  defeated. If the target was an enemy, all of the players will gain experience. 
     * RETURNS
     *  None
     */
    /**/
    private void OnDefeat(Stats a_target)
    {
        // Return if the target is still alive
        if (a_target.GetHealthManager().GetCurrentHealth() > 0)
        {
            return;
        }
        m_explinationText.SetMessage(a_target.m_entityName + " has been defeated!");
        // Give players experience
        if (a_target.m_isEnemy)
        {
            foreach (Stats player in m_players)
            {
                if (player.GetHealthManager().GetCurrentHealth() > 0)
                {
                    int experienceToGive = a_target.GetBaseExp() * a_target.m_level / player.m_level;
                    if (experienceToGive <= 0)
                    {
                        experienceToGive = 1;
                    }
                    m_explinationText.SetMessage(player.m_entityName + " gained " + experienceToGive + " experience.");
                    player.GainExp(experienceToGive, m_explinationText);
                }
            }
        }
    }
    /*private void OnDefeat(Stats a_target);*/

    /**/
    /*
     * OnAttackClick()
     * NAME
     *  OnAttackClick - is called when user clicks the attack button.
     * SYNOPSIS
     *  void OnAttackClick()
     * DESCRIPTION
     *  When the attack button is clicked, the turn menu disappears and the user can select a target to attack. 
     *  They are also able to click the back button to go back to the turn menu.
     * RETURNS
     *  None
     */
    /**/
    private void OnAttackClick()
    {
        m_attackClicked = true;
        m_turnMenu.SetActive(false);
        m_backBtn.gameObject.SetActive(true);
    }
    /*private void OnAttackClick();*/

    /**/
    /*
     * OnInventoryClick()
     * NAME
     *  OnInventoryClick - is called when user clicks the inventory button.
     * SYNOPSIS
     *  void OnInventoryClick()
     * DESCRIPTION
     *  When the inventory button is clicked, the inventory is shown and the turn menu and the turn order interface
     *  are set to not be active. 
     * RETURNS
     *  None
     */
    /**/
    private void OnInventoryClick()
    {
        m_turnMenu.SetActive(false);
        m_turnOrderUIObj.SetActive(false);
        m_inventoryManager.gameObject.SetActive(true);
    }
    /*private void OnInventoryClick();*/

    /**/
    /*
     * void OnPassClick()
     * NAME
     *  OnPassClick - is called when user clicks the pass button.
     * SYNOPSIS
     *  void OnPassClick()
     * DESCRIPTION
     *  When the user clicks the pass button, the turn is passed on to the next person in combat. 
     * RETURNS
     *  None
     */
    /**/
    private void OnPassClick()
    {
        NextTurn();
    }
    /*private void OnPassClick();*/

    /**/
    /*
     * OnBackClick()
     * NAME
     *  OnBackClick - is called when user clicks the back button.
     * SYNOPSIS
     *  void OnBackClick()
     * DESCRIPTION
     *  When the user clicks the back button, the combat state is restored to before they selected anything from the 
     *  turn menu. 
     * RETURNS
     *  None
     */
    /**/
    private void OnBackClick()
    {
        m_attackClicked = false;
        m_itemClicked = false;
        m_turnMenu.SetActive(true);
        m_turnOrderUIObj.SetActive(true);
        m_inventoryManager.gameObject.SetActive(false);
        m_backBtn.gameObject.SetActive(false);
    }
    /*private void OnBackClick();*/

    /**/
    /*
     * OnUseClick()
     * NAME
     *  OnUseClick - is called when user clicks on use button. 
     * SYNOPSIS
     *  void OnUseClick()
     * DESCRIPTION
     *  When the user clicks on the use button, the inventory menu disappears, the turn order interface is set to be 
     *  active again and the user is able to target an entity to use the selected item. 
     * RETURNS
     *  None
     */
    /**/
    private void OnUseClick()
    {
        m_itemClicked = true;
        m_inventoryManager.gameObject.SetActive(false);
        m_turnMenu.SetActive(false);
        m_turnOrderUIObj.SetActive(true);
        m_backBtn.gameObject.SetActive(true);
    }
    /*private void OnUseClick();*/

    /**/
    /*
     *  OnPlayerWin()
     * NAME
     *  OnPlayerWin - performs actions when the player wins. 
     * SYNOPSIS
     *  void OnPlayerWin()
     * DESCRIPTION
     *  When the player wins, the player gets prize money. All players that were defeated are set to be active again. 
     *  After all the explination text is seen by the user, the allied characters are sent back to the previous scene 
     *  and are able to move again.
     * RETURNS
     *  None
     */
    /**/
    private void OnPlayerWin()
    {
        //Calculate prize money. Player should get more money the more powerful the foe is. 
        int prizeMoney;
        int totalEnemyLevel = 0;
        foreach (Stats enemy in m_enemies)
        {
            totalEnemyLevel += enemy.m_level;
            enemy.NoLongerTurn();
        }
        prizeMoney = 10 * totalEnemyLevel;
        m_inventoryManager.GetPlayerInventory().RecieveCurrency(prizeMoney);
        m_explinationText.SetMessage("You recieved $" + prizeMoney + " for winning.");
        foreach (Stats player in m_players)
        {
            PlayerController pc = player.gameObject.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.SetCanMove(true);
            }
            FollowerController fc = player.gameObject.GetComponent<FollowerController>();
            if (fc != null)
            {
                fc.SetCanMove(true);
            }
            player.gameObject.SetActive(true);
            player.NoLongerTurn();
        }
        m_sceneID = m_sms.GetLastSceneID();
        m_thread = new Thread(ChangeScenesWhenDialogEnds);
        m_thread.Start();
    }
    /*private void OnPlayerWin();*/

    /**/
    /*
     * OnEnemyWin()
     * NAME
     *  OnEnemyWin - performs acitons when the enemy wins.
     * SYNOPSIS
     *  void OnEnemyWin()
     * DESCRIPTION
     *  When the enemy wins, everyone's health is reset and the player is sent back to the overworld when the 
     *  user is done reading the explination text. A "Game Over" message is also shown to the user. 
     * RETURNS
     *  None
     */
    /**/
    private void OnEnemyWin()
    {
        m_explinationText.SetMessage("Game Over!");

        //Reset health
        foreach (Stats player in m_turnOrder)
        {
            PlayerController pc = player.gameObject.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.SetCanMove(true);
            }
            FollowerController fc = player.gameObject.GetComponent<FollowerController>();
            if (fc != null)
            {
                fc.SetCanMove(true);
            }
            player.gameObject.SetActive(true);
            player.GetHealthManager().FullyHeal();
            player.NoLongerTurn(); 
        }
        m_enemies[0].GetComponentInChildren<CircleCollider2D>(true).gameObject.SetActive(true);
        m_sceneID = 0;
        m_thread = new Thread(ChangeScenesWhenDialogEnds);
        m_thread.Start();
    }
    /*private void OnEnemyWin();*/

    /**/
    /*
     * ChangeScenesWhenDialogEnds()
     * NAME
     *  ChangeScenesWhenDialogEnds - waits for dialog to end before changing scenes.
     * SYNOPSIS
     *  void ChangeScenesWhenDialogEnds()
     * DESCRIPTION
     *  This will be called as a thread that waits for there to be no more remaining messages in m_awaitingMessages
     *  in the ExplinaitonText class. 
     * RETURNS
     *  None
     */
    /**/
    private void ChangeScenesWhenDialogEnds()
    {
        while (m_explinationText.GetAwaitingMessages().Count != 0)
        {
        }
    }
    /*private void ChangeScenesWhenDialogEnds();*/
    /**/
    /*
     * GetTurnOrder()
     * NAME
     *  GetTurnOrder - Accessor for m_turnOrder.
     * SYNOPSIS
     *  Stats [] GetTurnOrder()
     * DESCRIPTION
     *  Returns the list of entities in the order they act. 
     * RETURNS
     *  m_turnOrder
     */
    /**/
    public Stats [] GetTurnOrder()
    {
        return m_turnOrder;
    }
    /*public Stats [] GetTurnOrder();*/

    /**/
    /*
     * GetCurrentTurn()
     * NAME
     *  GetCurrentTurn - accessor for m_currentTurn. 
     * SYNOPSIS
     *  int GetCurrentTurn()
     * DESCRIPTION
     *  Returns the index of the entity whose turn it is. 
     * RETURNS
     *  m_currentTurn
     */
    /**/
    public int GetCurrentTurn()
    {
        return m_currentTurn;
    }
    /*public int GetCurrentTurn();*/

    /**/
    /*
     * GetTurnMenu()
     * NAME
     *  GetTurnMenu - accessor for m_turnMenu.
     * SYNOPSIS
     *  GameObject GetTurnMenu()
     * DESCRIPTION
     *  Returns the menu that the player uses to participate in combat. 
     * RETURNS
     *  m_turnMenu
     */
    /**/
    public GameObject GetTurnMenu()
    {
        return m_turnMenu;
    }
    /*public GameObject GetTurnMenu();*/

    /**/
    /*
     * GetAttackClicked()
     * NAME
     *  GetAttackClicked - accessor for m_attackClicked. 
     * SYNOPSIS
     *  bool GetAttackClicked()
     * DESCRIPTION
     *  Returns true if the user cliked the attack button and false if they did not. 
     * RETURNS
     *  m_attackClicked
     */
    /**/
    public bool GetAttackClicked()
    {
        return m_attackClicked;
    }
    /*public bool GetAttackClicked();*/

    /**/
    /*
     * GetItemClicked()
     * NAME
     *  GetItemClicked - accessor for m_itemClicked.
     * SYNOPSIS
     *  bool GetItemClicked()
     * DESCRIPTION
     *  Returns true if the user clicked on the item button, false if they did not. 
     * RETURNS
     *  m_itemClicked
     */
    /**/
    public bool GetItemClicked()
    {
        return m_itemClicked;
    }
    /*public bool GetItemClicked();*/

    /**/
    /*
     * GetInventoryManager()
     * NAME
     *  GetInventoryManager - accessor for m_inventoryManager. 
     * SYNOPSIS
     *  InventoryManager GetInventoryManager()
     * DESCRIPTION
     *  Returns the InventoryManager class. 
     * RETURNS
     *  m_inventoryManager
     */
    /**/
    public InventoryManager GetInventoryManager()
    {
        return m_inventoryManager; 
    }
    /*public InventoryManager GetInventoryManager();*/

    /**/
    /*
     * SetAttackClicked()
     * NAME
     *  SetAttackClicked - setter for m_attackClicked. 
     * SYNOPSIS
     *  void SetAttackClicked(bool a_input)
     *      a_input --> the value m_attackClicked will be set to. 
     * DESCRIPTION
     *  Sets the value of m_attackClicked to a_input.  
     * RETURNS
     *  None
     */
    /**/
    public void SetAttackClicked(bool a_input)
    {
        m_attackClicked = a_input;
    }
    /*public void SetAttackClicked(bool a_input);*/

    /**/
    /*
     * SetItemClicked()
     * NAME
     *  SetItemClicked - setter for m_itemClicked.
     * SYNOPSIS
     *  void SetItemClicked(bool a_input)
     *      a_input --> the value m_itemClicked will be set to. 
     * DESCRIPTION
     *  Sets the value of m_itemClicked to a_input. 
     * RETURNS
     *  None
     */
    /**/
    public void SetItemClicked(bool a_input)
    {
        m_itemClicked = a_input;
    }
    /*public void SetItemClicked(bool a_input);*/

    
}
