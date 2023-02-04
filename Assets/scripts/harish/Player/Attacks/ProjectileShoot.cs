using System.Collections;
using System.Collections.Generic;
using harish.BossF;
using harish.Player;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
    [SerializeField] private Transform PlayerCam;
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject projectile;
    
    [SerializeField] private float damage = 5f;
    [SerializeField] private float attackDelay = 1.0f;
    [SerializeField] private float range = 30f;
    [SerializeField] private float force = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerData.instance.inputManger.OnShootEv += PerformShoot;
    }

   
    private float attckTimer = 0;
    void PerformShoot()
    {
        
        if (Time.time - attckTimer > attackDelay)
        {
            DamageBoss();
            DamageEnemy();
            
            
            attckTimer = Time.time;
            Debug.Log("attaking");
        }
        
    }

    private GameObject proj;
    void ShootProjectile(Vector3 dest)
    {
        if(proj != null)
            Destroy(proj);
        
        proj = Instantiate(projectile, startPoint.position, Quaternion.identity);
        proj.transform.rotation = Quaternion.LookRotation(dest - startPoint.position);
        proj.GetComponent<Rigidbody>().velocity = (dest - startPoint.position).normalized * force;
    }
    
    void DamageBoss()
    {
        if(Physics.Raycast(PlayerCam.position,PlayerCam.forward,out var hit, range))
        {
            if (hit.collider.CompareTag("BossArm"))
            {
                Debug.Log("Enemy arm hit");
                hit.collider.GetComponent<Arm>().TakeDamage(damage);
            }
        }
        ShootProjectile(hit.point);
    }    
    void DamageEnemy()
    {
        if(Physics.Raycast(PlayerCam.position,PlayerCam.forward,out var hit, range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyController>().health -= damage;
            }
        }
        ShootProjectile(hit.point);
    }
}
