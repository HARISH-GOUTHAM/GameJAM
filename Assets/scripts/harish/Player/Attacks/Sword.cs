using System.Collections;
using System.Collections.Generic;
using harish.Player;
using UnityEngine;
using UnityEngine.VFX;

public class Sword : MonoBehaviour
{
    [SerializeField] private Transform PlayerCam;

    [SerializeField] private Animator swordAnim;
    [SerializeField] private VisualEffect swordSlashVFX;

    [SerializeField] private float damage = 5f;
    [SerializeField] private float attackDelay = 1.0f;
    [SerializeField] private float slashStartDelay = .1f;
    // Start is called before the first frame update
    void Start()
    {
        PlayerData.instance.inputManger.OnSwordAttackEv += performSlash;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private float attckTimer = 0;
    void performSlash()
    {
        
        
        if (Time.time - attckTimer > attackDelay)
        {
            
            DamageEnemy();
            
           Invoke(nameof(startSlash),slashStartDelay);
            swordAnim.SetBool("isAttacking",true);
            Invoke(nameof(stopAnimation), 0.5f);
            attckTimer = Time.time;
            Debug.Log("attaking");
        }
        
    }

    void DamageBoss()
    {
        if(Physics.Raycast(PlayerCam.position,PlayerCam.forward,out var hit, 2f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyController>().health -= damage;
            }
        }
    }    
    void DamageEnemy()
    {
        if(Physics.Raycast(PlayerCam.position,PlayerCam.forward,out var hit, 2f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyController>().health -= damage;
            }
        }
    }
    
    void stopAnimation()
    {
        swordAnim.SetBool("isAttacking",false);
    }

    void startSlash()
    {
        swordSlashVFX.Play();
    }
}
