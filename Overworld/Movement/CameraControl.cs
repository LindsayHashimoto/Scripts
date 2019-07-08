using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    private GameObject m_followTarget;
    private Vector3 m_targetPos;
    private float m_moveSpeed;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  Sets the value of m_moveSpeed and sets m_followTarget to be the player. 
     * RETURNS
     *  None
     */
    /**/
    void Start ()
    {
        m_moveSpeed = 7;
        m_followTarget = FindObjectOfType<PlayerController>().gameObject;
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
     *  This moves the camera to where the player is. 
     * RETURNS
     *  None
     */
    /**/
    void Update ()
    {
        m_targetPos = new Vector3(m_followTarget.transform.position.x, m_followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, m_targetPos, m_moveSpeed * Time.deltaTime); 
	}
    /*void Update();*/
}
