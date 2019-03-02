using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularLinkedList : MonoBehaviour {
 
    public Node start = null;
    public Node current = null;
    public Node end = null; 
    
    public void addNodeToEnd(Stats data)
    {
        //if list is empty
        if (start == null)
        {
            start = new Node(data, start);
            end = start;
            current = start; 
        }
        else {
            end.next = new Node(data, start);
            end = end.next; 
        }
    }

	public void deleteNode(Node toDelete)
    {
        Node tmp = start;
        while(tmp.next != toDelete)
        {
            tmp = tmp.next;
            //if the toDelete node was not found
            if (tmp == start)
            {
                print("Node not found! No node was deleted!"); 
                return; 
            } 
        }
        tmp.next = toDelete.next;
        toDelete = null; 
    }
}
