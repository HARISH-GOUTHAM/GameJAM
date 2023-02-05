using System;
using System.Collections;
using System.Collections.Generic;
using harish.Player;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public enum BossState
{
    Idle,
    Saw,
    Chainsaw,
    Overheat
}

public class Boss : MonoBehaviour
{
    public BossState state = BossState.Idle;

    public float lArmHealth = 50;
    public float rArmHealth = 20;

    public int armDeadCount = 0;
    
    [SerializeField] Animator bossAnim;
    [SerializeField] float stateChangeDelay = 1f;
    [SerializeField] float afterAttackDelay = 1f;
    [SerializeField] private float overheatDelay = 7f;
    [SerializeField] private int overheatAttackCount = 5;
    [SerializeField] private Transform rotatePoint;
    [SerializeField] private float rotationOffset = 90;
    [SerializeField] private float rotateSpeed = 5f;
    private int attacksCount = 0;
    
    public static Boss instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StateUpdate();
    }

    private void Update()
    {
        if(state == BossState.Saw)
            RotateTowardsPlayer();
    }

    void StateUpdate()
    {
        if (armDeadCount >= 2)
        {
            bossAnim.speed = 100;
            
        }
        
        if (attacksCount > overheatAttackCount)
        {
            state = BossState.Overheat;
            CallStateFunction(BossState.Overheat);
            attacksCount = 0;
            return;
            
        }
        
        state = GetRandomState();
        attacksCount++;
        CallStateFunction(state);
    }

    void CallStateFunction(BossState s)
    {
        switch (s)
        {
            case BossState.Idle:
                
                break;
            case BossState.Saw:
                Invoke(nameof(Saw),stateChangeDelay);
                break;
            case BossState.Chainsaw:
                Invoke(nameof(chainSaw),stateChangeDelay);
                
                break;
            case BossState.Overheat:
                Overheat();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    BossState GetRandomState()
    {
        if (Random.Range(0, 3) == 0)
        {
            return BossState.Chainsaw;
        }
        else
        {
            return BossState.Saw;
        }
    }
    
    
    
    void chainSaw()
    {
        bossAnim.SetBool("chainsawAttack",true);
        Invoke(nameof(ResetAnimatorVariables),.1f);
        SetIdleState();
    }

    void Saw()
    {
        bossAnim.SetBool("sawAttack",true);
        Invoke(nameof(ResetAnimatorVariables),.1f);
        SetIdleState();
    }

    void Overheat()
    {
        bossAnim.SetBool("overheat",true);
        
        Invoke(nameof(ResetAnimatorVariables),.1f);
        Invoke(nameof(SetIdleState),overheatDelay);
    }

    void SetIdleState()
    {
        state = BossState.Idle;
        Invoke(nameof(StateUpdate),stateChangeDelay);
    }

    void ResetAnimatorVariables()
    {
        bossAnim.SetBool("chainsawAttack",false);
        bossAnim.SetBool("sawAttack",false);
        bossAnim.SetBool("overheat",false);
    }

    void RotateTowardsPlayer()
    {
        Vector3 dir =  PlayerData.instance.transform.position - rotatePoint.position ;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotatePoint.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        rotatePoint.rotation = Quaternion.Euler(0f, rotation.y + rotationOffset, 0f);
        
    }
    
}
