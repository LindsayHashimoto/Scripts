
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

    private PlayerController m_player;
    private FollowerController [] m_followers;
    private CameraControl m_camera;

    /**/
    /*
     * Start()
     * NAME
     *  Start - Use this for initialization.
     * SYNOPSIS
     *  void Start()
     * DESCRIPTION
     *  The position of this game object should be where the player spawns when the scene loads. This moves the 
     *  player, the followers and the camera to the positon of this GameObject on start. 
     * RETURNS
     *  None
     */
    /**/
    void Start () {
        m_player = FindObjectOfType<PlayerController>();
        m_player.transform.position = transform.position;

        m_followers = FindObjectsOfType<FollowerController>();
        foreach (FollowerController follower in m_followers)
        {
            follower.transform.position = transform.position;
        }

        m_camera = FindObjectOfType<CameraControl>();
        m_camera.transform.position = new Vector3(transform.position.x, transform.position.y, m_camera.transform.position.z);
	}
    /*void Start();*/
}
