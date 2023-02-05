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
    [SerializeField] private float manaRequired = 20;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData.instance.inputManger.OnShootEv += PerformShoot;
    }

    private void Update()
    {

    }
    private float attckTimer = 0;
    void PerformShoot()
    {
        
        if (Time.time - attckTimer > attackDelay && PlayerData.instance.mana >= 20)
        {
            DamageBoss();
            DamageEnemy();
            
            PlayerData.instance.mana -= manaRequired;
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
        proj.GetComponent<Rigidbody>().velocity = (startPoint.forward).normalized * force;
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
            ShootProjectile(hit.point);
        }
        else
        {
            ShootProjectile(PlayerCam.forward*100);
        }
        
    }    
    void DamageEnemy()
    {
        if(Physics.Raycast(PlayerCam.position,PlayerCam.forward,out var hit, range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyController>().health -= damage;
            }
            ShootProjectile(hit.point);
        }
        ShootProjectile(PlayerCam.forward*100);
    }
}
