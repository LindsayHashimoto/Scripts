/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void quicksort(Stats[] array, int low, int high)
    {
        int pivot;
        if (low < high)
        {
            pivot = partition(array, low, high);
            quicksort(array, low, pivot - 1);
            quicksort(array, low + pivot, high);
        }
    }
    public int partition(Stats[] array, int low, int high)
    {
        int pivot = array[high].initiative;

        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
            if (array[j].initiative <= pivot)
            {
                i++;
                swap(array, i, j);
            }
        }
        swap(array, (i + 1), high);
        return (i + 1);
    }
    public void swap(Stats[] array, int index1, int index2)
    {
        Stats tmp = array[index1];
        array[index1] = array[index2];
        array[index2] = tmp;
    }
}*/
