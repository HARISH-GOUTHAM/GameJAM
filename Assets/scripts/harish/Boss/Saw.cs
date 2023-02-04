using System;
using System.Collections;
using System.Collections.Generic;
using harish.Player;
using UnityEngine;


public class Saw : MonoBehaviour
{
   
    [SerializeField] private float damage = 10;

    [SerializeField] private float damageRadius = 2;
    [SerializeField] private float attackDelay = 3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isCollidingWithPlayer() && CanDamage())
        {
            Debug.Log("cpo");
            PlayerData.instance.health -= damage;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,damageRadius);
    }

    private float attackTime = 0;
    bool CanDamage()
    {
        if (Time.time - attackTime > attackDelay)
        {
            attackTime = Time.time;
            return true;
        }

        return false;
    }
    
    bool isCollidingWithPlayer()
    {
        if (Vector3.Distance(PlayerData.instance.transform.position, this.transform.position) < damageRadius)
        {
            return true;
        }

        return false;
    }
    
   
}
