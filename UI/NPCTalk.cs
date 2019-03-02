using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : MonoBehaviour {

    public string dialogue;
    private DialogueManager dm;
    
	// Use this for initialization
	void Start () {
        dm = FindObjectOfType<DialogueManager>(); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                dm.showBox(dialogue);
            }
        }
    }
}
