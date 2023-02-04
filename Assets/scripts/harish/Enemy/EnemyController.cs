using System.Collections;
using System.Collections.Generic;
using harish.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public Image Health_bar;
    private float H_health;//ignore its for UI
    public Animator animator_;
    private Transform player;
    private NavMeshAgent agent;

    public float health = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        H_health = health;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(patrolPoints[0].position);
        player = PlayerData.instance.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Health_bar.fillAmount = health / H_health;
        if(state==EnemyState.dead)
        {
            Debug.Log("ded");
            agent.isStopped = true;
            
            Destroy(gameObject);
            return;
        }
        if (state == EnemyState.agro)
        {
        animator_.SetBool("is_running", true);
            ChasePlayer();
        }
        else if (state == EnemyState.patrol)
        {
        animator_.SetBool("is_walking", true);
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
               
                animator_.SetBool("is_attacking", true);
                PlayerData.instance.health -= attackDamage;
                Debug.Log(health);
                Debug.Log("damaged plaer");
                attackTime = Time.time;
                animator_.SetBool("is_attacking", false);

            }
        }
    }
}
