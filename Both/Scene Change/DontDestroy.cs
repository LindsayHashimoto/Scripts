using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

    private bool m_dontDestroy;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization.
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  On start, m_dontDestroy is initally set to false and this GameObject is not destroyed on scene change. 
     * RETURNS
     *  None
     */
    /**/
    void Start ()
    {
        m_dontDestroy = false; 
        DontDestroyOnLoad(this.gameObject);
	}
    /*void Start();*/

    /**/
    /*
     * SetDontdestroy()
     * NAME
     *  SetDontDestroy - setter for m_dontDestory.
     * SYNOPSIS
     *  void SetDontDestroy(bool a_dontDestroy)
     *      a_dontDestroy --> the value that m_dontDestory will be set to. 
     * DESCRIPTION
     *  m_dontDestroy is set to the value of a_dontDestroy. 
     * RETURNS
     *  None
     */
    /**/
    public void SetDontDestroy(bool a_dontDestroy)
    {
        m_dontDestroy = a_dontDestroy; 
    }

    /**/
    /*
     * GetDontDestroy()
     * NAME
     *  GetDontDestroy - accessor for m_dontDestory.
     * SYNOPSIS
     *  bool GetDontDestroy()
     * DESCRIPTION
     *  Returns the value of m_dontDestroy. 
     * RETURNS
     *  m_dontDestroy
     */
    /**/
    public bool GetDontDestroy()
    {
        return m_dontDestroy; 
    }
}
