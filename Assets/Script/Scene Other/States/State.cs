using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE, PATROL, PURSUE, FIRE, ATTACK, RELOAD, WEAPON, DEAD
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name; 
    protected EVENT stage;
    protected GameObject npc; 
    protected Animator animator;
    protected Transform target;
    protected State nextState; 
    protected NavMeshAgent agent;

    public State(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _target)
    {
        npc = _npc;
        agent = _agent;
        animator = _animator;
        target = _target;
    }

    public virtual void Enter()
    {
        stage = EVENT.ENTER;
    }
    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }
    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public State Process(){
        if(stage == EVENT.ENTER){
            Enter();
        }
        if(stage == EVENT.UPDATE){
            Update();
        }
        if(stage == EVENT.EXIT){
            Exit();
            return nextState;
        }
        return this; 
    }
    
}
