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

    private Transform player;
    private NavMeshAgent agent;
    
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
        if (state == EnemyState.agro)
        {
            ChasePlayer();
        }
        else if (state == EnemyState.patrol)
        {
            Patrol();
        }
        Vision();
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
    
    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        
        if(agent.remainingDistance < 1f)
        {
            //attack code here
        }
    }
}
