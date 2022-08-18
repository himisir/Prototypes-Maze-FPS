using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _target):
    base (_npc, _agent, _animator, _target){
        name = STATE.IDLE;
    }

    public override void Enter(){
        Debug.Log("Enter Idle");
        animator.SetBool("isIdle", true);
    }

    

    
}