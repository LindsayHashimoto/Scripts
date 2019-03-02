using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public Stats data;
    public Node next; 

    public Node(Stats a_data, Node a_next)
    {
        data = a_data;
        next = a_next; 
    }

}
