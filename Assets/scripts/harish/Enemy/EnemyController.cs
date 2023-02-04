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
    private Transform player;
    private NavMeshAgent agent;

    public float health = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(patrolPoints[0].position);
        player = PlayerData.instance.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state==EnemyState.dead)
        {
            Debug.Log("ded");
            agent.isStopped = true;
            
            Destroy(gameObject);
            return;
        }
        if (state == EnemyState.agro)
        {
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
        Debug.Log(Vector3.Angle(transform.forward, player.transform.position - transform.position));
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
                PlayerData.instance.health -= attackDamage;
                Debug.Log("damaged plaer");
                attackTime = Time.time;
            }    
        }
    }
}
