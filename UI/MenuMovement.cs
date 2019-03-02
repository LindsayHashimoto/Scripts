using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMovement : MonoBehaviour {

    
    public int selected;
    public int numNodes;
    public Text[] nodes;
    public bool isActive; 
    // Use this for initialization
    void Start () {
        selected = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.W) && selected > 0)
        {
            if (isActive)
            {
                nodes[selected].text = nodes[selected].text.Substring(1);
                selected--;
                nodes[selected].text = ">" + nodes[selected].text;
            }
        }
        if (Input.GetKeyUp(KeyCode.S) && selected < (numNodes - 1))
        {
            if (isActive)
            {
                nodes[selected].text = nodes[selected].text.Substring(1);
                selected++;
                nodes[selected].text = ">" + nodes[selected].text;
            }
        }
    }
}
