using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStartPos : MonoBehaviour {

    private GameObject m_objectToMove;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  At the start of combat, this funciton moves the entities to their starting combat positions, faces them in the 
     *  correct direction and prevents them from moving around. 
     * RETURNS
     *  None
     */
    /**/
    void Start ()
    {
        GameObject [] objectsToMove = GameObject.FindGameObjectsWithTag(this.tag); 
        foreach(GameObject obj in objectsToMove)
        {
            if (obj.GetComponent<Stats>() != null)
            {
                m_objectToMove = obj;
                PlayerController pCon = obj.GetComponent<PlayerController>();
                if(pCon != null)
                {
                    pCon.GetAnim().SetFloat("LastMoveX", 1f);
                    pCon.GetAnim().SetFloat("LastMoveY", 0f);
                    pCon.GetAnim().SetBool("PlayerMoving", false);
                    pCon.SetCanMove(false); 
                    
                }
                FollowerController fCon = obj.GetComponent<FollowerController>();
                if (fCon != null)
                {
                    fCon.GetAnim().SetBool("PlayerMoving", false);
                    fCon.GetAnim().SetFloat("LastMoveX", 1f);
                    fCon.GetAnim().SetFloat("LastMoveY", 0f);
                    fCon.SetCanMove(false);
                }
                EnemyBehavior eCon = obj.GetComponent<EnemyBehavior>();
                if (eCon != null)
                {
                    eCon.GetAnim().SetBool("PlayerMoving", false);
                    eCon.GetAnim().SetFloat("LastMoveX", -1f);
                    eCon.GetAnim().SetFloat("LastMoveY", 0f);
                    eCon.SetCanMove(false);
                }
                m_objectToMove.transform.position = this.transform.position;
            }
        }
	}
    /*void Start();*/
}
