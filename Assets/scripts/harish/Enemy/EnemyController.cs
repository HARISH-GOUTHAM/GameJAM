using System.Collections;
using System.Collections.Generic;
using harish.Player;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    patrol,
    agro,
    dead
}

public class EnemyController : MonoBehaviour
{
   public EnemyState state = EnemyState.patrol;

    [SerializeField] private List<Transform> patrolPoints;
    [SerializeField] private float fov;
    [SerializeField] private float visionRange = 10;
    [SerializeField] private float attackRange = 2;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private Animator animator;
    
    private Transform player;
    private NavMeshAgent agent;
    public health_bar_Script health_bar;
    public GameObject ragdoll;

    public float health = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        health_bar.set_max_health(health);
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(patrolPoints[0].position);
        player = PlayerData.instance.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        health_bar.set_health(health);
        if(state==EnemyState.dead)
        {
            Debug.Log("ded");
            agent.isStopped = true;
            Instantiate(ragdoll, transform.position,transform.rotation);
            Destroy(gameObject);
            
            return;
        }
        if (state == EnemyState.agro)
        {
            animator.SetBool("isRunning",true);
            ChasePlayer();
        }
        else if (state == EnemyState.patrol)
        {
            Patrol();
        }
      
        Vision();
        DeadCheck();
    }
    
    int currentPatrolPoint = 0;
    void Patrol()
    {
        
        if (agent.remainingDistance < 0.5f)
        {
            currentPatrolPoint++;
            
            currentPatrolPoint = currentPatrolPoint % patrolPoints.Count;
            
            agent.SetDestination(patrolPoints[currentPatrolPoint].position);
            
        }
        
    }

  

    void DeadCheck()
    {
        if (health <= 0)
        {
            state = EnemyState.dead;
        }    
    }
    
    void Vision()
    {

        if(Vector3.Angle(transform.forward, player.transform.position - transform.position) < fov)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized , out hit, visionRange))
            {
                if(hit.collider.gameObject.tag == "Player")
                {
                    state = EnemyState.agro;
                    
                    Debug.Log("DETECTED PLAYER");
                }
            }
        }
        
        
    }

    private float attackTime = 0;
    void ChasePlayer()
    {
        
        agent.SetDestination(player.position);
        
        if(agent.remainingDistance < attackRange)
        {
            if (Time.time - attackTime > attackSpeed)
            {
                //animator_.SetBool("is_running", false);

              Invoke(nameof(performAxeAttak), 1.8f);
              animator.SetBool("isAttacking", true);
                
                attackTime = Time.time;
               // animator_.SetBool("is_attacking", false);
               agent.speed = 0;


            }
        }
        else
        {
            animator.SetBool("isAttacking",false);
        }
    }
    
    void performAxeAttak()
    {
        if(agent.remainingDistance<attackRange)
         PlayerData.instance.health -= attackDamage;
        Debug.Log(health);
        Debug.Log("damaged plaer");
        agent.speed = 3.5f;
        
        animator.SetBool("isAttacking",false);
    }

    void stopAttack()
    {
        
    }
}
