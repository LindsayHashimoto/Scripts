using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

    private PlayerController m_player;
    private FollowerController [] m_followers;
    private CameraControl m_camera; 

	// Use this for initialization
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
	
	// Update is called once per frame
	void Update () {
		
	}
}
