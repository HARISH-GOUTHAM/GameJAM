using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSuck : MonoBehaviour
{

    [SerializeField] private Transform playerCam;

    [SerializeField] private float range = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SuckEnemy();
    }
    
    void SuckEnemy()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, range))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                if (hit.transform.GetComponent<EnemyController>().state == EnemyState.dead)
                {
                }
            }
        }
    }
    
}
