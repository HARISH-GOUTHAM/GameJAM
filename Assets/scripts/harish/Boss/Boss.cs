using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] Animator bossAnim;
    [SerializeField] float stateChangeDelay = 1f;
    [SerializeField] float afterAttackDelay = 1f;

    private bool isAttacking = false;
    
    // Start is called before the first frame update
    void Start()
    {
        StateUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void StateUpdate()
    {
        state = GetRandomState();
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

    void SetIdleState()
    {
        state = BossState.Idle;
        Invoke(nameof(StateUpdate),stateChangeDelay);
    }

    void ResetAnimatorVariables()
    {
        bossAnim.SetBool("chainsawAttack",false);
        bossAnim.SetBool("sawAttack",false);
    }
}
