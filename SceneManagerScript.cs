using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneManagerScript : MonoBehaviour {

    public static SceneManagerScript m_sm; 
    DontDestroy[] m_dontDestroyObjs;
    private int m_currentScene; 
    private int m_lastScene;

    //https://answers.unity.com/questions/1072572/accessing-variables-on-a-script-on-a-dontdestroyon.html
    void Awake()
    {
        MakeThisTheOnlyGameObj();
    }


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
    // Use this for initialization
    void Start ()
    {
        SceneManager.LoadScene("a"); 
        SceneManager.activeSceneChanged += OnSceneChange;
        m_dontDestroyObjs = FindObjectsOfType<DontDestroy>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetLastSceneID(int a_lastScene)
    {
        m_lastScene = a_lastScene; 
    }

    public int GetLastSceneID()
    {
        return m_lastScene; 
    }

    private void OnSceneChange(Scene a_current, Scene a_next)
    {
        m_lastScene = m_currentScene;
        m_currentScene = a_next.buildIndex;
        foreach (DontDestroy objs in m_dontDestroyObjs)
        {
            objs.SetDontDestroy(true);
        }
    }
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

}
