using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

    private PlayerController thePlayer;
    private FollowerController theFollower;
    private CameraControl theCamera; 

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        thePlayer.transform.position = transform.position;

        theFollower = FindObjectOfType<FollowerController>();
        theFollower.transform.position = transform.position; 

        theCamera = FindObjectOfType<CameraControl>();
        theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
