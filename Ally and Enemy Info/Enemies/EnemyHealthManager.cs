using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{

    public int myMaxHealth;
    public int myCurrentHealth;

    // Use this for initialization
    void Start()
    {
        myCurrentHealth = myMaxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (myCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void dealDamage(int damage)
    {
        myCurrentHealth -= damage;
    }
}
