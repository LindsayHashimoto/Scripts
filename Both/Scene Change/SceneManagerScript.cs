using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneManagerScript : MonoBehaviour {

    public static SceneManagerScript m_sm; 
    DontDestroy[] m_dontDestroyObjs;
    private int m_currentScene; 
    private int m_lastScene;

    /**/
    /*
     * Awake()
     * NAME
     *  Awake - perform action before the game starts
     * SYNOPSIS
     *  void Awake()
     * DESCRIPTION
     *  When this is called, MakeThisTheOnlyGameObj is called which makes sure the static memeber value m_sm is set and there
     *  are no duplicates of it. 
     * RETURNS
     *  None
     */ 
    /**/
    void Awake()
    {
        MakeThisTheOnlyGameObj();
    }
    /*void Awake();*/

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  This initially loads the first scene called "a". This also makes the two funcitons OnSceneChange and OnSceneLoaded be called
     *  when the active scene changes and when a new scene is loaded respectively. The values for m_dontDestroyObjs are also set.  
     * RETURNS
     *  None
     */
    /**/
    void Start ()
    {
        SceneManager.LoadScene("a"); 
        SceneManager.activeSceneChanged += OnSceneChange;
        m_dontDestroyObjs = FindObjectsOfType<DontDestroy>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    /*void Start();*/

    /**/
    /*
     * MakeThisTheOnlyGameObj()
     * NAME
     *  MakeThisTheOnlyGameObj - makes sure this object is not destroyed when switching scenes and no new game objects are created
     * SYNOPSIS
     *  void MakeThisTheOnlyGameObj()
     * DESCRIPTION
     *  m_sm is an important static value that is needed to access dont destroy on load objects in other scenes. This function 
     *  makes sure that this value is set to this class and there are no clones of it. 
     * RETURNS
     *  None
     * SOURCE 
     *  https://answers.unity.com/questions/1072572/accessing-variables-on-a-script-on-a-dontdestroyon.html
     */
    /**/
    void MakeThisTheOnlyGameObj()
    {
        if (m_sm == null)
        {
            DontDestroyOnLoad(gameObject);
            m_sm = this;
        }
        else
        {
            if (m_sm != this)
            {
                Destroy(gameObject);
            }
        }
    }
    /*void MakeThisTheOnlyGameObj();*/

    /**/
    /*
     * OnSceneChange()
     * NAME
     *  OnSceneChange - saves the current and next scene and makes sure DontDestroy objects are not destroyed.
     * SYNOPSIS
     *  void OnSceneChange(Scene a_current, Scene a_next)
     *      a_current --> the current scene
     *      a_next --> the scene that will be loaded
     * DESCRIPTION
     *  When the scene changes, this funciton saves the current and next scene as the member values m_lastScene and m_currentScene. 
     *  The names are changed to last and current because when they are refered to again, the scene would have already changed to 
     *  the next scene. This also sets the original DontDestroy objects' member value m_dontDestroy to be true so that they can be 
     *  differentiated from the clones that are made when switching scenes.  
     * RETURNS
     *  None.
     */
    /**/
    private void OnSceneChange(Scene a_current, Scene a_next)
    {
        m_lastScene = m_currentScene;
        m_currentScene = a_next.buildIndex;
        foreach (DontDestroy objs in m_dontDestroyObjs)
        {
            objs.SetDontDestroy(true);
        }
    }
    /*private void OnSceneChange(Scene a_current, Scene a_next);*/

    /**/
    /*
     * OnSceneLoaded()
     * NAME
     *  OnSceneLoaded - when the scene is loaded, manage the dont destroy objects
     * SYNOPSIS
     *  void OnSceneLoaded(Scene a_scene, LoadSceneMode a_mode)
     *      a_scene, a_mode --> these two values are not used in the funciton but are needed in order for this funciton to be called 
     *      on scene load. 
     * DESCRIPTION
     *  This funciton finds all of the objects that contain the class "DontDestroy". The original object should have the member value
     *  m_dontDestroy set to true and all copies made when switching scenes have it set to false. This funciton destroys all objects
     *  that are excess copies. 
     * RETURNS
     *  None.
     */
    /**/
    private void OnSceneLoaded(Scene a_scene, LoadSceneMode a_mode)
    {
        m_dontDestroyObjs = FindObjectsOfType<DontDestroy>();
        foreach (DontDestroy objs in m_dontDestroyObjs)
        {
            if (!objs.GetDontDestroy())
            {
                Destroy(objs.gameObject);
            }
        }
    }
    /*private void OnSceneLoaded(Scene a_scene, LoadSceneMode a_mode);*/

    /**/
    /*
     * SetLastSceneID()
     * NAME
     *  SetLstSceneID - setter for the member value m_lastScene
     * SYNOPSIS
     *  void SetLastSceneID(int a_lastScene)
     *      a_lastScene --> the value that m_lastScene will be set to
     * DESCRIPTION
     *  This is used to change the value of the private member value m_lastScene. 
     * RETURNS 
     *  None     
     */
    /**/
    public void SetLastSceneID(int a_lastScene)
    {
        m_lastScene = a_lastScene; 
    }
    /*public void SetLastSceneID(int a_lastScene);*/

    /**/
    /*
     * GetLastSceneID()
     * NAME
     *  GetLastSceneID - accessor for the private member value m_lastScene
     * SYNOPSIS
     *  int GetLastSceneID()
     * DESCRIPTION
     *  This is used to access the value of the private member value m_lastScene. 
     * RETURNS
     *  The private member value m_lastScene
     */
    /**/
    public int GetLastSceneID()
    {
        return m_lastScene; 
    }
    /*public int GetLastSceneID()*/

    

}
