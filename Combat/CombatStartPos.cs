using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStartPos : MonoBehaviour {

    private GameObject m_objectToMove; 
	// Use this for initialization
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
                    pCon.GetAnim().SetFloat("MoveX", 1f);
                    pCon.GetAnim().SetFloat("MoveY", 0f);
                    pCon.GetAnim().SetBool("PlayerMoving", true);
                    pCon.SetCanMove(false); 
                    
                }
                FollowerController fCon = obj.GetComponent<FollowerController>();
                if (fCon != null)
                {
                    fCon.GetAnim().SetFloat("MoveX", 1f);
                    fCon.GetAnim().SetFloat("MoveY", 0f);
                    fCon.GetAnim().SetBool("PlayerMoving", true);
                    fCon.SetCanMove(false);
                    
                }
                m_objectToMove.transform.position = this.transform.position;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
