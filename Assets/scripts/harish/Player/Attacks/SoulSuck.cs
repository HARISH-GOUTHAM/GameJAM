using System.Collections;
using System.Collections.Generic;
using harish.Enemy;
using harish.Player;
using UnityEngine;

public class SoulSuck : MonoBehaviour
{

    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform rootPoint;

    [SerializeField] private float range = 1f;

    [SerializeField] private float attackDelay = 3f;

    private bool isSucking = false;
    void Start()
    {
        PlayerData.instance.inputManger.OnSuckEv += PerformSuckSoul;
    }

    // Update is called once per frame
    void Update()
    {
        
        SuckEnemy();
    }

    void SuckEnemy()
    {
        if (isSucking)
        {
            en.spine.position = rootPoint.position;
            en.spine.rotation = rootPoint.rotation;
        }
    }

    private float attackTime = 0;
    void PerformSuckSoul()
    {
        if (Time.time - attackTime > attackDelay)
        {
            SuckSoul();
            attackTime = Time.time;
        }   
        
    }

    private DeadEnemy en;
    void SuckSoul()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, range))
        {
            
            Debug.Log("hit");
            if (hit.collider.gameObject.CompareTag("Dead Enemy"))
            {
                
                Debug.Log("Sucking");

                en = hit.transform.root.GetComponent<DeadEnemy>();
               
                en.canSuck = false;
                en.spine.transform.GetComponent<Rigidbody>().isKinematic = true;
                isSucking = true;
                Invoke(nameof(StopSucking),2f);
            }
        }
    }
    
    void StopSucking()
    {
        isSucking = false;
        en.spine.GetComponent<Rigidbody>().isKinematic = false;
    }
    
}
